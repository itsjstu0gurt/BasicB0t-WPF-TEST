using BasicB0t.Logging;
using BasicB0t.Services;
using BasicB0t.Settings;
using BasicB0t.Events;
using BasicB0t.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchLib.Api;
using TwitchLib.Api.ThirdParty.UsernameChange;
using TwitchLib.Client;
using TwitchLib.Client.Events;
using TwitchLib.Client.Interfaces;
using TwitchLib.Client.Models;
using TwitchLib.Communication.Events;

namespace BasicB0t.Twitch
{
    public class TwitchAuthService : ITwitchService
    {
        public event EventHandler<TwitchUserConnectedEventArgs> TwitchUserConnected;
        public event EventHandler<TwitchUserDisconnectedEventArgs> TwitchUserDisconnected;
        public event EventHandler<TwitchMessageRecievedEventArgs> TwitchMessageRecieved;

        private TwitchClient _twitchClient;
        public TwitchSettingsViewModel _twitchSettingsViewModel;
        public TwitchAPI api;
        public WebServer server;
        private readonly Logger logger;

        private readonly string clientID = Client.Default.ClientID;
        private readonly string clientSecret = Client.Default.ClientSecret;
        private readonly string redirectURL =  Client.Default.RedirectURL;

        public TwitchAuthService()
        {
            logger = Logger.GetInstance();
            logger.Log("TwitchAuthService Initializing...", LogLevel.Info);
            
            //_twitchClient = new TwitchClient();
            api = new TwitchAPI();
            server = new WebServer(redirectURL);

            
            
            UserClient.Default.Reload();
            
            api.Settings.ClientId = clientID;
            api.Settings.Secret = clientSecret;

            //InitializeEvents();

            logger.Log("TwitchAuthService Initialized.", LogLevel.Info);
        }

        public void InitializeTwitchClient()
        {
            _twitchClient = new TwitchClient();
            _twitchClient.OnConnected += OnConnectedHandler;
            _twitchClient.OnDisconnected += OnDisconnectedHandler;
            _twitchClient.OnMessageReceived += OnMessageReceivedHandler;
            _twitchClient.OnJoinedChannel += OnJoinedChannelHandler;
            _twitchClient.OnLeftChannel += OnLeftChannelHandler;

        }

        public async void ConnectUserClient()
        {
            if (_twitchClient != null)
            {
                if (_twitchClient.IsConnected)
                {
                    logger.Log("Already connected to Twitch.", LogLevel.Info);
                    return;
                }
            }
            logger.Log("Connecting to Twitch Client...", LogLevel.Info);
            logger.Log("Checking for token...", LogLevel.Info);
            if (string.IsNullOrEmpty(UserClient.Default.OauthToken))
            {
                logger.Log("No token found, authorizing...", LogLevel.Info);
                await Authorize();

            }
            else
            {
                if (DateTime.Now > DateTime.Parse(UserClient.Default.TokenExpiry))
                {
                    logger.Log("Token expired, refreshing...", LogLevel.Info);
                    await RefreshToken(UserClient.Default.RefreshToken);
                }
            }

            logger.Log("Token found, connecting to Twitch...", LogLevel.Info);
            ConnectTwitchClient(UserClient.Default.OauthToken, UserClient.Default.Username);

        }

        public async Task Authorize()
        {
            OpenBrowser(GetAuthorizationCodeUrl(StaticSettings.scopes));

            var auth = await server.Listen();
            var resp = await api.Auth.GetAccessTokenFromCodeAsync(auth.Code, clientSecret, redirectURL);

            if (resp != null)
            {
                api.Settings.AccessToken = resp.AccessToken;
                api.Settings.Secret = clientSecret;
                UserClient.Default.OauthToken = api.Settings.AccessToken;
                UserClient.Default.RefreshToken = resp.RefreshToken;
                UserClient.Default.TokenExpiry = (DateTime.Now.AddSeconds(resp.ExpiresIn)).ToString();
                var usersResponse = await api.Helix.Users.GetUsersAsync();
                var currentUser = usersResponse.Users[0];
                UserClient.Default.Username = currentUser.DisplayName;
                UserClient.Default.UserID = currentUser.Id;
                UserClient.Default.ProfileURL = currentUser.ProfileImageUrl;
                //getUserChatColor().Wait();
                logger.Log("Got new tokens.", LogLevel.Info);
                return;
            }

        }

        private void OpenBrowser(string url) => Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });

        private string GetAuthorizationCodeUrl(List<string> scopes)
        {
            var scopesStr = string.Join("+", scopes);

            return "https://id.twitch.tv/oauth2/authorize?" +
                   $"client_id={clientID}&" +
                   $"redirect_uri={System.Web.HttpUtility.UrlEncode(redirectURL)}&" +
                   "response_type=code&" +
                   $"scope={scopesStr}";
        }

        public async Task getUserChatColor()
        {

            var userscolorResponse = await api.Helix.Chat.GetUserChatColorAsync(new List<string> { UserClient.Default.UserID });
            var currentUserColor = userscolorResponse.Data[0];
            UserClient.Default.ProfileColor = currentUserColor.Color;
            UserClient.Default.Save();
        }

        public async Task<bool> RefreshToken(string refreshToken)
        {
            logger.Log("Refreshing token...", LogLevel.Info);
            try
            {
                var token = await api.Auth.RefreshAuthTokenAsync(refreshToken, clientSecret, clientID);

                if (token != null)
                {
                    api.Settings.AccessToken = token.AccessToken;
                    UserClient.Default.OauthToken = token.AccessToken;
                    UserClient.Default.RefreshToken = token.RefreshToken;
                    UserClient.Default.TokenExpiry = (DateTime.Now.AddSeconds(token.ExpiresIn)).ToString();
                    UserClient.Default.Save();
                    logger.Log("Token refreshed.", LogLevel.Info);
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                logger.Log($"Error refreshing token: {ex.Message}", LogLevel.Error);
                return false;
            }
        }

        private void ConnectTwitchClient(string oauthToken, string username)
        {
            InitializeTwitchClient();
            _twitchClient.Initialize(new ConnectionCredentials(username, oauthToken, redirectURL));

            if (_twitchClient.IsInitialized)
            {
                logger.Log("Twitch Client Initialized.", LogLevel.Info);
            }
            else
            {
                logger.Log("Error initializing Twitch Client.", LogLevel.Error);
                return;
            }

            if (!_twitchClient.Connect())
            {

                api.Settings.Secret = clientSecret;


                logger.Log("Error connecting to Twitch.", LogLevel.Error);
                ReconnectUser();
            }
        }

        private void ReconnectUser()
        {
            int reconnectAttempts = 0;
            while (!_twitchClient.IsConnected && reconnectAttempts < 5)
            {
                logger.Log("Attempting to reconnect...", LogLevel.Info);
                reconnectAttempts++;
                if (!_twitchClient.Connect())
                {
                    if (reconnectAttempts == 5)
                    {
                        logger.Log("Failed to reconnect to Twitch.", LogLevel.Info);
                        return;
                    }

                    logger.Log("Error reconnecting to Twitch.", LogLevel.Info);
                }
                else
                {
                    logger.Log("Reconnected to Twitch.", LogLevel.Info);
                }

            }
        }

        public void DisconnectUserClient()
        {
            logger.Log("Disconnecting from Twitch...", LogLevel.Info);
            if (!_twitchClient.IsConnected)
            {
                logger.Log("Already disconnected from Twitch.", LogLevel.Info);
                return;
            }
            _twitchClient.Disconnect();
            if (_twitchClient.IsConnected)
            {
                logger.Log("Error disconnecting from Twitch.", LogLevel.Error);
                return;
            }
            

        }

        public void ClearUserSettings()
        {
            logger.Log("Clearing User Settings....", LogLevel.Info);
            api.Settings.AccessToken = string.Empty;
            UserClient.Default.RefreshToken = string.Empty;
            UserClient.Default.OauthToken = string.Empty;
            UserClient.Default.Username = string.Empty;
            UserClient.Default.UserID = string.Empty;
            UserClient.Default.TokenExpiry = DateTime.Now.ToString();
            UserClient.Default.ProfileURL = string.Empty;
            UserClient.Default.ProfileColor = string.Empty;
            UserClient.Default.Save();
            logger.Log("User Settings Cleared.", LogLevel.Info);
        }

        public void OnConnectedHandler(object sender, OnConnectedArgs e)
        {
            logger.Log("Connected to Twitch.", LogLevel.Info);
            getUserChatColor().Wait(); 
            //_twitchSettingsViewModel.UpdateUserUI(UserClient.Default.Username, UserClient.Default.ProfileURL);
            TwitchUserConnected?.Invoke(this, new TwitchUserConnectedEventArgs(UserClient.Default.Username, UserClient.Default.ProfileURL, UserClient.Default.ProfileColor));
        }

        private void OnDisconnectedHandler(object sender, OnDisconnectedEventArgs e)
        {
            logger.Log("Disconnected from Twitch.", LogLevel.Info);
            TwitchUserDisconnected?.Invoke(this, new TwitchUserDisconnectedEventArgs(UserClient.Default.Username, UserClient.Default.ProfileURL, UserClient.Default.ProfileColor));
            _twitchClient.OnConnected -= OnConnectedHandler;
            _twitchClient.OnDisconnected -= OnDisconnectedHandler;
            _twitchClient.OnMessageReceived -= OnMessageReceivedHandler;
            _twitchClient.OnJoinedChannel -= OnJoinedChannelHandler;
            _twitchClient.OnLeftChannel -= OnLeftChannelHandler;
        }
    

        private void OnMessageReceivedHandler(object sender, OnMessageReceivedArgs e)
        {
            logger.Log($"Message Received: {e.ChatMessage.Message}", LogLevel.Info);
            TwitchMessageRecieved?.Invoke(this, new TwitchMessageRecievedEventArgs(e.ChatMessage.Badges, e.ChatMessage.Username, e.ChatMessage.Color, e.ChatMessage.Message));
        }

        private void OnJoinedChannelHandler(object sender, OnJoinedChannelArgs e)
        {
            logger.Log($"Joined Channel: {e.Channel}", LogLevel.Info);
        }

        private void OnLeftChannelHandler(object sender, OnLeftChannelArgs e)
        {
            logger.Log($"Left Channel: {e.Channel}", LogLevel.Info);
        }

    }
}

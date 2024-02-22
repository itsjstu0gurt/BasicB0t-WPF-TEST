using BasicB0t.Logging;
using BasicB0t.Services;
using BasicB0t.Twitch;
using BasicB0t.Events;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BasicB0t.Enums;
using System.Collections.ObjectModel;
using System.Drawing;

namespace BasicB0t.ViewModels
{
    public class TwitchSettingsViewModel : INotifyPropertyChanged
    {
       

        private readonly Logger logger;
        private readonly ITwitchService _twitchAuthService;
        private ChatColorPresets _selectedChatColor;

        //public ObservableCollection<ChatColorPresets> ChatColorOptions;
        //public ObservableCollection<object> ChatColorOptions { get; }

        public List<ChatColorPresets> ChatColorOptions { get; }

        public ICommand ConnectUserBtn { get; }
        public ICommand DisconnectUserBtn { get; }
        public ICommand ForgetUserBtn { get; }
                
        private string _username;
        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        private string _userProfileImg;
        public string UserProfileImg
        {
            get { return _userProfileImg; }
            set
            {
                _userProfileImg = value;
                OnPropertyChanged(nameof(UserProfileImg));
            }
        }

        private string _userColor;
        public string UserColor
        {
            get { return _userColor; }
            set
            {
                _userColor = value;
                OnPropertyChanged(nameof(UserColor));
            }
        }

        private string _botname;
        public string Botname
        {
            get { return _botname; }
            set
            {
                _botname = value;
                OnPropertyChanged(nameof(Botname));
            }
        }

        private string _botColor;
        public string BotColor
        {
            get { return _botColor; }
            set
            {
                _botColor = value;
                OnPropertyChanged(nameof(BotColor));
            }
        }

        private string _botProfileImg;
        public string BotProfileImg
        {
            get { return _botProfileImg; }
            set
            {
                _botProfileImg = value;
                OnPropertyChanged(nameof(BotProfileImg));
            }
        }

        public ChatColorPresets SelectedChatColor
        {
            get { return _selectedChatColor; }
            set
            {
                if (_selectedChatColor != value)
                {
                    _selectedChatColor = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedChatColor)));
                }
            }
        }



        // INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public TwitchSettingsViewModel(ITwitchService twitchAuthService)
        {
            logger = Logger.GetInstance();
            logger.Log("TwitchSettingsViewModel Initializing...", LogLevel.Info);
            _twitchAuthService = twitchAuthService;
            

            twitchAuthService.TwitchUserConnected += UpdateUserConnectUI;
            twitchAuthService.TwitchUserDisconnected += UpdateUserDisconnectUI;
            


            ChatColorOptions = Enum.GetValues(typeof(ChatColorPresets)).Cast<ChatColorPresets>().ToList();

            ConnectUserBtn = new RelayCommand(ConnectUser);
            DisconnectUserBtn = new RelayCommand(DisconnectUser);
            ForgetUserBtn = new RelayCommand(ForgetUser);
            logger.Log("TwitchSettingsViewModel Initialized.", LogLevel.Info);
            
            


            _username = "Press Connect Button Below";
            _userColor = "Gold";
            _botname = "Press Connect Button Below";
            _userProfileImg = "pack://application:,,,/Resources/Images/NoImage.png";
            _botProfileImg = "pack://application:,,,/Resources/Images/NoImage.png";


        }

        private async void ConnectUser()
        {
            logger.Log("ConnectUser Button Pressed", LogLevel.Info);
            Username = "Connecting...";
            _twitchAuthService.ConnectUserClient();
            
        }

        private async void DisconnectUser()
        {
            logger.Log("DisconnectUser Button Pressed", LogLevel.Debug);
            Username = "Disconnecting...";
            _twitchAuthService.DisconnectUserClient();

        }

        private async void ForgetUser()
        {
            logger.Log("DisconnectUser Button Pressed", LogLevel.Debug);
            Username = "Disconnecting...";
            //_twitchAuthService.DisconnectUserClient();
            _twitchAuthService.ClearUserSettings();

        }

        public void UpdateUserConnectUI(object sender, TwitchUserConnectedEventArgs e)
        {
            logger.Log("Twitch User Connected.", LogLevel.Info);
            Username = e.Username;

            

            UserProfileImg = e.UserProfileImg;
            logger.Log("Username: " + e.Username + " | Chat Color: " + e.UserChatColor, LogLevel.Info);
        }


       


        public void UpdateUserDisconnectUI(object sender, TwitchUserDisconnectedEventArgs e)
        {
            logger.Log("Twitch User Disconnected.", LogLevel.Info);
            Username = "Press Connect Button Below";
            UserColor = "Gold";
            UserProfileImg = "pack://application:,,,/Resources/Images/NoImage.png";
        }
    }
}

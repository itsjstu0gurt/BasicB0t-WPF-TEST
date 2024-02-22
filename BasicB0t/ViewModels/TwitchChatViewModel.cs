using BasicB0t.Models;
using BasicB0t.Services;
using BasicB0t.Twitch;
using BasicB0t.Views;
using BasicB0t.Events;
using BasicB0t.Enums;
using BasicB0t.Logging;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using TwitchLib.Client.Models;
using System.Drawing;


namespace BasicB0t.ViewModels
{
    public class TwitchChatViewModel : ObservableObject
    {
        private readonly ITwitchService _twitchAuthService;
        private readonly Logger logger;

        public ObservableCollection<IncomingChatMessage> IncomingChatMessages { get; } = new ObservableCollection<IncomingChatMessage>();




        public TwitchChatViewModel(ITwitchService twitchAuthService)
        {
            logger = Logger.GetInstance();
            logger.Log("TwitchChatViewModel Initializing...", LogLevel.Info);
            _twitchAuthService = twitchAuthService;
            _twitchAuthService.TwitchMessageRecieved += OnMessageReceived;
        }

        private void OnMessageReceived(object sender, TwitchMessageRecievedEventArgs e)
        {
            string username = e.ChatUsername;
            Color userColor = e.ChatUserColor; // You need to implement a method to get the user's color
            string message = e.ChatUserMessage;
            List<KeyValuePair<string, string>> badges = e.ChatUserBadges; // Assuming badges is a list of badge URLs

            // Create a new instance of the ChatMessage model
            IncomingChatMessage chatMessage = new IncomingChatMessage(username, userColor, message, badges);

            // Add the chat message to the collection
            
            App.Current.Dispatcher.Invoke(() => // Ensure UI updates happen on the UI thread
            {
                IncomingChatMessages.Add(chatMessage);
            });
        }
    }
}

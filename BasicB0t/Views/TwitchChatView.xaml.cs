using BasicB0t.Services;
using BasicB0t.Twitch;
using BasicB0t.ViewModels;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace BasicB0t.Views
{
    public partial class TwitchChatView : UserControl
    {
        private readonly ITwitchService _twitchAuthService;
       

        public TwitchChatView()
        {
            InitializeComponent();
            // Create an instance of TwitchAuthService
            _twitchAuthService = new TwitchAuthService();
            // Pass the twitchAuthService instance to the view model
            DataContext = new TwitchChatViewModel(_twitchAuthService);
            

        }
    }
}

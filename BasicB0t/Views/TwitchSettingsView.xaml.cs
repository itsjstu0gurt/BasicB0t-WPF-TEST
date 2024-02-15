using BasicB0t.Services;
using BasicB0t.Twitch;
using BasicB0t.ViewModels;
using System.Windows.Controls;

namespace BasicB0t.Views
{
    /// <summary>
    /// Interaction logic for TwitchSettings.xaml
    /// </summary>
    public partial class TwitchSettingsView : UserControl
    {
        private readonly ITwitchService _twitchAuthService;

        public TwitchSettingsView()
        {
            InitializeComponent();
            // Create an instance of TwitchAuthService
            _twitchAuthService = new TwitchAuthService();
            // Pass the twitchAuthService instance to the view model
            DataContext = new TwitchSettingsViewModel(_twitchAuthService);
        }

        // Optional: If you need a constructor with parameters, you can add it as well
        // public TwitchSettingsView(ITwitchService twitchAuthService) { ... }
    }
}

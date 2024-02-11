using BasicB0t.Logging;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicB0t.ViewModels
{
    public class TwitchSettingsViewModel : INotifyPropertyChanged
    {
        private readonly Logger logger;
                
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

        // INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public TwitchSettingsViewModel()
        {
            logger = Logger.GetInstance();
            logger.Log("TwitchSettingsViewModel Initializing...", LogLevel.Info);
            logger.Log("TwitchSettingsViewModel Initialized.", LogLevel.Info);

            _username = "Press Connect Button Below";
            _botname = "Press Connect Button Below";
            _userProfileImg = "pack://application:,,,/Resources/Images/NoImage.png";
            _botProfileImg = "pack://application:,,,/Resources/Images/NoImage.png";


        }

    }
}

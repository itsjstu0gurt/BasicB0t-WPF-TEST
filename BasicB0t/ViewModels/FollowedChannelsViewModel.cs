using BasicB0t.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicB0t.ViewModels
{
    public class FollowedChannelsViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<FollowedChannel> _followedChannels;

        public ObservableCollection<FollowedChannel> FollowedChannels
        {
            get { return _followedChannels; }
            set
            {
                _followedChannels = value;
                OnPropertyChanged(nameof(FollowedChannels));
            }
        }

        public FollowedChannelsViewModel()
        {
            // Initialize the FollowedChannels collection with sample data
            FollowedChannels = new ObservableCollection<FollowedChannel>
            {
                new FollowedChannel
                {
                    StreamUsername = "LIRIK",
                    StreamTitle = "Lirik's Stream",
                    StreamCategory = "Just Chatting",
                    StreamViewers = 10000
                },
                new FollowedChannel
                {
                    StreamUsername = "summit1g",
                    StreamTitle = "Summit1g's Stream",
                    StreamCategory = "Just Chatting",
                    StreamViewers = 8000
                },
                new FollowedChannel
                {
                    StreamUsername = "shroud",
                    StreamTitle = "Shroud's Stream",
                    StreamCategory = "Just Chatting",
                    StreamViewers = 7000
                }
            };
            
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

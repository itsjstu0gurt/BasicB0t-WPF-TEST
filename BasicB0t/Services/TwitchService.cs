using BasicB0t.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicB0t.Services
{
    public interface ITwitchService
    {
        event EventHandler<TwitchUserConnectedEventArgs> TwitchUserConnected;
        event EventHandler<TwitchUserDisconnectedEventArgs> TwitchUserDisconnected;
        void ConnectUserClient();
        void DisconnectUserClient();
        void ClearUserSettings();
    }
}

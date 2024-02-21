using System.Drawing;
using System.Windows.Media.Imaging;

namespace BasicB0t.Events
{
    public class TwitchMessageRecievedEventArgs : EventArgs
    {
        public List<KeyValuePair<string, string>> ChatUserBadges { get; set; }
        public string ChatUsername { get; set; }
        public Color ChatUserColor { get; set; }
        public string ChatUserMessage { get; set; }

        public TwitchMessageRecievedEventArgs(List<KeyValuePair<string, string>> chatUserBadges, string chatUsername, Color chatUserColor, string chatUserMessage)
        {
            ChatUserBadges = chatUserBadges;
            ChatUsername = chatUsername;            
            ChatUserColor = chatUserColor;
            ChatUserMessage = chatUserMessage;
        }
    }
}

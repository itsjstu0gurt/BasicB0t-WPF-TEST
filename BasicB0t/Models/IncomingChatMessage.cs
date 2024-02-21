using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace BasicB0t.Models
{
    public class IncomingChatMessage
    {
        public string Username { get; set; }
        public Color UserColor { get; set; }
        public string Message { get; set; }
        public List<KeyValuePair<string, string>> Badges { get; set; }
        // You can add a property for the timestamp here if needed

        public IncomingChatMessage(string username, Color userColor, string message, List< KeyValuePair<string, string> > badges)
        {
            Username = username;
            UserColor = userColor;
            Message = message;
            Badges = badges;
        }
    }
}

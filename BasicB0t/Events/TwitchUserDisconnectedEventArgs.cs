namespace BasicB0t.Events
{
    public class TwitchUserDisconnectedEventArgs : EventArgs
    {
        public string Username { get; set; }
        public string UserProfileImg { get; set; }
        public string UserChatColor { get; set; }

        public TwitchUserDisconnectedEventArgs(string username, string userProfileImg, string userChatColor)
        {
            Username = username;
            UserProfileImg = userProfileImg;
            UserChatColor = userChatColor;

        }
    }
}

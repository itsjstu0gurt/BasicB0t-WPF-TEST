namespace BasicB0t.Events
{
    public class TwitchUserConnectedEventArgs : EventArgs
    {
        public string Username { get; set; }
        public string UserProfileImg { get; set; }
        public string UserChatColor { get; set; }

        public TwitchUserConnectedEventArgs(string username, string userProfileImg, string userChatColor)
        {
            Username = username;
            UserProfileImg = userProfileImg;
            UserChatColor = userChatColor;

        }
    }
}

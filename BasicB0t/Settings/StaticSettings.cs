﻿using System.Collections.Generic;
using System.Reflection;

namespace BasicB0t
{
    /// <summary>
    /// The place we will have varible and stuff we might need
    /// And keep stuff tidy
    /// </summary>
    public static class StaticSettings
    {

        public static List<string> scopes = new List<string> {
            "bits:read",
            "channel:manage:ads",
            "channel:read:ads",
            "channel:manage:broadcast",
            "channel:read:charity",
            "channel:edit:commercial",
            "channel:read:editors",
            "channel:manage:extensions",
            "channel:read:goals",
            "channel:read:guest_star",
            "channel:manage:guest_star",
            "channel:read:hype_train",
            "channel:manage:moderators",
            "channel:read:polls",
            "channel:manage:polls",
            "channel:read:predictions",
            "channel:manage:predictions",
            "channel:manage:raids",
            "channel:read:redemptions",
            "channel:manage:redemptions",
            "channel:manage:schedule",
            "channel:read:stream_key",
            "channel:read:subscriptions",
            "channel:manage:videos",
            "channel:read:vips",
            "channel:manage:vips",
            "clips:edit",
            "moderation:read",
            "moderator:manage:announcements",
            "moderator:manage:automod",
            "moderator:read:automod_settings",
            "moderator:manage:automod_settings",
            "moderator:manage:banned_users",
            "moderator:read:blocked_terms",
            "moderator:manage:blocked_terms",
            "moderator:manage:chat_messages",
            "moderator:read:chat_settings",
            "moderator:manage:chat_settings",
            "moderator:read:chatters",
            "moderator:read:followers",
            "moderator:read:guest_star",
            "moderator:manage:guest_star",
            "moderator:read:shield_mode",
            "moderator:manage:shield_mode",
            "moderator:read:shoutouts",
            "moderator:manage:shoutouts",
            "user:edit",
            "user:edit:follows",
            "user:read:blocked_users",
            "user:manage:blocked_users",
            "user:read:broadcast",
            "user:manage:chat_color",
            "user:read:email",
            "user:read:follows",
            "user:read:subscriptions",
            "user:manage:whispers",
            "channel:bot",
            "channel:moderate",
            "chat:edit",
            "chat:read",
            "user:bot",
            "user:read:chat",
            "whispers:read",
            "whispers:edit"
};

        public static string AppName => "BasicB0t";

        public static string AppVersion => Assembly.GetExecutingAssembly().GetName().Version.ToString();


    }
}

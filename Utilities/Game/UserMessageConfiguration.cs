using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace CodeConcussion.KVL.Utilities.Game
{
    internal static class UserMessageConfiguration
    {
        private static Dictionary<UserMessage, string> _messages;
        public static Dictionary<UserMessage, string> Messages
        {
            get { return _messages ?? (_messages = GetMessages()); }
        }

        private static Dictionary<UserMessage, string> GetMessages()
        {
            var settings = ConfigurationManager.AppSettings.AllKeys.Where(x => x.StartsWith("Message."));
            var messages = settings
                .Select( x => new KeyValuePair<UserMessage, string>(KeyToGameMessage(x), ConfigurationManager.AppSettings[x]))
                .ToDictionary(x => x.Key, x => x.Value);
            return messages;
        }

        private static UserMessage KeyToGameMessage(string key)
        {
            return (UserMessage)Enum.Parse(typeof(UserMessage), key.Replace("Message.", ""));
        }
    }
}

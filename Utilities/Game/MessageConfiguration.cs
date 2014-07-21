using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace CodeConcussion.KVL.Utilities.Game
{
    internal static class MessageConfiguration
    {
        private static Dictionary<GameMessage, string> _messages;
        public static Dictionary<GameMessage, string> Messages
        {
            get { return _messages ?? (_messages = GetMessages()); }
        }

        private static Dictionary<GameMessage, string> GetMessages()
        {
            var settings = ConfigurationManager.AppSettings.AllKeys.Where(x => x.StartsWith("Message."));
            var messages = settings
                .Select( x => new KeyValuePair<GameMessage, string>(KeyToGameMessage(x), ConfigurationManager.AppSettings[x]))
                .ToDictionary(x => x.Key, x => x.Value);
            return messages;
        }

        private static GameMessage KeyToGameMessage(string key)
        {
            return (GameMessage)Enum.Parse(typeof(GameMessage), key.Replace("Message.", ""));
        }
    }
}

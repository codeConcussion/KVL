using System;
using System.Configuration;
using System.IO;
using System.Text;
using CodeConcussion.KVL.Entities;
using Newtonsoft.Json;

namespace CodeConcussion.KVL.Utilities.Game
{
    internal static class UserStorage
    {
        public static User LoadUser(string userName)
        {
            var file = GetUserFile(userName);
            if (!File.Exists(file)) return new User(userName);

            var serialized = File.ReadAllText(file);
            var decoded = Convert.FromBase64String(serialized);
            var json = Encoding.ASCII.GetString(decoded);
            var user = JsonConvert.DeserializeObject<User>(json);
            return user;
        }

        public static void SaveUser(User user)
        {
            if (string.IsNullOrWhiteSpace(user?.Name)) return;

            var file = GetUserFile(user.Name);
            var json = JsonConvert.SerializeObject(user, Formatting.Indented);
            var serialized = Encoding.ASCII.GetBytes(json);
            var encoded = Convert.ToBase64String(serialized);
            File.WriteAllText(file, encoded);
        }

        private static string GetUserFile(string userName)
        {
            var setting = ConfigurationManager.AppSettings["UserDirectory"] ?? @".\Users";
            var directory = Path.GetFullPath(setting);
            if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);
            return Path.Combine(directory, userName + ".kvl");
        }
    }
}
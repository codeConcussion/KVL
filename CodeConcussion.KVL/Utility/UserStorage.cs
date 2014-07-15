using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using CodeConcussion.KVL.Entity;
using Newtonsoft.Json;

namespace CodeConcussion.KVL.Utility
{
    internal static class UserStorage
    {
        public static List<Record> LoadRecords(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName)) return new List<Record>();

            var file = GetRecordFile(userName);
            var serialized = File.ReadAllText(file);
            var decoded = Convert.FromBase64String(serialized);
            var json = Encoding.ASCII.GetString(decoded);
            var records = JsonConvert.DeserializeObject<List<Record>>(json);
            return records;
        }

        public static void SaveRecords(User user)
        {
            if (user == null) return;
            if (!user.Records.Any()) return;
            if (string.IsNullOrWhiteSpace(user.Name)) return;

            var file = GetRecordFile(user.Name);
            var json = JsonConvert.SerializeObject(user.Records, Formatting.Indented);
            var serialized = Encoding.ASCII.GetBytes(json);
            var encoded = Convert.ToBase64String(serialized);
            File.WriteAllText(file, encoded);
        }

        private static string GetRecordFile(string userName)
        {
            var setting = ConfigurationManager.AppSettings["RecordDirectory"] ?? @".\Records";
            var directory = Path.GetFullPath(setting);
            if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);
            return Path.Combine(directory, userName + ".kvl");
        }
    }
}
using System.Collections.Generic;
using System.Linq;

namespace CodeConcussion.KVL.Entities
{
    public sealed class User
    {
        public User(string name)
        {
            Name = name;
        }

        public string Name { get; set; }

        private List<Record> _records;
        public List<Record> Records
        {
            get => _records ?? (_records = new List<Record>());
            set => _records = value;
        }

        public bool UpdateRecord(Record record)
        {
            var existing = Records.FirstOrDefault(x => x.Key == record.Key);
            
            if (existing == null)
            {
                Records.Add(record);
                return true;
            }

            if (record.Seconds <= existing.Seconds)
            {
                Records.Remove(existing);
                Records.Add(record);
                return true;
            }

            return false;
        }
    }
}
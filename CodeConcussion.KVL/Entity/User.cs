using System.Collections.Generic;

namespace CodeConcussion.KVL.Entity
{
    internal sealed class User
    {
        public User(string name)
        {
            Name = name;
        }

        public string Name { get; set; }

        private List<Record> _records;
        public List<Record> Records
        {
            get { return _records ?? (_records = new List<Record>()); }
            set { _records = value; }
        }
    }
}
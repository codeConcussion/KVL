using System.Collections.Generic;

namespace CodeConcussion.KVL.Entity
{
    internal sealed class User
    {
        public string Name { get; set; }
        public List<Record> Records { get; set; }
    }
}
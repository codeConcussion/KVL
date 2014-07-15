namespace CodeConcussion.KVL.Entity
{
    internal sealed class Record
    {
        public string Name { get; set; }
        public Operation Operation { get; set; }
        public decimal Seconds { get; set; }
    }
}
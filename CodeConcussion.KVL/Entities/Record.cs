namespace CodeConcussion.KVL.Entities
{
    public sealed class Record
    {
        public string Name { get; set; }
        public Operation Operation { get; set; }
        public decimal Seconds { get; set; }
    }
}
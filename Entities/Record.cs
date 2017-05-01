using CodeConcussion.KVL.Utilities;

namespace CodeConcussion.KVL.Entities
{
    public sealed class Record
    {
        public Record() { } //needed for json serialization

        public Record(Deck deck, decimal seconds)
        {
            if (deck == null) return;
            Key = deck.Key;
            Description = deck.Description;
            Operation = deck.Operation;
            Order = deck.Order;
            Seconds = seconds;
        }

        public string Key { get; set; }
        public string Description { get; set; }
        public Operation Operation { get; set; }
        public int Order { get; set; }
        public decimal Seconds { get; set; }

        public string DisplayTime => Seconds.GetTiming();
    }
}
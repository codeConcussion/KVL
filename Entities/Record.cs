namespace CodeConcussion.KVL.Entities
{
    public sealed class Record
    {
        public Record() { }

        public Record(Deck deck, decimal seconds)
        {
            if (deck == null) return;
            Key = deck.Key;
            Description = deck.Description;
            Operation = deck.Operation;
            Order = deck.Order;
            Seconds = seconds;
        }

        public string Key { get; private set; }
        public string Description { get; private set; }
        public Operation Operation { get; private set; }
        public int Order { get; private set; }
        public decimal Seconds { get; private set; }
    }
}
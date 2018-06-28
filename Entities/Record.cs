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
            GameType = deck.GameType;
            Order = deck.Order;
            Seconds = seconds;
        }

        public string Key { get; set; }
        public string Description { get; set; }
        public GameType GameType { get; set; }
        public int Order { get; set; }
        public decimal Seconds { get; set; }

        public string DisplayTime => Seconds.GetTiming();
    }
}
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

        public string Key { get; set; }
        public string Description { get; set; }
        public Operation Operation { get; set; }
        public int Order { get; set; }
        public decimal Seconds { get; set; }

        public string DisplayTime
        {
            get
            {
                if (Seconds <= 0) return "";
                var minutes = (int)Seconds / 60;
                var seconds = Seconds - (minutes * 60);
                return string.Format("{0:D2}:{1:00.0}", minutes, seconds);
            }
        }
    }
}
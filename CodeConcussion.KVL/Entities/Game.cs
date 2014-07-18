namespace CodeConcussion.KVL.Entities
{
    public sealed class Game
    {
        //public Game(string key, string name, Deck deck)
        //{
        //    Key = key;
        //    Name = name;
        //    Deck = deck;
        //}

        public string Key { get; set; }
        public string Name { get; set; }
        public Deck Deck { get; set; }
    }
}
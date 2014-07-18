using CodeConcussion.KVL.Entities;

namespace CodeConcussion.KVL.Messages
{
    public sealed class StartGame
    {
        public StartGame(Deck deck)
        {
            Deck = deck;
        }

        public Deck Deck { get; set; }
    }
}
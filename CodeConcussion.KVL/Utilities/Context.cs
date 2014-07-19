using System.Collections.Generic;
using CodeConcussion.KVL.Entities;

namespace CodeConcussion.KVL.Utilities
{
    public static class Context
    {
        public static User User { get; set; }
        public static Deck CurrentDeck { get; set; }

        public static List<Deck> Decks
        {
            get { return DeckConfiguration.Decks; }
        }
    }
}
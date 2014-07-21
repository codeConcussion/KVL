using System.Collections.Generic;
using CodeConcussion.KVL.Entities;

namespace CodeConcussion.KVL.Utilities.Game
{
    public static class Context
    {
        public static User User { get; set; }

        public static List<Deck> Decks
        {
            get { return DeckConfiguration.Decks; }
        }
    }
}
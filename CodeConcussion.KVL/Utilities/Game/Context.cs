using System.Collections.Generic;
using CodeConcussion.KVL.Entities;

namespace CodeConcussion.KVL.Utilities.Game
{
    public static class Context
    {
        public static User User { get; set; }
        public static Deck CurrentDeck { get; private set; }
        public static int CurrentCount { get; private set; }

        public static List<Deck> Decks
        {
            get { return DeckConfiguration.Decks; }
        }

        public static void CorrectAnswer()
        {
            CurrentCount++;
        }

        public static void StartGame(Deck deck)
        {
            CurrentDeck = deck;
            CurrentCount = 1;
        }
    }
}
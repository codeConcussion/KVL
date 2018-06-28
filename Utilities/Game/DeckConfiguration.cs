using CodeConcussion.KVL.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace CodeConcussion.KVL.Utilities.Game
{
    internal static class DeckConfiguration
    {
        private static List<Deck> _decks;
        public static List<Deck> Decks => _decks ?? (_decks = CreateDecks());

        private static readonly Dictionary<GameType, Func<Card, Operation>> CardOperationMap = new Dictionary<GameType, Func<Card, Operation>>
        {
            [GameType.Addition] = x => Operation.Addition,
            [GameType.Multiplication] = x => Operation.Multiplication,
            [GameType.SignedNumbers] = x => x.Operation //the operation is stored on each individual card
        };

        private static List<Deck> CreateDecks()
        {
            var settings = ConfigurationManager.AppSettings.AllKeys.Where(x => x.StartsWith("Deck."));

            var decks = settings
                .Select(x => DeserializeDeck(ConfigurationManager.AppSettings[x])).Where(x => x != null)
                .OrderBy(x => x.Order)
                .ToList();

            return decks;
        }

        private static Deck DeserializeDeck(string json)
        {
            try
            {
                var deck = JsonConvert.DeserializeObject<Deck>(json);
                deck.Cards.ForEach(x => x.Operation = CardOperationMap[deck.GameType](x));
                return deck;
            }
            catch
            {
                return null;
            }
        }
    }
}
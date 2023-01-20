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
                return JsonConvert.DeserializeObject<Deck>(json);
            }
            catch
            {
                return null;
            }
        }
    }
}
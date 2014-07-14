﻿using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using CodeConcussion.KVL.Entity;
using Newtonsoft.Json;

namespace CodeConcussion.KVL.Utility
{
    internal static class DeckConfiguration
    {
        private static List<Deck> _decks;
        public static List<Deck> Decks
        {
            get { return _decks ?? (_decks = CreateDecks()); }
        }

        private static List<Deck> CreateDecks()
        {
            var settings = ConfigurationManager.AppSettings.AllKeys.Where(x => x.StartsWith("Deck."));
            var decks = settings.Select(x => DeserializeDeck(ConfigurationManager.AppSettings[x])).Where(x => x != null).ToList();
            return decks;
        }

        private static Deck DeserializeDeck(string json)
        {
            try { return JsonConvert.DeserializeObject<Deck>(json); }
            catch { return null; }
        }
    }
}
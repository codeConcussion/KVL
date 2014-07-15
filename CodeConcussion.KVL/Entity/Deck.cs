using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeConcussion.KVL.Entity
{
    internal sealed class Deck
    {
        public string Key { get; set; }
        public string Description { get; set; }
        public Operation Operation { get; set; }
        public List<Card> Cards { get; set; }

        public void Shuffle()
        {
            var random = new Random();
            Cards = Cards.OrderBy(x => random.Next()).ToList();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeConcussion.KVL.Entities
{
    public sealed class Deck
    {
        public string Key { get; set; }
        public string Description { get; set; }
        public Operation Operation { get; set; }
        public int Order { get; set; }

        public Card CurrentCard { get; set; }
        public List<Card> Cards { get; set; }

        public int CurrentIndex { get { return Cards.IndexOf(CurrentCard); } }
        public bool HasCurrentCard { get { return CurrentCard != null; } }
        public bool IsLastCard { get { return CurrentCard == Cards.Last(); } }

        public Card Deal()
        {
            if (!HasCurrentCard) return (CurrentCard = Cards[0]);
            if (IsLastCard) return (CurrentCard = null);

            var index = Cards.IndexOf(CurrentCard);
            return (CurrentCard = Cards[index + 1]);
        }

        public void Shuffle()
        {
            var random = new Random();
            Cards = Cards.OrderBy(x => random.Next()).ToList();
            CurrentCard = null;
        }
    }
}
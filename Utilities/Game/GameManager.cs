using System;
using System.Collections.Generic;
using CodeConcussion.KVL.Entities;
using CodeConcussion.KVL.Utilities.Messages;

namespace CodeConcussion.KVL.Utilities.Game
{
    public sealed class GameManager
    {
        public GameManager(MessageDispatch dispatcher)
        {
            _dispatcher = dispatcher;
        }

        private readonly MessageDispatch _dispatcher;

        public User User { get; set; }
        public List<Deck> AllDecks { get { return DeckConfiguration.Decks; } }

        public Deck Deck { get; private set; }
        public Card CurrentCard { get; private set; }
        public Card PreviewCard { get; private set; }
        public bool IsPlaying { get; private set; }
        public DateTime? StartedAt { get; private set; }
        public decimal Elapsed { get; private set; }
        public int Progress { get; private set; }
        
        public bool CheckAnswer(string guess)
        {
            var answer = int.Parse("0" + guess.Trim());
            return answer == CurrentCard.Answer;
        }

        public void Deal()
        {
            Progress++;
            if (PreviewCard == null) CurrentCard = null;

            if (CurrentCard != null)
            {
                _dispatcher.PublishMessage(MessageType.DealCard);
                CurrentCard = PreviewCard;
                PreviewCard = Deck.Deal();
            }
            else
            {
                FinishGame();
            }
        }

        public void FinishGame()
        {
            StopGame();

            var seconds = decimal.Round(Elapsed, 1, MidpointRounding.AwayFromZero);
            var record = new Record(Deck, seconds);
            var isNewRecord = User.UpdateRecord(record);
            if (isNewRecord) UserStorage.SaveUser(User);

            var type = isNewRecord ? MessageType.NewRecord : MessageType.NoRecord;
            _dispatcher.PublishMessage(type, record);
        }

        public void StartGame(Deck deck)
        {
            Deck = deck;
            Deck.Shuffle();
            CurrentCard = Deck.Deal();
            PreviewCard = Deck.Deal();

            Elapsed = 0;
            Progress = 1;
            IsPlaying = true;
            StartedAt = DateTime.Now;
            _dispatcher.PublishMessage(MessageType.StartGame);
        }

        public void StopGame()
        {
            if (!IsPlaying) return;
            Elapsed = (decimal)(DateTime.Now - StartedAt.GetValueOrDefault()).TotalSeconds;
            Progress = 0;
            IsPlaying = false;
            StartedAt = null;
            _dispatcher.PublishMessage(MessageType.StopGame);
        }
    }
}
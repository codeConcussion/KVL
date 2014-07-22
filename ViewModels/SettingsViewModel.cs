using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Threading;
using CodeConcussion.KVL.Entities;
using CodeConcussion.KVL.Utilities;
using CodeConcussion.KVL.Utilities.Messages;

namespace CodeConcussion.KVL.ViewModels
{
    public sealed class SettingsViewModel : BaseViewModel
    {
        //icons - https://www.iconfinder.com/iconsets/small-n-flat

        public SettingsViewModel()
        {
            _timer.Interval = TimeSpan.FromMilliseconds(100);
            _timer.Tick += (x, y) => NotifyOfPropertyChange(() => Timing);
        }

        private readonly DispatcherTimer _timer = new DispatcherTimer();
        private Operation _operation = Operation.Addition;
        
        public bool IsAddition
        {
            get { return _operation == Operation.Addition; }
            set
            {
                if (!value) return;
                _operation = Operation.Addition;
                NotifyOfPropertyChange(() => IsAddition);
                NotifyOfPropertyChange(() => IsMultiplication);
                NotifyOfPropertyChange(() => Decks);
            }
        }

        public bool IsMultiplication
        {
            get { return _operation == Operation.Multiplication; }
            set
            {
                if (!value) return;
                _operation = Operation.Multiplication;
                NotifyOfPropertyChange(() => IsAddition);
                NotifyOfPropertyChange(() => IsMultiplication);
                NotifyOfPropertyChange(() => Decks);
            }
        }

        public bool IsPlaying
        {
            get { return GameManager.IsPlaying; }
        }

        private Deck _selectedDeck;
        public Deck SelectedDeck
        {
            get { return _selectedDeck; }
            set
            {
                if (_selectedDeck == value) return;
                _selectedDeck = value;
                NotifyOfPropertyChange(() => SelectedDeck);

                if (GameManager.IsPlaying) StopGame();
            }
        }

        public List<Deck> Decks
        {
            get
            {
                var decks = GameManager.AllDecks.Where(x => x.Operation == _operation).ToList();
                SelectedDeck = decks.FirstOrDefault();
                return decks;
            }
        }

        public string Progress
        {
            get
            {
                if (!GameManager.IsPlaying) return "";
                return string.Format("{0} of {1}", GameManager.Progress, SelectedDeck.Cards.Count);
            }
        }

        public string Timing
        {
            get
            {
                if (!GameManager.IsPlaying) return "";
                var delta = (decimal)(DateTime.Now - GameManager.StartedAt.GetValueOrDefault()).TotalSeconds;
                return delta.GetTiming();
            }
        }

        public void OpenUser()
        {
            PublishMessage(MessageType.OpenUser);
        }

        public void OpenRecords()
        {
            PublishMessage(MessageType.OpenRecords);
        }

        public void StartGame()
        {
            GameManager.StartGame(SelectedDeck);
            _timer.Start();

            NotifyOfPropertyChange(() => IsPlaying);
            NotifyOfPropertyChange(() => Progress);
            NotifyOfPropertyChange(() => Timing);
        }

        public void StopGame()
        {
            GameManager.StopGame();
            _timer.Stop();

            NotifyOfPropertyChange(() => IsPlaying);
            NotifyOfPropertyChange(() => Progress);
            NotifyOfPropertyChange(() => Timing);
        }

        protected override void AddMessageHandlers(Dictionary<MessageType, Action<dynamic>> map)
        {
            map.Add(MessageType.DealCard, x => NotifyOfPropertyChange(() => Progress));
            map.Add(MessageType.OpenRecords, x => StopGame());
            map.Add(MessageType.OpenUser, x => StopGame());
            map.Add(MessageType.StopGame, x => StopGame());
        }
    }
}
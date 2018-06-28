using CodeConcussion.KVL.Entities;
using CodeConcussion.KVL.Utilities;
using CodeConcussion.KVL.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Threading;

namespace CodeConcussion.KVL.ViewModels
{
    public sealed class ControlsViewModel : BaseViewModel
    {
        //icons - https://www.iconfinder.com/iconsets/small-n-flat

        public ControlsViewModel()
        {
            _timer.Interval = TimeSpan.FromMilliseconds(100);
            _timer.Tick += (x, y) => NotifyOfPropertyChange(() => Timing);
        }

        private readonly DispatcherTimer _timer = new DispatcherTimer();
        private GameType _gameType = GameType.Addition;
        
        public bool IsAddition
        {
            get => _gameType == GameType.Addition;
            set
            {
                if (!value) return;
                _gameType = GameType.Addition;
                NotifyOfOperationChange();
            }
        }

        public bool IsMultiplication
        {
            get => _gameType == GameType.Multiplication;
            set
            {
                if (!value) return;
                _gameType = GameType.Multiplication;
                NotifyOfOperationChange();
            }
        }

        public bool IsSignedNumbers
        {
            get => _gameType == GameType.SignedNumbers;
            set
            {
                if (!value) return;
                _gameType = GameType.SignedNumbers;
                NotifyOfOperationChange();
            }
        }

        public bool IsPlaying => GameManager.IsPlaying;

        private Deck _selectedDeck;
        public Deck SelectedDeck
        {
            get => _selectedDeck;
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
                var decks = GameManager.AllDecks.Where(x => x.GameType == _gameType).ToList();
                SelectedDeck = decks.FirstOrDefault();
                return decks;
            }
        }

        public string Progress => !GameManager.IsPlaying ? "" : $"{GameManager.Progress} of {SelectedDeck.Cards.Count}";

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

        public void OpenSettings()
        {
            PublishMessage(MessageType.OpenSettings);
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
            map.Add(MessageType.OpenSettings, x => StopGame());
            map.Add(MessageType.OpenUser, x => StopGame());
            map.Add(MessageType.StopGame, x => StopGame());
        }

        private void NotifyOfOperationChange()
        {
            NotifyOfPropertyChange(() => IsAddition);
            NotifyOfPropertyChange(() => IsMultiplication);
            NotifyOfPropertyChange(() => IsSignedNumbers);
            NotifyOfPropertyChange(() => Decks);
        }
    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Windows;
using System.Windows.Threading;
using CodeConcussion.KVL.Entities;
using CodeConcussion.KVL.Messages;
using CodeConcussion.KVL.Utilities.Game;

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
        private int _progress;
        private DateTime? _started;
        
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

        private Deck _selectedDeck;
        public Deck SelectedDeck
        {
            get { return _selectedDeck; }
            set
            {
                if (_selectedDeck == value) return;
                _selectedDeck = value;
                NotifyOfPropertyChange(() => SelectedDeck);
            }
        }

        public List<Deck> Decks
        {
            get
            {
                var decks = Context.Decks.Where(x => x.Operation == _operation).ToList();
                SelectedDeck = decks.FirstOrDefault();
                return decks;
            }
        }

        public string Progress
        {
            get
            {
                if (SelectedDeck == null) return "";
                if (!_started.HasValue) return "";
                return string.Format("{0} of {1}", _progress, SelectedDeck.Cards.Count);
            }
        }

        public string Timing
        {
            get
            {
                if (!_started.HasValue) return "";
                var delta = DateTime.Now - _started.Value;
                var minutes = delta.Minutes.ToString("D2");
                var seconds = delta.Seconds.ToString("D2");
                var tenths = (delta.Milliseconds / 100).ToString("D1");
                return string.Format("{0}:{1}.{2}", minutes, seconds, tenths);
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
            _progress = 1;
            _started = DateTime.Now;
            _timer.Start();

            PublishMessage(MessageType.StartGame, SelectedDeck);
            NotifyOfPropertyChange(() => Progress);
        }

        public void StopGame()
        {
            _progress = 0;
            _started = null;
            _timer.Stop();

            NotifyOfPropertyChange(() => Progress);
            NotifyOfPropertyChange(() => Timing);
        }

        private void CorrectAnswer(Card card)
        {
            _progress = SelectedDeck.Cards.IndexOf(card) + 2;
            NotifyOfPropertyChange(() => Progress);
        }

        private void FinishGame()
        {
            var time = DateTime.Now - _started.Value;
            StopGame();

            var seconds = decimal.Round((decimal)time.TotalSeconds, 1, MidpointRounding.AwayFromZero);
            var record = new Record(SelectedDeck, seconds);
            var isNewRecord = Context.User.UpdateRecord(record);
            var type = isNewRecord ? MessageType.NewRecord : MessageType.NoRecord;
            PublishMessage(type, record);
        }

        protected override void AddMessageHandlers(Dictionary<MessageType, Action<dynamic>> map)
        {
            map.Add(MessageType.CorrectAnswer, x => CorrectAnswer(x));
            map.Add(MessageType.FinishGame, x => FinishGame());
        }
    }
}
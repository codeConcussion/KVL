using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Threading;
using Caliburn.Micro;
using CodeConcussion.KVL.Entities;
using CodeConcussion.KVL.Messages;
using CodeConcussion.KVL.Utilities;

namespace CodeConcussion.KVL.ViewModels
{
    public sealed class SettingsViewModel : PropertyChangedBase, IHandle<CorrectAnswer>
    {
        //icons - https://www.iconfinder.com/iconsets/small-n-flat

        public SettingsViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _timer.Tick += (x, y) => NotifyOfPropertyChange(() => Timing);
        }

        private readonly IEventAggregator _eventAggregator;
        private readonly DispatcherTimer _timer = new DispatcherTimer();
        private Operation _operation = Operation.Addition;
        private int _progress = 1;
        private DateTime _started;
        
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
            get { return string.Format("{0} of {1}", _progress, SelectedDeck.Cards.Count); }
        }

        public string Timing
        {
            get
            {
                var delta = DateTime.Now - _started;
                var minutes = delta.Minutes.ToString("D2");
                var seconds = delta.Seconds.ToString("D2");
                var tenths = (delta.Milliseconds / 100).ToString("D1");
                return string.Format("{0}:{1}.{2}", minutes, seconds, tenths);
                //return string.Format("{0}:{1}", delta.Minutes.ToString("D2"), delta.Seconds.ToString("D2"));
            }
        }

        public void OpenUser()
        {
            _eventAggregator.Publish(new OpenUser(), x => x());
        }

        public void OpenRecords()
        {
            _eventAggregator.Publish(new OpenRecords(), x => x());
        }

        public void StartGame()
        {
            _eventAggregator.Publish(new StartGame(SelectedDeck), x => x());
            StartTimer();
            UpdateProgress(1);
        }

        public void StopGame()
        {
            //TODO:finish
        }

        private void StartTimer()
        {
            _started = DateTime.Now;
            _timer.Interval = TimeSpan.FromMilliseconds(100);
            _timer.Start();
        }

        private void UpdateProgress(int progress)
        {
            _progress = progress;
            NotifyOfPropertyChange(() => Progress);
        }

        public void Handle(CorrectAnswer message) { UpdateProgress(_progress + 1); }
    }
}
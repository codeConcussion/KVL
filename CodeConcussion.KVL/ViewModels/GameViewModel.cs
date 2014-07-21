using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Forms.VisualStyles;
using System.Windows.Input;
using CodeConcussion.KVL.Entities;
using CodeConcussion.KVL.Messages;
using CodeConcussion.KVL.Utilities.Game;
using Control = System.Windows.Controls.Control;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;

namespace CodeConcussion.KVL.ViewModels
{
    public sealed class GameViewModel : BaseViewModel
    {
        public GameViewModel(CardViewModel currentCardViewModel, CardViewModel previewCardViewModel)
        {
            CurrentCardView = currentCardViewModel;
            PreviewCardView = previewCardViewModel;
            IsAnswerWrong = false;
            EventManager.RegisterClassHandler(typeof(Control), UIElement.KeyDownEvent, new RoutedEventHandler(KeyDown));
        }

        public CardViewModel CurrentCardView { get; private set; }
        public CardViewModel PreviewCardView { get; private set; }
        public Deck Deck { get; set; }

        private bool _hasCurrentCard;
        public bool HasCurrentCard
        {
            get { return _hasCurrentCard; }
            set
            {
                if (_hasCurrentCard == value) return;
                _hasCurrentCard = value;
                NotifyOfPropertyChange(() => HasCurrentCard);
            }
        }

        private bool _hasPreviousCard;
        public bool HasPreviewCard
        {
            get { return _hasPreviousCard; }
            set
            {
                if (_hasPreviousCard == value) return;
                _hasPreviousCard = value;
                NotifyOfPropertyChange(() => HasPreviewCard);
            }
        }

        private bool _isAnswerWrong;
        public bool IsAnswerWrong
        {
            get { return _isAnswerWrong; }
            set
            {
                _isAnswerWrong = value;
                NotifyOfPropertyChange(() => IsAnswerWrong);
            }
        }

        private void Start(Deck deck)
        {
            Deck = deck;
            Deck.Shuffle();
            CurrentCardView.Card = Deck.Deal();
            PreviewCardView.Card = Deck.Deal();
            HasCurrentCard = HasPreviewCard = true;
        }

        private void AddDigit(int digit)
        {
            CurrentCardView.AddDigit(digit);
        }

        private void Answer()
        {
            IsAnswerWrong = false;
            IsAnswerWrong = !CurrentCardView.IsCorrect;
            Clear();
            if (IsAnswerWrong) return;

            HasCurrentCard = HasPreviewCard;
            HasPreviewCard = !Deck.IsLastCard && HasCurrentCard;
            var isFinished = !HasCurrentCard;
            if (isFinished)
            {
                PublishMessage(MessageType.FinishGame);
            }
            else
            {
                PublishMessage(MessageType.CorrectAnswer, CurrentCardView.Card);
                CurrentCardView.Card = PreviewCardView.Card;
                PreviewCardView.Card = Deck.Deal();
            }
        }

        private void Clear()
        {
            CurrentCardView.Answer = "";
        }

        private void Delete()
        {
            CurrentCardView.RemoveDigit();
        }

        private void KeyDown(object sender, RoutedEventArgs e)
        {
            var args = e as KeyEventArgs;
            if (args == null) return;

            args.Handled = true;
            var isAction = KeyMap.ContainsKey(args.Key);
            if (isAction) KeyMap[args.Key](this);
        }

        protected override void AddMessageHandlers(Dictionary<MessageType, Action<dynamic>> map)
        {
            map.Add(MessageType.StartGame, x => Start(x));
        }

        #region Key Map

        private static readonly Dictionary<Key, Action<GameViewModel>> KeyMap = new Dictionary<Key, Action<GameViewModel>>
        {
            { Key.Escape, x => x.Clear() },
            { Key.Back, x => x.Delete() },
            { Key.Delete, x => x.Delete() },
            { Key.Decimal, x => x.Delete() },
            { Key.Enter, x => x.Answer() },
            { Key.D0, x => x.AddDigit(0) },
            { Key.D1, x => x.AddDigit(1) },
            { Key.D2, x => x.AddDigit(2) },
            { Key.D3, x => x.AddDigit(3) },
            { Key.D4, x => x.AddDigit(4) },
            { Key.D5, x => x.AddDigit(5) },
            { Key.D6, x => x.AddDigit(6) },
            { Key.D7, x => x.AddDigit(7) },
            { Key.D8, x => x.AddDigit(8) },
            { Key.D9, x => x.AddDigit(9) },
            { Key.NumPad0, x => x.AddDigit(0) },
            { Key.NumPad1, x => x.AddDigit(1) },
            { Key.NumPad2, x => x.AddDigit(2) },
            { Key.NumPad3, x => x.AddDigit(3) },
            { Key.NumPad4, x => x.AddDigit(4) },
            { Key.NumPad5, x => x.AddDigit(5) },
            { Key.NumPad6, x => x.AddDigit(6) },
            { Key.NumPad7, x => x.AddDigit(7) },
            { Key.NumPad8, x => x.AddDigit(8) },
            { Key.NumPad9, x => x.AddDigit(9) }
        };

        #endregion
    }
}
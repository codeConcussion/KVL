using CodeConcussion.KVL.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
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

        public CardViewModel CurrentCardView { get; }
        public CardViewModel PreviewCardView { get; }
        public Brush BackgroundColor => new SolidColorBrush(GameManager.BackgroundColor);

        private bool _hasCurrentCard;
        public bool HasCurrentCard
        {
            get => _hasCurrentCard;
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
            get => _hasPreviousCard;
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
            get => _isAnswerWrong;
            set
            {
                //toggle flag for error animation
                if (value && IsAnswerWrong) IsAnswerWrong = false;
                _isAnswerWrong = value;
                NotifyOfPropertyChange(() => IsAnswerWrong);
            }
        }

        private void AddDigit(int digit)
        {
            CurrentCardView.AddDigit(digit);
        }

        private void Answer()
        {
            IsAnswerWrong = !GameManager.CheckAnswer(CurrentCardView.Answer);
            Clear();

            if (IsAnswerWrong)
            {
                PlayErrorSound();
            }
            else
            {
                Deal();
            }
        }

        private void Clear()
        {
            CurrentCardView.Answer = "";
        }

        private void Deal()
        {
            GameManager.Deal();
            CurrentCardView.Card = GameManager.CurrentCard;
            PreviewCardView.Card = GameManager.PreviewCard;
            HasCurrentCard = GameManager.CurrentCard != null;
            HasPreviewCard = GameManager.PreviewCard != null;
        }

        private void Delete()
        {
            CurrentCardView.RemoveDigit();
        }

        private void KeyDown(object sender, RoutedEventArgs e)
        {
            if (!(e is KeyEventArgs args) || !HasCurrentCard) return;

            args.Handled = true;
            var isAction = KeyMap.ContainsKey(args.Key);
            if (isAction) KeyMap[args.Key](this);
        }

        private void PlayErrorSound()
        {
            if (GameManager.PlayErrorSound) System.Media.SystemSounds.Exclamation.Play();
        }

        private void StartGame()
        {
            HasCurrentCard = HasPreviewCard = true;
            CurrentCardView.Card = GameManager.CurrentCard;
            PreviewCardView.Card = GameManager.PreviewCard;
        }

        private void ToggleSign()
        {
            CurrentCardView.ToggleSign();
        }

        protected override void AddMessageHandlers(Dictionary<MessageType, Action<dynamic>> map)
        {
            map.Add(MessageType.StartGame, x => StartGame());
            map.Add(MessageType.StopGame, x => HasCurrentCard = HasPreviewCard = false);
            map.Add(MessageType.CloseSettings, x => NotifyOfPropertyChange(() => BackgroundColor));
        }

        #region Key Map

        private static readonly Dictionary<Key, Action<GameViewModel>> KeyMap = new Dictionary<Key, Action<GameViewModel>>
        {
            { Key.Escape, x => x.Clear() },
            { Key.Back, x => x.Delete() },
            { Key.Delete, x => x.Delete() },
            { Key.Decimal, x => x.Delete() },
            { Key.Enter, x => x.Answer() },
            { Key.Subtract, x => x.ToggleSign() },
            { Key.OemMinus, x => x.ToggleSign() },
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
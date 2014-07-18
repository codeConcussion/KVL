using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using Caliburn.Micro;
using CodeConcussion.KVL.Entity;
using Control = System.Windows.Controls.Control;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;


namespace CodeConcussion.KVL.ViewModels
{
    public sealed class GameViewModel : PropertyChangedBase
    {
        public GameViewModel(CardViewModel currentCardViewModel, CardViewModel previewCardViewModel)
        {
            CurrentCardView = currentCardViewModel;
            PreviewCardView = previewCardViewModel;
            EventManager.RegisterClassHandler(typeof(Control), UIElement.KeyDownEvent, new RoutedEventHandler(KeyDown));
        }

        public Game Game { get; set; }
        public CardViewModel CurrentCardView { get; private set; }
        public CardViewModel PreviewCardView { get; private set; }

        public bool IsAnswerWrong
        {
            get { return !CurrentCardView.IsCorrect; }
        }

        public void Answer()
        {
            if (CurrentCardView.IsCorrect)
            {
                CurrentCardView.Card = PreviewCardView.Card;
                PreviewCardView.Card = Game.Deck.Deal();
            }
            else
            {
                //TODO:error animation
            }

            NotifyOfPropertyChange(() => IsAnswerWrong);
            Clear();
        }

        private void AddDigit(int digit)
        {
            CurrentCardView.AddDigit(digit);
        }

        private void Clear()
        {
            CurrentCardView.Answer = "";
        }

        private void Delete()
        {
            CurrentCardView.RemoveDigit();
        }

        public void Start()
        {
            Game.Deck.Shuffle();
            CurrentCardView.Card = Game.Deck.Deal();
            PreviewCardView.Card = Game.Deck.Deal();
        }

        private void KeyDown(object sender, RoutedEventArgs e)
        {
            var args = e as KeyEventArgs;
            if (args == null) return;

            args.Handled = true;
            var isAction = ActionMap.ContainsKey(args.Key);
            if (isAction) ActionMap[args.Key](this);
        }

        #region Action Map

        private static readonly Dictionary<Key, Action<GameViewModel>> ActionMap = new Dictionary<Key, Action<GameViewModel>>
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
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Forms.VisualStyles;
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

        public CardViewModel CurrentCardView { get; private set; }
        public CardViewModel PreviewCardView { get; private set; }

        public Game Game { get; set; }
        
        public void Answer()
        {
            //if (CurrentCardView.IsCorrect)
            if (true)
            {
                CurrentCardView.Card = PreviewCardView.Card;
                PreviewCardView.Card = Game.Deck.Deal();
                Clear();
            }
        }

        public void Clear()
        {
            CurrentCardView.EnteredAnswer = "";
        }

        public void Delete()
        {

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
            var key = args.Key;

            var isAction = ActionMap.ContainsKey(key);
            if (isAction)
            {
                var action = ActionMap[key];
            }

            var isDigit = DigitMap.ContainsKey(key);
            if (isDigit) CurrentCardView.EnteredAnswer += DigitMap[key];

            //if (isNumber) Testing += DigitConverter.ConvertToString(args.Key);
        }

        #region Maps

        private enum GameAction
        {
            Clear,
            Delete,
            Answer
        }
        
        private static readonly Dictionary<Key, GameAction> ActionMap = new Dictionary<Key, GameAction>
        {
            { Key.Escape, GameAction.Clear },
            { Key.Back, GameAction.Delete },
            { Key.Delete, GameAction.Delete },
            { Key.Enter, GameAction.Answer },
            //{ Key.Return, GameAction.Answer }
        };

        private static readonly Dictionary<GameAction, Action<GameViewModel>> Foo = new Dictionary<GameAction, Action<GameViewModel>>
        {
            { GameAction.Clear, x => x.Clear() },
            { GameAction.Delete, x => x.Delete() },
            { GameAction.Answer, x => x.Answer() }
        };

        private static readonly Dictionary<Key, int> DigitMap = new Dictionary<Key, int>
        {
            { Key.D0, 0 },
            { Key.D1, 1 },
            { Key.D2, 2 },
            { Key.D3, 3 },
            { Key.D4, 4 },
            { Key.D5, 5 },
            { Key.D6, 6 },
            { Key.D7, 7 },
            { Key.D8, 8 },
            { Key.D9, 9 },
            { Key.NumPad0, 0 },
            { Key.NumPad1, 1 },
            { Key.NumPad2, 2 },
            { Key.NumPad3, 3 },
            { Key.NumPad4, 4 },
            { Key.NumPad5, 5 },
            { Key.NumPad6, 6 },
            { Key.NumPad7, 7 },
            { Key.NumPad8, 8 },
            { Key.NumPad9, 9 }
        };

        #endregion
    }
}
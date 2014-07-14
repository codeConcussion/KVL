using Caliburn.Micro;
using CodeConcussion.KVL.Utility;

namespace CodeConcussion.KVL.ViewModels
{
    public sealed class ShellViewModel : PropertyChangedBase
    {
        public ShellViewModel(GameViewModel gameViewModel, IdentityViewModel identityViewModel)
        {
            GameViewModel = gameViewModel;
            IdentityViewModel = identityViewModel;
            var decks = DeckConfiguration.Decks;
        }

        public GameViewModel GameViewModel { get; private set; }
        public IdentityViewModel IdentityViewModel { get; private set; }

        private int _height;
        public int Height
        {
            get { return _height; }
            set
            {
                _height = value;
                MinWidth = value * 2;
            }
        }

        private int _minWidth;
        public int MinWidth
        {
            get { return _minWidth; }
            set
            {
                if (_minWidth == value) return;
                _minWidth = value;
                NotifyOfPropertyChange(() => MinWidth);
            }
        }
    }
}

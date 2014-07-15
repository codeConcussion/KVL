using Caliburn.Micro;
using CodeConcussion.KVL.Messages;
using CodeConcussion.KVL.Utility;

namespace CodeConcussion.KVL.ViewModels
{
    public sealed class ShellViewModel : PropertyChangedBase, IHandle<OpenUser>, IHandle<CloseUser>
    {
        public ShellViewModel(
            GameViewModel gameViewModel,
            SettingsViewModel settingsViewModel,
            UserViewModel userViewModel)
        {
            GameViewModel = gameViewModel;
            SettingsViewModel = settingsViewModel;
            UserViewModel = userViewModel;
            var decks = DeckConfiguration.Decks;
        }

        public GameViewModel GameViewModel { get; private set; }
        public SettingsViewModel SettingsViewModel { get; private set; }
        public UserViewModel UserViewModel { get; private set; }

        private bool _isUserActive;
        public bool IsUserActive
        {
            get { return _isUserActive; }
            private set
            {
                if (_isUserActive == value) return;
                _isUserActive = value;
                NotifyOfPropertyChange(() => IsUserActive);
            }
        }

        public void Handle(CloseUser message) { IsUserActive = false; }
        public void Handle(OpenUser message) { IsUserActive = true; }
    }
}
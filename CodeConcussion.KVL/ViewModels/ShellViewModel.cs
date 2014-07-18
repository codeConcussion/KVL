using System.Data.Odbc;
using Caliburn.Micro;
using CodeConcussion.KVL.Messages;
using CodeConcussion.KVL.Utilities;

namespace CodeConcussion.KVL.ViewModels
{
    public sealed class ShellViewModel :
        PropertyChangedBase,
        IHandle<OpenRecords>,
        IHandle<CloseRecords>,
        IHandle<OpenUser>,
        IHandle<CloseUser>,
        IHandle<StartGame>
    {
        public ShellViewModel(
            GameViewModel gameViewModel,
            SettingsViewModel settingsViewModel,
            UserViewModel userViewModel,
            RecordsViewModel recordsViewModel)
        {
            GameViewModel = gameViewModel;
            SettingsViewModel = settingsViewModel;
            RecordsViewModel = recordsViewModel;
            UserViewModel = userViewModel;
            var decks = DeckConfiguration.Decks;

            var user = UserStorage.LoadUser("toby");
            //var user = new User("Toby");
            //user.Records.Add(new Record {Name = "One-Away", Operation = Operation.Addition, Seconds = 10m});
            //user.Records.Add(new Record {Name = "Whole Deck", Operation = Operation.Addition, Seconds = 20m});
            //UserStorage.SaveUser(user);
        }

        public GameViewModel GameViewModel { get; private set; }
        public SettingsViewModel SettingsViewModel { get; private set; }
        public RecordsViewModel RecordsViewModel { get; private set; }
        public UserViewModel UserViewModel { get; private set; }

        #region Game

        public void Handle(StartGame message)
        {
            GameViewModel.Game = message.Game;
            GameViewModel.Start();
        }

        #endregion

        #region Records

        private bool _isRecordsActive;
        public bool IsRecordsActive
        {
            get { return _isRecordsActive; }
            private set
            {
                if (_isRecordsActive == value) return;
                _isRecordsActive = value;
                NotifyOfPropertyChange(() => IsRecordsActive);
            }
        }

        public void Handle(OpenRecords message) { IsRecordsActive = true; }
        public void Handle(CloseRecords message) { IsRecordsActive = false; }

        #endregion

        #region User

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

        public void Handle(OpenUser message) { IsUserActive = true; }
        public void Handle(CloseUser message) { IsUserActive = false; }

        #endregion
    }
}
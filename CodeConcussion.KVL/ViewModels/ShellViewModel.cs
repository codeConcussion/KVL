using System.Collections.Generic;
using Caliburn.Micro;
using CodeConcussion.KVL.Messages;
using CodeConcussion.KVL.Utilities.Game;

namespace CodeConcussion.KVL.ViewModels
{
    public sealed class ShellViewModel : BaseViewModel, IHaveDisplayName
    {
        public ShellViewModel(
            GameViewModel gameViewModel,
            SettingsViewModel settingsViewModel,
            RecordsViewModel recordsViewModel,
            UserViewModel userViewModel)
        {
            DisplayName = "KVL";
            GameViewModel = gameViewModel;
            SettingsViewModel = settingsViewModel;
            RecordsViewModel = recordsViewModel;
            UserViewModel = userViewModel;
            Context.User = UserStorage.LoadUser("toby");
        }

        public string DisplayName { get; set; }
        public GameViewModel GameViewModel { get; private set; }
        public SettingsViewModel SettingsViewModel { get; private set; }
        public RecordsViewModel RecordsViewModel { get; private set; }
        public UserViewModel UserViewModel { get; private set; }

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

        protected override void AddMessageHandlers(Dictionary<MessageType, System.Action<dynamic>> map)
        {
            map.Add(MessageType.CloseRecords, x => IsRecordsActive = false);
            map.Add(MessageType.OpenRecords, x => IsRecordsActive = true);
            map.Add(MessageType.CloseUser, x => IsUserActive = false);
            map.Add(MessageType.OpenUser, x => IsUserActive = true);
        }
    }
}
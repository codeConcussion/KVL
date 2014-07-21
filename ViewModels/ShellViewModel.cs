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
            MessageViewModel messageViewModel,
            RecordsViewModel recordsViewModel,
            SettingsViewModel settingsViewModel,
            UserViewModel userViewModel)
        {
            GameViewModel = gameViewModel;
            MessageViewModel = messageViewModel;
            RecordsViewModel = recordsViewModel;
            SettingsViewModel = settingsViewModel;
            UserViewModel = userViewModel;
            
            SetTitle();
            IsUserActive = true;
        }

        public string DisplayName { get; set; }
        public GameViewModel GameViewModel { get; private set; }
        public MessageViewModel MessageViewModel { get; private set; }
        public RecordsViewModel RecordsViewModel { get; private set; }
        public SettingsViewModel SettingsViewModel { get; private set; }
        public UserViewModel UserViewModel { get; private set; }

        private bool _isMessageActive;
        public bool IsMessageActive
        {
            get { return _isMessageActive; }
            private set
            {
                if (_isMessageActive == value) return;
                _isMessageActive = value;
                NotifyOfPropertyChange(() => IsMessageActive);
            }
        }

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
                SetTitle();
                NotifyOfPropertyChange(() => IsUserActive);
            }
        }

        protected override void AddMessageHandlers(Dictionary<MessageType, System.Action<dynamic>> map)
        {
            map.Add(MessageType.CloseMessage, x => IsMessageActive = false);
            map.Add(MessageType.CloseRecords, x => IsRecordsActive = false);
            map.Add(MessageType.CloseUser, x => IsUserActive = false);
            map.Add(MessageType.NewRecord, x => IsMessageActive = true);
            map.Add(MessageType.NoRecord, x => IsMessageActive = true);
            map.Add(MessageType.OpenRecords, x => IsRecordsActive = true);
            map.Add(MessageType.OpenUser, x => IsUserActive = true);
        }

        private void SetTitle()
        {
            DisplayName = Context.User == null ? "KVL" : "KVL :: " + Context.User.Name;
            NotifyOfPropertyChange(() => DisplayName);
        }
    }
}
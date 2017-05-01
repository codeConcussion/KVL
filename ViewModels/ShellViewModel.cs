using System.Collections.Generic;
using Caliburn.Micro;
using CodeConcussion.KVL.Utilities.Messages;

namespace CodeConcussion.KVL.ViewModels
{
    public sealed class ShellViewModel : BaseViewModel, IHaveDisplayName
    {
        public ShellViewModel(
            GameViewModel gameView,
            ControlsViewModel controlsView,
            MessageViewModel messageView,
            RecordsViewModel recordsView,
            SettingsViewModel settingsView,
            UserViewModel userView)
        {
            GameView = gameView;
            ControlsView = controlsView;
            MessageView = messageView;
            RecordsView = recordsView;
            SettingsView = settingsView;
            UserView = userView;

            SetTitle();
            IsUserActive = true;
        }

        public string DisplayName { get; set; }
        public GameViewModel GameView { get; }
        public ControlsViewModel ControlsView { get; }
        public MessageViewModel MessageView { get; }
        public RecordsViewModel RecordsView { get; }
        public SettingsViewModel SettingsView { get; }
        public UserViewModel UserView { get; }

        private bool _isMessageActive;
        public bool IsMessageActive
        {
            get => _isMessageActive;
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
            get => _isRecordsActive;
            private set
            {
                if (_isRecordsActive == value) return;
                _isRecordsActive = value;
                NotifyOfPropertyChange(() => IsRecordsActive);
            }
        }

        private bool _isSettingsActive;
        public bool IsSettingsActive
        {
            get => _isSettingsActive;
            private set
            {
                if (_isSettingsActive == value) return;
                _isSettingsActive = value;
                NotifyOfPropertyChange(() => IsSettingsActive);
            }
        }

        private bool _isUserActive;
        public bool IsUserActive
        {
            get => _isUserActive;
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
            map.Add(MessageType.CloseSettings, x => IsSettingsActive = false);
            map.Add(MessageType.CloseUser, x => IsUserActive = false);
            map.Add(MessageType.NewRecord, x => IsMessageActive = true);
            map.Add(MessageType.NoRecord, x => IsMessageActive = true);
            map.Add(MessageType.OpenRecords, x => IsRecordsActive = true);
            map.Add(MessageType.OpenSettings, x => IsSettingsActive = true);
            map.Add(MessageType.OpenUser, x => IsUserActive = true);
        }

        private void SetTitle()
        {
            DisplayName = GameManager.User == null ? "KVL" : "KVL :: " + GameManager.User.Name;
            NotifyOfPropertyChange(() => DisplayName);
        }
    }
}
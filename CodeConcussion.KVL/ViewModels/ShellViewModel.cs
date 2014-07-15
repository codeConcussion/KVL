﻿using Caliburn.Micro;
using CodeConcussion.KVL.Messages;
using CodeConcussion.KVL.Utility;

namespace CodeConcussion.KVL.ViewModels
{
    public sealed class ShellViewModel : PropertyChangedBase, IHandle<OpenRecords>, IHandle<CloseRecords>, IHandle<OpenUser>, IHandle<CloseUser>
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
        }

        public GameViewModel GameViewModel { get; private set; }
        public SettingsViewModel SettingsViewModel { get; private set; }
        public RecordsViewModel RecordsViewModel { get; private set; }
        public UserViewModel UserViewModel { get; private set; }

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
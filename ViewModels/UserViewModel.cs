using System;
using System.Collections.Generic;
using CodeConcussion.KVL.Utilities.Game;
using CodeConcussion.KVL.Utilities.Messages;

namespace CodeConcussion.KVL.ViewModels
{
    public sealed class UserViewModel : BaseViewModel
    {
        private bool _isUserFocused = true;
        public bool IsUserFocused
        {
            get { return _isUserFocused; }
            set
            {
                _isUserFocused = value;
                NotifyOfPropertyChange(() => IsUserFocused);
            }
        }

        private string _user;
        public string User
        {
            get { return _user; }
            set
            {
                if (_user == value) return;
                _user = value;
                NotifyOfPropertyChange(() => User);
            }
        }

        public void Close()
        {
            if (GameManager.User == null) return;
            User = GameManager.User.Name;
            PublishMessage(MessageType.CloseUser);
        }

        public void OK()
        {
            if (string.IsNullOrWhiteSpace(User)) return;
            GameManager.User = UserStorage.LoadUser(User);
            PublishMessage(MessageType.CloseUser);
        }

        protected override void AddMessageHandlers(Dictionary<MessageType, Action<dynamic>> map)
        {
            map.Add(MessageType.OpenUser, x => IsUserFocused = true);
        }
    }
}
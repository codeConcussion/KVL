using System;
using System.Collections.Generic;
using CodeConcussion.KVL.Utilities.Game;
using CodeConcussion.KVL.Utilities.Messages;

namespace CodeConcussion.KVL.ViewModels
{
    public sealed class UserViewModel : BaseViewModel
    {
        public string User { get; set; }

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

        public void Close()
        {
            if (string.IsNullOrWhiteSpace(User)) return;

            Context.User = UserStorage.LoadUser(User);
            PublishMessage(MessageType.CloseUser);
        }

        protected override void AddMessageHandlers(Dictionary<MessageType, Action<dynamic>> map)
        {
            map.Add(MessageType.OpenUser, x => IsUserFocused = true);
        }
    }
}
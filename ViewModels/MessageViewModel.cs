using System;
using System.Collections.Generic;
using CodeConcussion.KVL.Entities;
using CodeConcussion.KVL.Messages;
using CodeConcussion.KVL.Utilities.Game;

namespace CodeConcussion.KVL.ViewModels
{
    public sealed class MessageViewModel : BaseViewModel
    {
        public void Close()
        {
            PublishMessage(MessageType.CloseMessage);
        }

        private string _message;
        public string Message
        {
            get { return _message ?? ""; }
            set
            {
                if (_message == value) return;
                _message = value;
                NotifyOfPropertyChange(() => Message);
            }
        }

        private void NewRecord(Record record)
        {
            Message = string.Format(MessageConfiguration.Messages[GameMessage.NewRecord], Context.User.Name, record.Description, FormatTime(record.Seconds));
        }

        private void NoRecord(Record record)
        {
            Message = string.Format(MessageConfiguration.Messages[GameMessage.NoRecord], Context.User.Name, record.Description, FormatTime(record.Seconds));
        }

        private string FormatTime(decimal time)
        {
            var minutes = (int)time / 60;
            var seconds = time - (minutes * 60);
            return string.Format("{0:D2}:{1:00.0}", minutes, seconds);
        }

        protected override void AddMessageHandlers(Dictionary<MessageType, Action<dynamic>> map)
        {
            map.Add(MessageType.NewRecord, x => NewRecord(x));
            map.Add(MessageType.NoRecord, x => NoRecord(x));
        }
    }
}
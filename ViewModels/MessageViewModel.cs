﻿using System;
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

        private bool _isNewRecord;
        public bool IsNewRecord
        {
            get { return _isNewRecord; }
            set
            {
                if (_isNewRecord == value) return;
                _isNewRecord = value;
                NotifyOfPropertyChange(() => IsNewRecord);
            }
        }
        
        private void NewRecord(Record record)
        {
            IsNewRecord = true;
            Message = string.Format(MessageConfiguration.Messages[GameMessage.NewRecord], Context.User.Name, record.Description, record.DisplayTime);
        }

        private void NoRecord(Record record)
        {
            IsNewRecord = false;
            Message = string.Format(MessageConfiguration.Messages[GameMessage.NoRecord], Context.User.Name, record.Description, record.DisplayTime);
        }

        protected override void AddMessageHandlers(Dictionary<MessageType, Action<dynamic>> map)
        {
            map.Add(MessageType.NewRecord, x => NewRecord(x));
            map.Add(MessageType.NoRecord, x => NoRecord(x));
        }
    }
}
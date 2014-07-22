using System;
using System.Collections.Generic;
using System.Linq;
using CodeConcussion.KVL.Entities;
using CodeConcussion.KVL.Utilities.Game;
using CodeConcussion.KVL.Utilities.Messages;

namespace CodeConcussion.KVL.ViewModels
{
    public sealed class RecordsViewModel : BaseViewModel
    {
        public List<Record> AdditionRecords
        {
            get { return GetRecords(Operation.Addition); }
        }

        public List<Record> MultiplicationRecords
        {
            get { return GetRecords(Operation.Multiplication); }
        }

        public void Close()
        {
            PublishMessage(MessageType.CloseRecords);
        }

        private void Open()
        {
            NotifyOfPropertyChange(() => AdditionRecords);
            NotifyOfPropertyChange(() => MultiplicationRecords);
        }

        private static List<Record> GetRecords(Operation operation)
        {
            if (Context.User == null) return new List<Record>();

            var allRecords = Context.Decks
                .Where(x => x.Operation == operation)
                .Select(x => new Record(x, 0m))
                .ToList();

            var userRecords = Context.User.Records
                .Where(x => x.Operation == operation)
                .OrderBy(x => x.Order)
                .ToList();

            foreach (var record in userRecords)
            {
                var all = allRecords.FirstOrDefault(x => x.Key == record.Key);
                if (all != null) all.Seconds = record.Seconds;
            }

            return allRecords;
        }

        protected override void AddMessageHandlers(Dictionary<MessageType, Action<dynamic>> map)
        {
            map.Add(MessageType.OpenRecords, x => Open());
        }
    }
}
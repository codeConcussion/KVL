using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using CodeConcussion.KVL.Entities;
using CodeConcussion.KVL.Utilities.Messages;

namespace CodeConcussion.KVL.ViewModels
{
    public sealed class RecordsViewModel : BaseViewModel
    {
        public List<Record> AdditionRecords => GetRecords(Operation.Addition);
        public List<Record> MultiplicationRecords => GetRecords(Operation.Multiplication);

        public void Close()
        {
            PublishMessage(MessageType.CloseRecords);
        }

        public void Print(FrameworkElement source)
        {
            var parent = VisualTreeHelper.GetParent(source);;
            for(;;)
            {
                parent = VisualTreeHelper.GetParent(parent);
                if (parent is UserControl) break;
                if (parent == null) return;
            }

            var dialog = new PrintDialog();
            var print = dialog.ShowDialog();
            if (print.GetValueOrDefault()) dialog.PrintVisual((Visual)parent, "KVL Records :: " + GameManager.User.Name);
        }

        private void Open()
        {
            NotifyOfPropertyChange(() => AdditionRecords);
            NotifyOfPropertyChange(() => MultiplicationRecords);
        }

        private List<Record> GetRecords(Operation operation)
        {
            if (GameManager.User == null) return new List<Record>();

            var allRecords = GameManager.AllDecks
                .Where(x => x.Operation == operation)
                .Select(x => new Record(x, 0m))
                .ToList();

            var userRecords = GameManager.User.Records
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
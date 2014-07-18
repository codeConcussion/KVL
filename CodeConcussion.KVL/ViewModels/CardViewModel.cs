using System.Globalization;
using Caliburn.Micro;
using CodeConcussion.KVL.Entity;
using CodeConcussion.KVL.Utility;

namespace CodeConcussion.KVL.ViewModels
{
    public sealed class CardViewModel : PropertyChangedBase
    {
        private Card _card;
        public Card Card
        {
            get { return _card ?? (_card = new Card()); }
            set
            {
                _card = value;
                NotifyOfPropertyChange(() => Card);
                NotifyOfPropertyChange(() => FirstLine);
                NotifyOfPropertyChange(() => SecondLine);
                Answer = "";
            }
        }

        public string FirstLine
        {
            get { return Card.FirstNumber.ToString(CultureInfo.InvariantCulture).PadLeft(2); }
        }

        public string SecondLine
        {
            get { return Card.Operation.GetSign() + Card.SecondNumber.ToString(CultureInfo.InvariantCulture).PadLeft(2); }
        }

        private string _answer;
        public string Answer
        {
            get { return _answer ?? (_answer = ""); }
            set
            {
                if (_answer == value) return;
                _answer = value;
                NotifyOfPropertyChange(() => Answer);
            }
        }

        public bool IsCorrect
        {
            get
            {
                var answer = int.Parse("0" + Answer.Trim());
                return answer == Card.Answer;
            }
        }

        public void AddDigit(int digit)
        {
            var added = (Answer + digit).PadLeft(3);
            Answer = added.Substring(added.Length - 3, 3);
        }

        public void RemoveDigit()
        {
            var length = Answer.Length;
            Answer = length <= 1 ? "" : Answer.Substring(0, length - 1);
        }
    }
}
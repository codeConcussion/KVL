using System.Globalization;
using System.Linq;
using CodeConcussion.KVL.Entities;
using CodeConcussion.KVL.Utilities;

namespace CodeConcussion.KVL.ViewModels
{
    public sealed class CardViewModel : BaseViewModel
    {
        private Card _card;
        public Card Card
        {
            get => _card ?? (_card = new Card());
            set
            {
                _card = value;
                NotifyOfPropertyChange(() => Card);
                NotifyOfPropertyChange(() => FirstLine);
                NotifyOfPropertyChange(() => SecondLine);
                Answer = "";
            }
        }

        public string FirstLine => Card.FirstNumber.ToString(CultureInfo.InvariantCulture).PadLeft(2);

        public string SecondLine => Card.Operation.GetSign() + Card.SecondNumber.ToString(CultureInfo.InvariantCulture).PadLeft(2);

        private string _answer;
        public string Answer
        {
            get => _answer ?? (_answer = "");
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
            var answer = Answer + digit;
            if (answer.Length == 4) answer = answer.Remove(0, 1);
            if (answer.All(x => x == '0')) answer = "0";
            if (answer.Length > 1) answer = answer.TrimStart('0');
            Answer = answer;
        }

        public void RemoveDigit()
        {
            var length = Answer.Length;
            Answer = length <= 1 ? "" : Answer.Substring(0, length - 1);
        }
    }
}
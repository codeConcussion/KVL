using CodeConcussion.KVL.Entities;
using CodeConcussion.KVL.Utilities;
using System.Globalization;
using System.Linq;

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
                NotifyOfPropertyChange(() => FirstNumber);
                NotifyOfPropertyChange(() => SecondNumber);
                Answer = "";
            }
        }

        public string FirstNumber => Card.FirstNumber.ToString(CultureInfo.InvariantCulture).PadLeft(2);
        public string SecondNumber => Card.Operation.GetSign() + Card.SecondNumber.ToString(CultureInfo.InvariantCulture).PadLeft(2);

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

        public void AddDigit(int digit)
        {
            var answer = Answer + digit;
            var isNegative = answer.StartsWith("-");

            if (isNegative) answer = answer.Remove(0, 1);
            if (answer.Length == 4) answer = answer.Remove(0, 1);
            if (answer.All(x => x == '0')) answer = "0";
            if (answer.Length > 1) answer = answer.TrimStart('0');
            if (isNegative) answer = $"-{answer}";

            Answer = answer;
        }

        public void RemoveDigit()
        {
            Answer = Answer.Length <= 1 ? "" : Answer.Substring(0, Answer.Length - 1);
        }

        public void ToggleSign()
        {
            Answer = Answer.StartsWith("-") ? Answer.Substring(1) : $"-{Answer}";
        }
    }
}
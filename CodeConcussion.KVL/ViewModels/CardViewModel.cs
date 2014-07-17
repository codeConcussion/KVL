using System.Globalization;
using Caliburn.Micro;
using CodeConcussion.KVL.Entity;

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
                NotifyOfPropertyChange(() => FirstNumber);
                NotifyOfPropertyChange(() => SecondNumber);
                NotifyOfPropertyChange(() => Operation);
                NotifyOfPropertyChange(() => Answer);
            }
        }

        public string FirstNumber
        {
            get { return Card.FirstNumber.ToString().PadLeft(2); }
            set
            {
                if (Card.FirstNumber.ToString() == value) return;
                Card.FirstNumber = int.Parse(value);
                NotifyOfPropertyChange(() => FirstNumber);
                NotifyOfPropertyChange(() => Answer);
            }
        }

        public string SecondNumber
        {
            get { return Card.SecondNumber.ToString().PadLeft(2); }
            set
            {
                if (Card.SecondNumber.ToString() == value) return;
                Card.SecondNumber = int.Parse(value);
                NotifyOfPropertyChange(() => SecondNumber);
                NotifyOfPropertyChange(() => Answer);
            }
        }

        public Operation Operation
        {
            get { return Card.Operation; }
            set
            {
                if (Card.Operation == value) return;
                Card.Operation = value;
                NotifyOfPropertyChange(() => Operation);
                NotifyOfPropertyChange(() => Answer);
            }
        }
        
        public int Answer
        {
            get { return Card.Answer; }
        }

        private string _enteredAnswer;
        public string EnteredAnswer
        {
            get { return _enteredAnswer ?? (_enteredAnswer = ""); }
            set
            {
                if (_enteredAnswer == value) return;
                _enteredAnswer = value;
                NotifyOfPropertyChange(() => EnteredAnswer);
            }
        }
    }
}
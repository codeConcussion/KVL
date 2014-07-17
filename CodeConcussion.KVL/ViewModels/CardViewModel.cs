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

        public int FirstNumber
        {
            get { return Card.FirstNumber; }
            set
            {
                if (Card.FirstNumber == value) return;
                Card.FirstNumber = value;
                NotifyOfPropertyChange(() => FirstNumber);
                NotifyOfPropertyChange(() => Answer);
            }
        }

        public int SecondNumber
        {
            get { return Card.SecondNumber; }
            set
            {
                if (Card.SecondNumber == value) return;
                Card.SecondNumber = value;
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
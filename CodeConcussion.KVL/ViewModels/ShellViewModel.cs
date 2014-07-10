using System.Windows;
using Caliburn.Micro;
using CodeConcussion.KVL.Utility;

namespace CodeConcussion.KVL.ViewModels
{
    public sealed class ShellViewModel : PropertyChangedBase
    {
        public ShellViewModel(IdentityViewModel identityViewModel)
        {
            IdentityViewModel = identityViewModel;
            var decks = DeckConfiguration.Decks;
        }

        public IdentityViewModel IdentityViewModel { get; private set; }

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                NotifyOfPropertyChange(() => Name);
                NotifyOfPropertyChange(() => CanSayHello);
            }
        }

        public bool CanSayHello
        {
            get { return !string.IsNullOrWhiteSpace(Name); }
        }

        public void SayHello()
        {
            MessageBox.Show(string.Format("Hello {0}!", Name)); //Don't do this in real life :)
        }
    }
}

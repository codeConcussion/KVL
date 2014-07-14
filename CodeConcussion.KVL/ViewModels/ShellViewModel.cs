using Caliburn.Micro;
using CodeConcussion.KVL.Utility;

namespace CodeConcussion.KVL.ViewModels
{
    public sealed class ShellViewModel : PropertyChangedBase
    {
        public ShellViewModel(GameViewModel gameViewModel, IdentityViewModel identityViewModel)
        {
            GameViewModel = gameViewModel;
            IdentityViewModel = identityViewModel;
            var decks = DeckConfiguration.Decks;
        }

        public GameViewModel GameViewModel { get; private set; }
        public IdentityViewModel IdentityViewModel { get; private set; }
    }
}
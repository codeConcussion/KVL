using System.Linq;
using Caliburn.Micro;
using CodeConcussion.KVL.Entities;
using CodeConcussion.KVL.Messages;
using CodeConcussion.KVL.Utilities;

namespace CodeConcussion.KVL.ViewModels
{
    public sealed class SettingsViewModel : PropertyChangedBase
    {
        //icons - https://www.iconfinder.com/iconsets/small-n-flat

        public SettingsViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        private readonly IEventAggregator _eventAggregator;

        public void OpenUser()
        {
            _eventAggregator.Publish(new OpenUser(), x => x());
        }

        public void OpenRecords()
        {
            _eventAggregator.Publish(new OpenRecords(), x => x());
        }

        public void StartGame()
        {
            //TODO:select game
            var game = new Game
            {
                Key = "AddTens",
                Name = "Tens",
                Deck = DeckConfiguration.Decks.First(x => x.Key == "AddTens")
            };

            _eventAggregator.Publish(new StartGame(game), x => x());
        }
    }
}
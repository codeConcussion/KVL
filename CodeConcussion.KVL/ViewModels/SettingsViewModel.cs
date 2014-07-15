using Caliburn.Micro;
using CodeConcussion.KVL.Messages;

namespace CodeConcussion.KVL.ViewModels
{
    public sealed class SettingsViewModel : PropertyChangedBase
    {
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
    }
}
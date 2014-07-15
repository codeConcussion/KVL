using Caliburn.Micro;
using CodeConcussion.KVL.Messages;

namespace CodeConcussion.KVL.ViewModels
{
    public sealed class RecordsViewModel : PropertyChangedBase
    {
        public RecordsViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        private readonly IEventAggregator _eventAggregator;

        public void Close()
        {
            _eventAggregator.Publish(new CloseRecords(), x => x());
        }
    }
}
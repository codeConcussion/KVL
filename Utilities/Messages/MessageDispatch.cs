using Caliburn.Micro;

namespace CodeConcussion.KVL.Utilities.Messages
{
    public sealed class MessageDispatch
    {
        public MessageDispatch(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        private readonly IEventAggregator _eventAggregator;

        public void PublishMessage(MessageType type, dynamic data = null)
        {
            _eventAggregator.Publish(new ViewModelMessage(type, data), x => x());
        }
    }
}

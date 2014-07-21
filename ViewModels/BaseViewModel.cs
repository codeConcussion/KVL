using System.Collections.Generic;
using Caliburn.Micro;
using CodeConcussion.KVL.Messages;
using CodeConcussion.KVL.Utilities.Container;

namespace CodeConcussion.KVL.ViewModels
{
    public abstract class BaseViewModel : PropertyChangedBase, IHandle<ViewModelMessage>
    {
        protected BaseViewModel()
        {
            _eventAggregator = ContainerBootstrapper.Resolve<IEventAggregator>();
            _messageHandlers = new Dictionary<MessageType, System.Action<dynamic>>();
            AddMessageHandlers(_messageHandlers);
        }

        private readonly IEventAggregator _eventAggregator;
        //private readonly Dictionary<MessageType, System.Action> _messageHandlers;
        private readonly Dictionary<MessageType, System.Action<dynamic>> _messageHandlers;

        protected virtual void AddMessageHandlers(Dictionary<MessageType, System.Action<dynamic>> map) { }

        protected void PublishMessage(MessageType type, dynamic data = null)
        {
            _eventAggregator.Publish(new ViewModelMessage(type, data), x => x());
        }

        public void Handle(ViewModelMessage message)
        {
            if (_messageHandlers.ContainsKey(message.Type)) _messageHandlers[message.Type](message.Data);
        }
    }
}
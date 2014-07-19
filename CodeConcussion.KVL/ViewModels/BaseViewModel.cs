using System.Collections.Generic;
using Caliburn.Micro;
using CodeConcussion.KVL.Messages;
using CodeConcussion.KVL.Utilities;

namespace CodeConcussion.KVL.ViewModels
{
    public abstract class BaseViewModel : PropertyChangedBase, IHandle<GameMessage>
    {
        protected BaseViewModel()
        {
            _eventAggregator = ContainerBootstrapper.Resolve<IEventAggregator>();
            _messageHandlers = new Dictionary<MessageType, System.Action>();
            AddMessageHandlers(_messageHandlers);
        }

        private readonly IEventAggregator _eventAggregator;
        private readonly Dictionary<MessageType, System.Action> _messageHandlers;

        protected virtual void AddMessageHandlers(Dictionary<MessageType, System.Action> map) { }

        protected void PublishMessage(MessageType type)
        {
            _eventAggregator.Publish(new GameMessage(type), x => x());
        }

        public void Handle(GameMessage message)
        {
            if (_messageHandlers.ContainsKey(message.Type)) _messageHandlers[message.Type]();
        }
    }
}
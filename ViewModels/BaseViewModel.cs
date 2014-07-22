using System.Collections.Generic;
using Caliburn.Micro;
using CodeConcussion.KVL.Utilities.Container;
using CodeConcussion.KVL.Utilities.Messages;

namespace CodeConcussion.KVL.ViewModels
{
    public abstract class BaseViewModel : PropertyChangedBase, IHandle<ViewModelMessage>
    {
        protected BaseViewModel()
        {
            _dispatcher = ContainerBootstrapper.Resolve<MessageDispatch>();
            _messageHandlers = new Dictionary<MessageType, System.Action<dynamic>>();

            // ReSharper disable once DoNotCallOverridableMethodsInConstructor
            AddMessageHandlers(_messageHandlers);
        }

        private readonly MessageDispatch _dispatcher;
        private readonly Dictionary<MessageType, System.Action<dynamic>> _messageHandlers;

        protected virtual void AddMessageHandlers(Dictionary<MessageType, System.Action<dynamic>> map) { }

        protected void PublishMessage(MessageType type, dynamic data = null)
        {
            _dispatcher.PublishMessage(type, data);
        }

        public void Handle(ViewModelMessage message)
        {
            if (_messageHandlers.ContainsKey(message.Type)) _messageHandlers[message.Type](message.Data);
        }
    }
}
using Caliburn.Micro;
using CodeConcussion.KVL.Utilities.Container;
using CodeConcussion.KVL.Utilities.Game;
using CodeConcussion.KVL.Utilities.Messages;
using System.Collections.Generic;

namespace CodeConcussion.KVL.ViewModels
{
    public abstract class BaseViewModel : PropertyChangedBase, IHandle<ViewModelMessage>
    {
        protected BaseViewModel()
        {
            Dispatcher = ContainerBootstrapper.Resolve<MessageDispatch>();
            MessageHandlers = new Dictionary<MessageType, System.Action<dynamic>>();
            GameManager = ContainerBootstrapper.Resolve<GameManager>();

            // ReSharper disable once DoNotCallOverridableMethodsInConstructor
            AddMessageHandlers(MessageHandlers);
        }


        private MessageDispatch Dispatcher { get; }
        private Dictionary<MessageType, System.Action<dynamic>> MessageHandlers;
        protected GameManager GameManager { get; }

        protected virtual void AddMessageHandlers(Dictionary<MessageType, System.Action<dynamic>> map) { }

        protected void PublishMessage(MessageType type, dynamic data = null)
        {
            Dispatcher.PublishMessage(type, data);
        }

        public void Handle(ViewModelMessage message)
        {
            if (MessageHandlers.ContainsKey(message.Type)) MessageHandlers[message.Type](message.Data);
        }
    }
}
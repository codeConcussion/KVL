using Caliburn.Micro;
using CodeConcussion.KVL.Messages;
using CodeConcussion.KVL.Utility;

namespace CodeConcussion.KVL.ViewModels
{
    public sealed class UserViewModel : PropertyChangedBase
    {
        public UserViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        private readonly IEventAggregator _eventAggregator;

        public string User { get; set; }

        public void Close()
        {
            _eventAggregator.Publish(new CloseUser(), x => x());
        }

        public void Cancel()
        {
            Close();
        }

        public void Ok()
        {
            Context.User = User;
            //TODO:load user info
            Close();
        }
    }
}
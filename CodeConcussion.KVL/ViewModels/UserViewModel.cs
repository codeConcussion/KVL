using Caliburn.Micro;
using CodeConcussion.KVL.Entity;
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
            if (!string.IsNullOrWhiteSpace(User)) Close();
        }

        public void Ok()
        {
            if (string.IsNullOrWhiteSpace(User)) return;

            var user = new User(User);
            //TODO:load records
            Context.User = user;
            
            Close();
        }
    }
}
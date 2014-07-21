using CodeConcussion.KVL.Entities;
using CodeConcussion.KVL.Messages;
using CodeConcussion.KVL.Utilities.Game;

namespace CodeConcussion.KVL.ViewModels
{
    public sealed class UserViewModel : BaseViewModel
    {
        public string User { get; set; }

        public void Close()
        {
            if (string.IsNullOrWhiteSpace(User)) return;

            var user = new User(User);
            //TODO:load records
            Context.User = user;

            PublishMessage(MessageType.CloseUser);
        }
    }
}
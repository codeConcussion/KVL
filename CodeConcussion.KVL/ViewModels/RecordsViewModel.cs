using CodeConcussion.KVL.Messages;

namespace CodeConcussion.KVL.ViewModels
{
    public sealed class RecordsViewModel : BaseViewModel
    {
        public void Close()
        {
            PublishMessage(MessageType.CloseRecords);
        }
    }
}
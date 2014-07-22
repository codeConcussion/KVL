namespace CodeConcussion.KVL.Utilities.Messages
{
    public sealed class ViewModelMessage
    {
        public ViewModelMessage(MessageType type, dynamic data)
        {
            Type = type;
            Data = data;
        }

        public MessageType Type { get; set; }
        public dynamic Data { get; set; }
    }
}
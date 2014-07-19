namespace CodeConcussion.KVL.Messages
{
    public sealed class GameMessage
    {
        public GameMessage(MessageType type)
        {
            Type = type;
        }

        public MessageType Type { get; set; }
    }
}

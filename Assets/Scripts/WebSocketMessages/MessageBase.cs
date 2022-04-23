using System;

namespace WebSocketMessages
{
    [Serializable]
    public class MessageBase
    {
        public string MessageType { get; set; }
    }
}
using System;

namespace WebSocketMessages
{
    [Serializable]
    public class ServerMessagePlayerEnter
    {
        public string MessageType { get; set; }
        public Player Player { get; set; }
    }
}
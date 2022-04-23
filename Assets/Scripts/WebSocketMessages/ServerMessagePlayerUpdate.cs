using System;

namespace WebSocketMessages
{
    [Serializable]
    public class ServerMessagePlayerUpdate
    {
        public string MessageType { get; set; }
        public Player Player { get; set; }
    }
}
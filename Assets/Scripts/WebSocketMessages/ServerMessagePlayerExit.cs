using System;

namespace WebSocketMessages
{
    [Serializable]
    public class ServerMessagePlayerExit
    {
        public string MessageType { get; set; }
        public Player Player { get; set; }
    }
}
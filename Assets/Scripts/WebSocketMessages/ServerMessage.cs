using System;
using Models;

namespace WebSocketMessages
{
    [Serializable]
    public class ServerMessage : MessageBase
    {
        public Player Player { get; set; }
    }
}
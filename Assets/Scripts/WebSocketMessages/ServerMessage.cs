using System;
using Models;
using Models.Player;

namespace WebSocketMessages
{
    [Serializable]
    public class ServerMessage : MessageBase
    {
        public Player Player { get; set; }
    }
}
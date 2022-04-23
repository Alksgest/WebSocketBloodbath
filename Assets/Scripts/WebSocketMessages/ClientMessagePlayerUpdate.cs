using System;
using Models;

namespace WebSocketMessages
{
    [Serializable]
    public class ClientMessagePlayerUpdate : MessageBase
    {
        public ClientMessagePlayerUpdate()
        {
            MessageType = "CLIENT_MESSAGE_TYPE_PLAYER_UPDATE";
        }
        
        public Player Player { get; set; }
    }
}
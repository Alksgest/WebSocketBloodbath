using System;
using Models;
using Models.Player;

namespace WebSocketMessages
{
    [Serializable]
    public class ClientMessagePlayerEnter: MessageBase
    {
        public ClientMessagePlayerEnter()
        {
            MessageType = "CLIENT_MESSAGE_TYPE_PLAYER_ENTER";
        }
        
        public Player Player { get; set; }
    }
}
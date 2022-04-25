using System;
using Models;
using Models.Player;

namespace WebSocketMessages
{
    [Serializable]
    public class ClientMessagePlayerShoot: MessageBase
    {
        public ClientMessagePlayerShoot()
        {
            MessageType = "CLIENT_MESSAGE_TYPE_PLAYER_SHOOT";
        }
        
        public Player Player { get; set; }
        public Position ShootVector { get; set; }
        public Position ShootPosition { get; set; }
    }
}
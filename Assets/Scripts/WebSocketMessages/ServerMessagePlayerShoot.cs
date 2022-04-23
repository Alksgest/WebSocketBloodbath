using System;
using Models;

namespace WebSocketMessages
{
    [Serializable]
    public class ServerMessagePlayerShoot : ServerMessage
    {
        public Position ShootVector { get; set; }
        public Position ShootPosition { get; set; }
    }
}
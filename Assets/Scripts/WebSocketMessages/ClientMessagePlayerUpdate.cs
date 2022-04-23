using System;

namespace WebSocketMessages
{
    [Serializable]
    public class ClientMessagePlayerUpdate
    {
        public string MessageType => "CLIENT_MESSAGE_TYPE_PLAYER_UPDATE";
        public Player Player { get; set; }
    }
}
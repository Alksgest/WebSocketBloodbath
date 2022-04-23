using System;

namespace WebSocketMessages
{
    [Serializable]
    public class ClientMessagePlayerEnter
    {
        public string MessageType => "CLIENT_MESSAGE_TYPE_PLAYER_ENTER";
        public Player Player { get; set; }
    }
}
using System;
using Models;

namespace WebSocketMessages
{
    [Serializable]
    public class MessageGameState : MessageBase
    {
        public GameState GameState { get; set; }
    }
}
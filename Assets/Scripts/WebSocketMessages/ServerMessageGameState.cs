using System;
using UnityEngine.EventSystems;

namespace WebSocketMessages
{
    [Serializable]
    public class ServerMessageGameState
    {
        public string MessageType { get; set; }
        public GameState GameState { get; set; }
    }
}
using System;
using System.Collections.Generic;

namespace Models
{
    [Serializable]
    public class GameState
    {
        public List<string> ConnectionIds { get; set; }
        public List<Player.Player> Players { get; set; }
    }
}
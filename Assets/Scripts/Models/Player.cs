using System;

namespace Models
{
    [Serializable]
    public class Player : ISharedObject
    {
        public string Id { get; set; }
        public PlayerStats PlayerStats { get; set; }
        public Position Position { get; set; }
        public Rotation Rotation { get; set; }
    }
}
using System;

namespace Models
{
    [Serializable]
    public class Player : ISharedObject
    {
        public string Id { get; set; }
        public Position Position { get; set; }
        public Position Rotation { get; set; }
    }
}
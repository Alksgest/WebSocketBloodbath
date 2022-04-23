using System;

namespace Models
{
    [Serializable]
    public class Bullet : ISharedObject
    {
        public string Id { get; set; }
        public Position Position { get; set; }
    }
}
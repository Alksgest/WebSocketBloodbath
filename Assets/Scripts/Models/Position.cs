using UnityEngine;

namespace Models
{
    public class Position
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public static implicit operator Position(Vector3 vec)
        {
            return new Position
            {
                X = vec.x,
                Y = vec.y,
                Z = vec.z
            };
        }
    }
}
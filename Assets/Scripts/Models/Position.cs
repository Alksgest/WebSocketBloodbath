using System;
using UnityEngine;

namespace Models
{
    [Serializable]
    public class Position
    {
        [field: SerializeField] public float X { get; set; }
        [field: SerializeField] public float Y { get; set; }
        [field: SerializeField] public float Z { get; set; }

        public static implicit operator Position(Vector3 vec)
        {
            return new Position
            {
                X = vec.x,
                Y = vec.y,
                Z = vec.z
            };
        }

        public static implicit operator Vector3(Position rValue)
        {
            return new Vector3(rValue.X, rValue.Y, rValue.Z);
        }
    }
}
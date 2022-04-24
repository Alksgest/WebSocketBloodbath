using System;
using UnityEngine;

namespace Models
{
    [Serializable]
    public class Position
    {
        [SerializeField] private float x;
        [SerializeField] private float y;
        [SerializeField] private float z;

        public float X
        {
            get => x;
            set => x = value;
        }

        public float Y
        {
            get => y;
            set => y = value;
        }

        public float Z
        {
            get => z;
            set => z = value;
        }

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
using System;
using UnityEngine;

namespace Models
{
    [Serializable]
    public class Rotation
    {
        [SerializeField] private float x;
        [SerializeField] private float y;
        [SerializeField] private float z;
        [SerializeField] private float w;

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

        public float W
        {
            get => w;
            set => w = value;
        }

        public static implicit operator Quaternion(Rotation rValue)
        {
            return new Quaternion(rValue.X, rValue.Y, rValue.Z, rValue.W);
        }

        public static implicit operator Rotation(Quaternion rValue)
        {
            return new Rotation
            {
                W = rValue.w,
                X = rValue.x,
                Y = rValue.y,
                Z = rValue.z,
            };
        }
    }
}
using System;
using UnityEngine;

namespace Models
{
    [Serializable]
    public class Rotation
    {
        [field: SerializeField] public float X { get; set; }
        [field: SerializeField] public float Y { get; set; }
        [field: SerializeField] public float Z { get; set; }
        [field: SerializeField] public float W { get; set; }

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
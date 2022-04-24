using System;
using UnityEngine;

namespace Models
{
    [Serializable]
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
        
        public static implicit operator Vector3(Position rValue)
        {
            return new Vector3(rValue.X, rValue.Y, rValue.Z);
        }
    }

    [Serializable]
    public class Rotation
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
        public float W { get; set; }
        
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
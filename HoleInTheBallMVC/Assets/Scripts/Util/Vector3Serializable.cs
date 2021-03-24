using System;
using UnityEngine;

namespace Hole
{
    [Serializable]
    public class Vector3Serializable
    {
        public float X;
        public float Y;
        public float Z;

        private Vector3Serializable(float valueX, float valueY, float valueZ)
        {
            X = valueX;
            Y = valueY;
            Z = valueZ;
        }

        public static implicit operator Vector3(Vector3Serializable value)
        {
            return new Vector3(value.X, value.Y, value.Z);
        }

        public static implicit operator Quaternion(Vector3Serializable value)
        {
            var q = new Quaternion();
            q.eulerAngles = new Vector3(value.X, value.Y, value.Z);
            return q;
        }

        public static implicit operator Vector3Serializable(Vector3 value)
        {
            return new Vector3Serializable(value.x, value.y, value.z);
        }

        public static implicit operator Vector3Serializable(Quaternion value)
        {
            return new Vector3Serializable(value.eulerAngles.x, value.eulerAngles.y, value.eulerAngles.z);
        }


        public override string ToString()
        {
            return $"Vector: {X},{Y},{Z}";
        }
    }
}
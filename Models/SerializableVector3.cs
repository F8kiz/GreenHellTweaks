﻿namespace GHTweaks.Models
{
    public class SerializableVector3
    {
        public float X { get; set; }

        public float Y { get; set; }

        public float Z { get; set; }


        public SerializableVector3() { }

        public SerializableVector3(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public override string ToString()
        {
            return $"X: {X}, Y: {Y}, Z: {Z}";
        }
    }
}

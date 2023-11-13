using System;
using UnityEngine;

namespace trrne.Box
{
    public static partial class Vector100
    {
        public static Vector3 X => new(1, 0, 0);
        public static Vector3 Y => new(0, 1, 0);
        public static Vector3 Z => new(0, 0, 1);
        public static Vector3 Zero => new(0, 0, 0);
        public static Vector3 One => new(1, 1, 1);

        public static Vector2 X2D => new(1, 0);
        public static Vector2 Y2D => new(0, 1);
        public static Vector2 Zero2D => new(0, 0);
        public static Vector2 One2D => new(1, 1);

        public static Quaternion XQ => new(1, 0, 0, 0);
        public static Quaternion YQ => new(0, 1, 0, 0);
        public static Quaternion ZQ => new(0, 0, 1, 0);
        public static Quaternion WQ => new(0, 0, 0, 1);

        /// <summary>
        /// [xX]<br/>[yY]<br/>[zZ]<br/>[wW]<br/>[zZ]ero<br/>[oO]ne
        /// </summary>
        public static Quaternion Q(string xyzw0) => xyzw0 switch
        {
            "x" or "X" => new Quaternion(1, 0, 0, 0),
            "y" or "Y" => new Quaternion(0, 1, 0, 0),
            "z" or "Z" => new Quaternion(0, 0, 1, 0),
            "w" or "W" => new Quaternion(0, 0, 0, 1),
            "zero" or "Zero" or "0" => new Quaternion(0, 0, 0, 0),
            "one" or "One" or "1" => new Quaternion(1, 1, 1, 1),
            _ => throw null
        };

        /// <summary>
        /// 重力加速度
        /// </summary>
        public static float GravitationalAcceleration => 9.80665f;

        /// <summary>
        /// 重力加速度
        /// </summary>
        public static Vector3 Gravity => -Y * GravitationalAcceleration;

        public static bool Twins(Vector3 n1, Vector3 n2) => Maths.Twins(n1.x, n2.x) && Maths.Twins(n1.y, n2.y) && Maths.Twins(n1.z, n2.z);

        [Obsolete] public static Vector2 Direction(Vector2 target, Vector2 origin) => target - origin;
    }
}

using System;
using UnityEngine;

namespace trrne.Box
{
    public static partial class Vec
    {
        /// <summary>
        /// Vector3(1, 0, 0)
        /// </summary>
        public static Vector3 X => new(1, 0, 0);

        /// <summary>
        /// Vector3(0, 1, 0)
        /// </summary>
        public static Vector3 Y => new(0, 1, 0);

        /// <summary>
        /// Vector3(0, 0, 1)
        /// </summary>
        public static Vector3 Z => new(0, 0, 1);

        /// <summary>
        /// Quaternion(1, 0, 0, 0)
        /// </summary>
        public static Quaternion QX => new(1, 0, 0, 0);

        /// <summary>
        /// Quaternion(0, 1, 0, 0)
        /// </summary>
        public static Quaternion QY => new(0, 1, 0, 0);

        /// <summary>
        /// Quaternion(0, 0, 1, 0)
        /// </summary>
        public static Quaternion QZ => new(0, 0, 1, 0);

        /// <summary>
        /// Quaternion(0, 0, 0, 1)
        /// </summary>
        public static Quaternion QW => new(0, 0, 0, 1);

        /// <summary>
        /// Quaternion(0, 0, 0, 0)
        /// </summary>
        public static Quaternion Q0 => new(0, 0, 0, 0);

        /// <summary>
        /// Quaternion(1, 1, 1, 1)
        /// </summary>
        public static Quaternion Q1 => new(1, 1, 1, 1);

        public static Vector2 Make2(float x = 0, float y = 0) => new(x, y);
        public static Vector3 Make3(float x = 0, float y = 0, float z = 0) => new(x, y, z);

        /// <summary>
        /// 重力加速度
        /// </summary>
        public static float GravitationalAcceleration => 9.80665f;

        public static Vector2 Gravity => GravitationalAcceleration * -Y;

        /// <summary>
        /// 2つのベクトルを比較する -> <b>bool</b><br/>
        /// </summary>
        /// <returns>aとbがほぼ同じならtrueを変える</returns>
        public static bool Twins(Vector3 a, Vector3 b)
        => Mathf.Approximately(a.x, b.x)
            && Mathf.Approximately(a.y, b.y)
            && Mathf.Approximately(a.z, b.z);

        public static float Dot(Vector2 a, Vector3 b)
        => a.x * b.x + a.y * b.y;

        public static float Angle(Vector2 a, Vector2 b)
        => MathF.Acos(Dot(a, b) / (a.magnitude * b.magnitude));

        /// <summary>
        /// 極座標を直交座標に変換
        /// </summary>
        public static Vector2 Polor2Rectangular(Vector2 p)
        => new(p.x * MathF.Cos(p.y), p.x * MathF.Sin(p.y));

        public static Vector3 Mul(this Vector3 v, float a)
        => new(v.x * a, v.y * a, v.z * a);
    }
}

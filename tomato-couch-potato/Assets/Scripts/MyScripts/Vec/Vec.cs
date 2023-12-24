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

        public static float Dot(Vector2 a, Vector3 b) => a.x * b.x + a.y * b.y;

        public static float Angle(Vector2 a, Vector2 b)
        {
            float lal = Mathf.Sqrt(a.x * a.x + a.y * a.y),
                lbl = Mathf.Sqrt(b.x * b.x + b.y * b.y);
            return Mathf.Acos(Dot(a, b) / (lal * lbl));
        }

        /// <summary>
        /// 極座標を直交座標に変換
        /// </summary>
        public static Vector2 Polor2Rectangular(Vector2 p)
        => new(
            x: p.x * MathF.Cos(p.y),
            y: p.x * MathF.Sin(p.y)
        );
    }
}

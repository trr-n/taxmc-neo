using System;
using UnityEngine;

namespace trrne.Box
{
    public static partial class Coordinate
    {
        public static Vector2 Position2(this GameObject gob) => gob.transform.position;
        public static Vector2 Position2(this Collider2D info) => info.gameObject.transform.position;
        public static Vector2 Position2(this Collision2D info) => info.gameObject.transform.position;

        public static Vector2 Normalized(this Vector2 v) => v.normalized;

        [Obsolete] public static float Magnitude(this Quaternion q) => Mathf.Sqrt(q.x * q.x + q.y * q.y + q.z * q.z + q.w * q.w);
    }
}
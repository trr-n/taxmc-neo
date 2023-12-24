using System;
using UnityEngine;

namespace trrne.Box
{
    public static partial class Vec
    {
        public static Vector2 Position2(this GameObject g)
        => g.transform.position;
        public static Vector2 Position2(this Collider2D info)
        => info.gameObject.transform.position;
        public static Vector2 Position2(this Collision2D info)
        => info.gameObject.transform.position;

        public static Vector2 Normalized(this Vector2 v) => v.normalized;

        [Obsolete]
        public static float Magnitude(this Quaternion q)
        => Mathf.Sqrt(q.x * q.x + q.y * q.y + q.z * q.z + q.w * q.w);

        public static float Distance(Vector2 a, Vector2 b)
        => (a - b).magnitude;

        public static float Distance(Transform a, Transform b)
        => (a.position - b.position).magnitude;

        public static float Distance(Vector3 a, Transform b)
        => (a - b.position).magnitude;

        public static float Distance(Transform a, Vector3 b)
        => (a.position - b).magnitude;
    }
}
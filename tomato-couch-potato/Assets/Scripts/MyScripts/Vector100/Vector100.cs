using System;
using Unity.Mathematics;
using UnityEngine;

namespace trrne.Pancreas
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

        public static float G => 9.80665f;
        public static Vector3 Gravity => -Y * G;

        public static bool Twins(Vector3 n1, Vector3 n2)
        => Mathf.Approximately(n1.x, n2.x) && Mathf.Approximately(n1.y, n2.y) && Mathf.Approximately(n1.z, n2.z);

        static readonly Runner setter = new();
        static Vector3 latest;
        [Obsolete]
        public static float Speed(this GameObject gob)
        {
            setter.RunOnce(() => latest = gob.transform.position);
            var direction = gob.transform.position - latest;
            var speed = direction / Time.deltaTime;
            latest = gob.transform.position;
            return speed.magnitude;
        }

        [Obsolete] public static float Speed(this Transform transform) => transform.gameObject.Speed();

        public static Vector2 Direction(Vector2 target, Vector2 origin) => target - origin;
    }
}

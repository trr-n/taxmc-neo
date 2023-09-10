using System;
using UnityEngine;

namespace Self.Utils
{
    public static class Coordinate
    {
        public static Vector3 X => new(1, 0);
        public static Vector3 MX => new(-1, 0);
        public static Vector3 Y => new(0, 1);
        public static Vector3 MY => new(0, -1);
        public static Vector3 Z => new(0, 0, 1);
        public static Vector3 MZ => new(0, 0, -1);

        public static float G => 9.81f;

        public static void SetPosition(this Transform t, double? x = null, double? y = null, double? z = null)
        {
            if (x is null && y is null && z is null)
                throw new Karappoyanke();

            t.position = new(x is null ? t.position.x : (float)x, y is null ? t.position.y : (float)y, z is null ? t.position.z : (float)z);
        }

        public static void SetPosition(this Transform t, Vector3 position) => t.position = position;

        public static void SetPosition(this Transform t, Vector2 position) => t.position = position;

        public static void ClampPosition22(this Transform t,
            float? minX = null, float? maxX = null, float? minY = null, float? maxY = null, float? minZ = null, float? maxZ = null)
        {
            if (minX is null && maxX is null && minY is null && maxY is null && minZ is null && maxZ is null)
                throw new Karappoyanke();

            t.position = new(minX is null ? t.position.x : Mathf.Clamp(t.position.x, (float)minX, (float)maxX),
                minY is null ? t.position.y : Mathf.Clamp(t.position.y, (float)minY, (float)maxY),
                minZ is null ? t.position.z : Mathf.Clamp(t.position.z, (float)minZ, (float)maxZ));
        }

        public static void ClampPosition2(this Transform t, float? minX = null, float? maxX = null, float? minY = null, float? maxY = null)
        {
            if (minX is null && maxX is null && minY is null && maxY is null)
                throw new Karappoyanke();

            t.position = new(Mathf.Clamp(t.position.x, (float)minX, (float)maxX), Mathf.Clamp(t.position.y, (float)minY, (float)maxY));
        }

        public static void ClampPosition2(this Transform t, (float min, float max)? x = null, (float min, float max)? y = null)
        {
            if (x is null && y is null)
                throw new Karappoyanke();

            t.position = new(Mathf.Clamp(t.position.x, x.Value.max, x.Value.max), Mathf.Clamp(t.position.y, y.Value.min, y.Value.max));
        }

        public static void ClampPosition2(this Transform t, float? x = null, float? y = null)
        {
            if (x is null && y is null)
                throw new Karappoyanke();

            t.position = new(Mathf.Clamp(t.position.x, (float)x, (float)-x), Mathf.Clamp(t.position.y, (float)y, (float)-y));
        }

        public static void SetRotation(this Transform t, double? x = null, double? y = null, double? z = null, double? w = null)
        {
            if (x is null && y is null && z is null & w is null)
                throw new Karappoyanke();

            t.rotation = new(x is null ? t.rotation.x : (float)x, y is null ? t.rotation.y : (float)y, z is null ? t.rotation.z : (float)z, w is null ? t.rotation.w : (float)w);
        }

        public static void SetEuler(this Transform t, double? x = null, double? y = null, double? z = null)
        {
            if (x is null && y is null && z is null)
                throw new Karappoyanke();

            t.rotation = Quaternion.Euler(x is null ? t.localScale.x : (float)x, y is null ? t.localScale.y : (float)y, z is null ? t.localScale.z : (float)z);
        }

        public static void SetRotation(this Transform t, Quaternion rotation) => t.rotation = rotation;

        public static void SetScale(this Transform t, double? x = null, double? y = null, double? z = null)
        {
            if (x is null && y is null && z is null)
                throw new Karappoyanke();

            t.localScale = new(x is null ? t.localScale.x : (float)x, y is null ? t.localScale.y : (float)y, z is null ? t.localScale.z : (float)z);
        }

        public static void SetScale(this Transform t, Vector3 scale) => t.localScale = scale;

        public static bool Twins(Vector3 n1, Vector3 n2)
        => Mathf.Approximately(n1.x, n2.x) && Mathf.Approximately(n1.y, n2.y) && Mathf.Approximately(n1.z, n2.z);

        static readonly Runner setLatest = new();
        static Vector3 latest;
        public static float Speed(this GameObject gob)
        {
            setLatest.RunOnce(() => latest = gob.transform.position);
            var direction = gob.transform.position - latest;
            var speed = direction / Time.deltaTime;
            latest = gob.transform.position;
            return speed.magnitude;
        }

        public static float Speed(this Transform transform) => transform.gameObject.Speed();
    }
}

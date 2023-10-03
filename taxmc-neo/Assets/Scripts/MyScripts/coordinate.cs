using System;
using System.Linq.Expressions;
using UnityEngine;

namespace trrne.Bag
{
    public static class Coordinate
    {
        public static Vector3 x => new(1, 0, 0);
        public static Vector3 y => new(0, 1, 0);
        public static Vector3 z => new(0, 0, 1);
        public static Vector3 zero => Vector3.zero;
        public static Vector3 one => Vector3.one;

        public static Vector2 x2d => new(1, 0);
        public static Vector2 y2d => new(0, 1);
        public static Vector2 z2d => new(0, 0);
        public static Vector2 zero2d => Vector2.zero;
        public static Vector2 one2d => Vector2.one;

        public static float g => 9.80665f;
        public static Vector3 gravity = -y * g;

        public static Vector3 Position(this GameObject gob) => gob.transform.position;

        public static Vector2 Position2(this Collider2D info) => info.gameObject.transform.position;

        public static void SetPosition(this Transform t, float? x = null, float? y = null, float? z = null)
        {
            if (x is null && y is null && z is null) { throw new Karappoyanke(); }
            t.position = new(x is null ? t.position.x : (float)x, y is null ? t.position.y : (float)y, z is null ? t.position.z : (float)z);
        }

        public static void SetPosition(this Transform t, Vector3 position) => t.position = position;
        public static void SetPosition(this Transform t, Vector2 position) => t.position = position;
        public static void SetPosition(this RaycastHit2D hit, Vector2 position) => hit.collider.transform.position = position;

        public static void SetPosition(this RectTransform rt, Vector2 position) => rt.transform.position = position;
        public static void SetPosition(this RectTransform rt, Vector3 position) => rt.transform.position = (Vector2)position;

        public static void ClampPosition22(this Transform t,
            float? minX = null, float? maxX = null, float? minY = null, float? maxY = null, float? minZ = null, float? maxZ = null)
        {
            if (minX is null && maxX is null && minY is null && maxY is null && minZ is null && maxZ is null) { throw new Karappoyanke(); }

            t.position = new(minX is null ? t.position.x : Mathf.Clamp(t.position.x, (float)minX, (float)maxX),
                minY is null ? t.position.y : Mathf.Clamp(t.position.y, (float)minY, (float)maxY),
                minZ is null ? t.position.z : Mathf.Clamp(t.position.z, (float)minZ, (float)maxZ));
        }

        public static void ClampPosition2(this Transform t, float? minX = null, float? maxX = null, float? minY = null, float? maxY = null)
        {
            if (minX is null && maxX is null && minY is null && maxY is null) { throw new Karappoyanke(); }

            t.position = new(Mathf.Clamp(t.position.x, (float)minX, (float)maxX), Mathf.Clamp(t.position.y, (float)minY, (float)maxY));
        }

        public static void ClampPosition2(this Transform t, (float min, float max)? x = null, (float min, float max)? y = null)
        {
            if (x is null && y is null) { throw new Karappoyanke(); }

            t.position = new(Mathf.Clamp(t.position.x, x.Value.max, x.Value.max), Mathf.Clamp(t.position.y, y.Value.min, y.Value.max));
        }

        public static void ClampPosition2(this Transform t, float? x = null, float? y = null)
        {
            if (x is null && y is null) { throw new Karappoyanke(); }

            t.position = new(Mathf.Clamp(t.position.x, (float)x, (float)-x), Mathf.Clamp(t.position.y, (float)y, (float)-y));
        }

        public static void SetVelocity(this Rigidbody2D rb, float x, float y) => rb.velocity = new(x, y);

        public static void SetVelocityX(this Rigidbody2D rb, float velocity) { rb.velocity = new(velocity, rb.velocity.y); }

        public static void SetRotation(this Transform t, float? x = null, float? y = null, float? z = null, float? w = null)
        {
            if (x is null && y is null && z is null & w is null) { throw new Karappoyanke(); }

            t.rotation = new(x is null ? t.rotation.x : (float)x, y is null ? t.rotation.y : (float)y, z is null ? t.rotation.z : (float)z, w is null ? t.rotation.w : (float)w);
        }

        public static void SetEuler(this Transform t, float? x = null, float? y = null, float? z = null)
        {
            if (x is null && y is null && z is null) { throw new Karappoyanke(); }

            t.rotation = Quaternion.Euler(x is null ? t.localScale.x : (float)x, y is null ? t.localScale.y : (float)y, z is null ? t.localScale.z : (float)z);
        }

        public static void SetRotation(this Transform t, Quaternion rotation) => t.rotation = rotation;

        public static void SetScale(this Transform t, float? x = null, float? y = null, float? z = null)
        {
            if (x is null && y is null && z is null) { throw new Karappoyanke(); }

            t.localScale = new(x is null ? t.localScale.x : (float)x, y is null ? t.localScale.y : (float)y, z is null ? t.localScale.z : (float)z);
        }

        public static void SetScale(this Transform t, Vector3 scale) => t.localScale = scale;

        public static bool Twins(Vector3 n1, Vector3 n2)
        => Mathf.Approximately(n1.x, n2.x) && Mathf.Approximately(n1.y, n2.y) && Mathf.Approximately(n1.z, n2.z);

        [Obsolete]
        static readonly Runner setter = new();
        [Obsolete]
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

        [Obsolete]
        public static float Speed(this Transform transform) => transform.gameObject.Speed();
    }
}

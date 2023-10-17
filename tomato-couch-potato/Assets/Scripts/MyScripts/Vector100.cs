using System;
using Unity.Mathematics;
using UnityEngine;

namespace trrne.Pancreas
{
    public static class Vector100
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

        public static Vector2 Position2(this GameObject gob) => gob.transform.position;
        public static Vector2 Position2(this Collider2D info) => info.gameObject.transform.position;
        public static Vector2 Position2(this Collision2D info) => info.gameObject.transform.position;

        public static void SetPosition(this Transform t, float? x = null, float? y = null, float? z = null)
        {
            t.position = new(x ?? t.position.x, y ?? t.position.y, z ?? t.position.z);
        }

        public static void SetPosition(this GameObject gob, Vector2 position)
        {
            gob.transform.position = position;
        }

        public static void SetPosition(this Transform t, Vector3 position)
        {
            t.position = position;
        }

        public static void SetPosition(this Transform t, Vector2 position)
        {
            t.position = position;
        }

        public static void SetPosition(this RaycastHit2D hit, Vector2 position)
        {
            hit.collider.transform.position = position;
        }

        public static void SetPosition(this Collider2D info, Vector2 position)
        {
            info.transform.position = position;
        }

        public static void SetPosition(this RectTransform rt, Vector2 position)
        {
            rt.transform.position = position;
        }

        public static void SetPosition(this RectTransform rt, Vector3 position)
        {
            rt.transform.position = (Vector2)position;
        }

        public static void ClampPosition2(this Transform t, float? minX = null, float? maxX = null, float? minY = null, float? maxY = null)
        {
            t.position = new(Mathf.Clamp(t.position.x, (float)minX, (float)maxX), Mathf.Clamp(t.position.y, (float)minY, (float)maxY));
        }

        public static void ClampPosition2(this Transform t, float? x = null, float? y = null)
        {
            t.position = new(Mathf.Clamp(t.position.x, (float)x, (float)-x), Mathf.Clamp(t.position.y, (float)y, (float)-y));
        }

        public static void SetVelocity(this Rigidbody2D rb, float x, float y)
        {
            rb.velocity = new(x, y);
        }

        public static void SetVelocityX(this Rigidbody2D rb, float velocity)
        {
            rb.velocity = new(velocity, rb.velocity.y);
        }

        public static void SetEuler(this Transform t, float? x = null, float? y = null, float? z = null)
        {
            t.rotation = quaternion.Euler(x ?? t.localScale.x, y ?? t.localScale.y, z ?? t.lossyScale.z);
        }

        public static void SetRotation(this Transform t, Quaternion rotation)
        {
            t.rotation = rotation;
        }

        public static void SetScale(this Transform t, float? x = null, float? y = null, float? z = null)
        {
            t.rotation = quaternion.Euler(x ?? t.localScale.x, y ?? t.localScale.y, z ?? t.lossyScale.z);
        }

        public static void SetScale(this Transform t, Vector3 scale)
        {
            t.localScale = scale;
        }

        public static bool Twins(Vector3 n1, Vector3 n2)
        {
            return Mathf.Approximately(n1.x, n2.x) && Mathf.Approximately(n1.y, n2.y) && Mathf.Approximately(n1.z, n2.z);
        }

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

        [Obsolete]
        public static float Speed(this Transform transform)
        {
            return transform.gameObject.Speed();
        }
    }
}

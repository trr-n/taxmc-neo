using System;
using UnityEngine;

namespace trrne.Box
{
    public static partial class Vec
    {
        // position
        public static void SetPosition(this Transform t, float? x = null, float? y = null, float? z = null)
        => t.position = new(x ?? t.position.x, y ?? t.position.y, z ?? t.position.z);
        public static void SetPosition(this GameObject g, Vector2 p) => g.transform.position = p;
        public static void SetPosition(this Transform t, Vector3 p) => t.position = p;
        public static void SetPosition(this Transform t, Vector2 p) => t.position = p;
        public static void SetPosition(this Transform t, Transform p) => t.position = p.position;
        public static void SetPosition(this RaycastHit2D hit, Vector2 p) => hit.collider.transform.position = p;
        public static void SetPosition(this Collider2D info, Vector2 p) => info.transform.position = p;
        public static void SetPosition(this RectTransform rt, Vector2 p) => rt.transform.position = p;
        public static void SetPosition(this RectTransform rt, Vector3 p) => rt.transform.position = (Vector2)p;

        public static void ClampPosition(this Transform t, float? minX = null, float? maxX = null, float? minY = null, float? maxY = null)
        => t.position = new(x: Mathf.Clamp(t.position.x, (float)minX, (float)maxX), y: Mathf.Clamp(t.position.y, (float)minY, (float)maxY));
        public static void ClampPosition(this Transform t, float? x = null, float? y = null)
        => t.position = new(Mathf.Clamp(t.position.x, (float)x, (float)-x), Mathf.Clamp(t.position.y, (float)y, (float)-y));
        public static void ClampPosition(this Transform t, (float min, float max)? x = null, (float min, float max)? y = null, (float min, float max)? z = null)
        => t.position = new(
            x is null ? t.position.x : Mathf.Clamp(t.position.x, -x.Value.min, x.Value.max),
            y is null ? t.position.y : Mathf.Clamp(t.position.y, -y.Value.min, y.Value.max),
            z is null ? t.position.z : Mathf.Clamp(t.position.z, -z.Value.min, z.Value.max)
        );

        public static void Translate(this Transform t, float x = 0, float y = 0, float z = 0, Space space = Space.Self) => t.Translate(x, y, z, space);

        // velocity
        public static void SetVelocity(this Rigidbody2D rb, float? x = null, float? y = null) => rb.velocity = new(x ?? rb.velocity.x, y ?? rb.velocity.y);

        public static void AddVelocity(this Rigidbody2D rb, float x = 0, float y = 0) => rb.velocity += new Vector2(x, y);
        public static void AddVelocity(this Rigidbody2D rb, Vector2 v) => rb.velocity += v;

        public static void ClampVelocity(this Rigidbody2D rb, (float min, float max)? x = null, (float min, float max)? y = null)
        => rb.velocity = new(
            x is null ? rb.velocity.x : Mathf.Clamp(rb.velocity.x, x.Value.min, x.Value.max),
            y is null ? rb.velocity.y : Mathf.Clamp(rb.velocity.y, y.Value.min, y.Value.max)
        );

        // rotation
        public static void SetEuler(this Transform t, float? x = null, float? y = null, float? z = null)
        => t.rotation = Quaternion.Euler(x ?? t.localScale.x, y ?? t.localScale.y, z ?? t.lossyScale.z);
        public static void SetEuler(this Transform t, Vector3? p = null) => t.rotation = Quaternion.Euler(p ?? t.eulerAngles);

        public static void SetRotation(this Transform t, Quaternion q) => t.rotation = q;
        public static void SetRotation(this Transform t, float? x = null, float? y = null, float? z = null, float? w = null)
        => t.rotation = new(x ?? t.rotation.x, y ?? t.rotation.y, z ?? t.rotation.z, w ?? t.rotation.w);

        public static void Rotate(this Transform t, float x = 0, float y = 0, float z = 0, Space space = Space.Self) => t.Rotate(x, y, z, space);

        // scale
        public static void SetScale(this Transform t, float? x = null, float? y = null, float? z = null)
        => t.localScale = new(x ?? t.localScale.x, y ?? t.localScale.y, z ?? t.localScale.z);
        public static void SetScale(this Transform t, Vector3 scale) => t.localScale = scale;
    }
}
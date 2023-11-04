using UnityEngine;

namespace trrne.Box
{
    public static partial class Vector100
    {
        public static void SetPosition(this Transform t, float? x = null, float? y = null, float? z = null)
        => t.position = new(x ?? t.position.x, y ?? t.position.y, z ?? t.position.z);
        public static void SetPosition(this GameObject gob, Vector2 position) => gob.transform.position = position;
        public static void SetPosition(this Transform t, Vector3 position) => t.position = position;
        public static void SetPosition(this Transform t, Vector2 position) => t.position = position;
        public static void SetPosition(this RaycastHit2D hit, Vector2 position) => hit.collider.transform.position = position;
        public static void SetPosition(this Collider2D info, Vector2 position) => info.transform.position = position;
        public static void SetPosition(this RectTransform rt, Vector2 position) => rt.transform.position = position;
        public static void SetPosition(this RectTransform rt, Vector3 position) => rt.transform.position = (Vector2)position;

        public static void ClampPosition2(this Transform t, float? minX = null, float? maxX = null, float? minY = null, float? maxY = null)
        => t.position = new(Mathf.Clamp(t.position.x, (float)minX, (float)maxX), Mathf.Clamp(t.position.y, (float)minY, (float)maxY));
        public static void ClampPosition2(this Transform t, float? x = null, float? y = null)
        => t.position = new(Mathf.Clamp(t.position.x, (float)x, (float)-x), Mathf.Clamp(t.position.y, (float)y, (float)-y));

        public static void SetVelocity(this Rigidbody2D rb, float x, float y) => rb.velocity = new(x, y);
        public static void SetVelocityX(this Rigidbody2D rb, float velocity) => rb.velocity = new(velocity, rb.velocity.y);

        public static void SetEuler(this Transform t, float? x = null, float? y = null, float? z = null)
        => t.rotation = Quaternion.Euler(x ?? t.localScale.x, y ?? t.localScale.y, z ?? t.lossyScale.z);
        public static void SetEuler(this Transform t, Vector3? p) => t.rotation = Quaternion.Euler(p ?? t.eulerAngles);
        public static void SetRotation(this Transform t, Quaternion rotation) => t.rotation = rotation;
        public static void SetRotation(this Transform t, float? x = null, float? y = null, float? z = null, float? w = null)
        => t.rotation = new(x ?? t.rotation.x, y ?? t.rotation.y, z ?? t.rotation.z, w ?? t.rotation.w);

        public static void SetScale(this Transform t, float? x = null, float? y = null, float? z = null)
        => t.rotation = Quaternion.Euler(x ?? t.localScale.x, y ?? t.localScale.y, z ?? t.lossyScale.z);
        public static void SetScale(this Transform t, Vector3 scale) => t.localScale = scale;
    }
}
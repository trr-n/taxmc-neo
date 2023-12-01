#pragma warning disable IDE0002

using UnityEngine;

namespace trrne.Box
{
    public static partial class Gobject
    {
        public static T Instantiate<T>(this T t, Vector3 p = new(), Quaternion r = new()) where T : Object
        => GameObject.Instantiate(t, p, r);
        public static T Instantiate<T>(this T[] ts, Vector3 p = new(), Quaternion r = new()) where T : Object
        => GameObject.Instantiate(ts.Choice(), p, r);
        public static T Instantiate<T>(this T t, Transform transform) where T : Object
        => GameObject.Instantiate(t, transform.position, transform.rotation);
        public static T Instantiate<T>(this T[] ts, Transform t) where T : Object
        => GameObject.Instantiate(ts.Choice(), t.position, t.rotation);

        public static T TryInstantiate<T>(this T t, Vector3 p = new(), Quaternion r = new()) where T : Object
        => t != null ? GameObject.Instantiate(t, p, r) : null;
        public static T TryInstantiate<T>(this T t, Transform transform) where T : Object
        => t != null ? GameObject.Instantiate(t, transform.position, transform.rotation) : null;
        public static T TryInstantiate<T>(this T[] ts, Vector3 p = new(), Quaternion q = new()) where T : Object
        => ts.Length > 0 ? GameObject.Instantiate(ts.Choice(), p, q) : null;
        public static T TryInstantiate<T>(this T[] ts, Transform t) where T : Object
        => ts.Length > 0 ? GameObject.Instantiate(ts.Choice(), t.position, t.rotation) : null;
    }
}
#pragma warning disable IDE0002

using UnityEngine;

namespace trrne.Box
{
    public static partial class Gobject
    {
        public static T Instantiate<T>(this T g, Vector3 p = default, Quaternion r = default) where T : Object
        => GameObject.Instantiate(g, p, r);
        public static T Instantiate<T>(this T[] gs, Vector3 p = default, Quaternion r = default) where T : Object
        => GameObject.Instantiate(gs.Choice(), p, r);
        public static T Instantiate<T>(this T g, Transform t) where T : Object
        => GameObject.Instantiate(g, t.position, t.rotation);
        public static T Instantiate<T>(this T[] gs, Transform t) where T : Object
        => GameObject.Instantiate(gs.Choice(), t.position, t.rotation);

        public static T TryInstantiate<T>(this T g, Vector3 p = default, Quaternion r = default) where T : Object
        => g != null ? GameObject.Instantiate(g, p, r) : null;
        public static T TryInstantiate<T>(this T g, Transform t) where T : Object
        => g != null ? GameObject.Instantiate(g, t.position, t.rotation) : null;
        public static T TryInstantiate<T>(this T[] gs, Vector3 p = default, Quaternion r = default) where T : Object
        => gs.Length > 0 ? GameObject.Instantiate(gs.Choice(), p, r) : null;
        public static T TryInstantiate<T>(this T[] gs, Transform t) where T : Object
        => gs.Length > 0 ? GameObject.Instantiate(gs.Choice(), t.position, t.rotation) : null;
    }
}
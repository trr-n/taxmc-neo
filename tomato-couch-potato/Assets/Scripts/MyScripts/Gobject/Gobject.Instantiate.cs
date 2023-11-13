#pragma warning disable IDE0002

using UnityEngine;

namespace trrne.Box
{
    public static partial class Gobject
    {
        public static T Instantiate<T>(this T g) where T : Object => GameObject.Instantiate(g);
        public static T Instantiate<T>(this T g, Vector3 p) where T : Object => GameObject.Instantiate(g, p, Quaternion.identity);
        public static T Instantiate<T>(this T g, Vector3 p, Quaternion r) where T : Object => GameObject.Instantiate(g, p, r);
        public static T Instantiate<T>(this T[] gs) where T : Object => GameObject.Instantiate(gs.Choice());
        public static T Instantiate<T>(this T[] gs, Vector3 p) where T : Object => GameObject.Instantiate(gs.Choice(), p, Quaternion.identity);
        public static T Instantiate<T>(this T[] gs, Vector3 p, Quaternion r) where T : Object => GameObject.Instantiate(gs.Choice(), p, r);

        public static T TryInstantiate<T>(this T g) where T : Object => g != null ? g.Instantiate() : null;
        public static T TryInstantiate<T>(this T g, Vector3 p) where T : Object => g != null ? g.Instantiate(p, Quaternion.identity) : null;
        public static T TryInstantiate<T>(this T g, Vector3 p, Quaternion r) where T : Object => g != null ? g.Instantiate(p, r) : null;
        public static T TryInstantiate<T>(this T[] gs) where T : Object => gs.Length > 0 ? gs.Instantiate() : null;
        public static T TryInstantiate<T>(this T[] gs, Vector3 p) where T : Object => gs.Length > 0 ? gs.Instantiate(p, Quaternion.identity) : null;
        public static T TryInstantiate<T>(this T[] gs, Vector3 p, Quaternion r) where T : Object => gs.Length > 0 ? gs.Instantiate(p, r) : null;
    }
}
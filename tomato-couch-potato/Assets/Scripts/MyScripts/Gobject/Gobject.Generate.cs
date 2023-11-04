#pragma warning disable IDE0002

using UnityEngine;

namespace trrne.Box
{
    public static partial class Gobject
    {
        public static T Generate<T>(this T g) where T : Object => GameObject.Instantiate(g);
        public static T Generate<T>(this T g, Vector3 p) where T : Object => GameObject.Instantiate(g, p, Quaternion.identity);
        public static T Generate<T>(this T g, Vector3 p, Quaternion r) where T : Object => GameObject.Instantiate(g, p, r);
        public static T Generate<T>(this T[] gs) where T : Object => GameObject.Instantiate(gs.Choice());
        public static T Generate<T>(this T[] gs, Vector3 p) where T : Object => GameObject.Instantiate(gs.Choice(), p, Quaternion.identity);
        public static T Generate<T>(this T[] gs, Vector3 p, Quaternion r) where T : Object => GameObject.Instantiate(gs.Choice(), p, r);

        public static T TryGenerate<T>(this T g) where T : Object => g != null ? g.Generate() : null;
        public static T TryGenerate<T>(this T g, Vector3 p) where T : Object => g != null ? g.Generate(p, Quaternion.identity) : null;
        public static T TryGenerate<T>(this T g, Vector3 p, Quaternion r) where T : Object => g != null ? g.Generate(p, r) : null;
        public static T TryGenerate<T>(this T[] gs) where T : Object => gs.Length > 0 ? gs.Generate() : null;
        public static T TryGenerate<T>(this T[] gs, Vector3 p) where T : Object => gs.Length > 0 ? gs.Generate(p, Quaternion.identity) : null;
        public static T TryGenerate<T>(this T[] gs, Vector3 p, Quaternion r) where T : Object => gs.Length > 0 ? gs.Generate(p, r) : null;
    }
}
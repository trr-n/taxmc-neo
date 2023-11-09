using System;
using UnityEngine;

namespace trrne.Box
{
    public static class Typing
    {
        public static T Cast<T>(this object obj) => (T)obj;
        public static Vector2 ToVec2(this Vector3 vector) => (Vector2)vector;
        public static Vector3 ToVec3(this Vector2 vector) => (Vector3)vector;
        public static Vector3 ToVec3(this Quaternion q) => q.eulerAngles;
        public static Quaternion ToQ(this Vector3 vector) => Quaternion.Euler(vector);
        public static Quaternion ToQ(this Vector2 vector) => Quaternion.Euler(vector);

        public static string ReplaceLump(this string target, in string[] befores, string after)
        {
            befores.ForEach(before => target = target.Replace(before, after));
            return target;
        }

        public static string Delete(this string target, string be) => target.Replace(be, "");
        public static string DeleteLump(this string basis, params string[] targets)
        {
            targets.ForEach(target => basis = basis.Delete(target));
            return basis;
        }

        public static bool Subclass(this object obj, Type t) => obj.GetType().IsSubclassOf(t);
        public static bool Subclass(this object[] objs, Type t) => objs.GetType().IsSubclassOf(t);

        public static string Join(this object[] objs, string sep) => string.Join(sep, objs);

        public static string Link(this char[] objs) => string.Join("", objs);
        public static string Link(this byte[] b) => string.Join("", b);
    }
}

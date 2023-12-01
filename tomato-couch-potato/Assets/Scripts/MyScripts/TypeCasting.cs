#pragma warning disable CS8509

using System;
using UnityEngine;

namespace trrne.Box
{
    public static class TypeCasting
    {
        public static Vector2 ToVec2(this Vector3 vector) => (Vector2)vector;

        public static Vector3 ToVec3(this Vector2 vector) => (Vector3)vector;
        public static Vector3 ToVec3(this Quaternion q)
        => new(2 * ((q.x * q.w) + (q.y * q.z)),
            2 * ((q.x * q.z) - (q.y * q.w)),
            2 * ((q.y * q.w) + (q.x * q.z)));

        public static Quaternion ToQ(this Vector3 vector) => Quaternion.Euler(vector);
        public static Quaternion ToQ(this Vector2 vector) => Quaternion.Euler(vector);

        public static string ToString(this bool boolean) => boolean ? "true" : "false";

        public static bool ToBoolean(this int i) => i switch { 0 => false, 1 => true };
        public static bool ToBoolean(this string str) => str switch { "false" => false, "true" => true };

        public static int ToInt(this bool boolean) => boolean ? 1 : 0;
        public static int ToInt(this string str) => int.Parse(str);
        public static int ToInt(this float single) => Maths.Cutail(single);

        public static float ToSingle(this bool boolean) => boolean ? 1f : 0f;
        public static float ToSingle(this string str) => float.Parse(str);

        public static T ToEnum<T>(int index) where T : Enum => (T)Enum.ToObject(typeof(T), index);
    }
}
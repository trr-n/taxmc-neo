#pragma warning disable CS8509

using System;
using UnityEngine;

namespace trrne.Box
{
    public static class TypeCasting
    {
        public static T Cast<T>(this object o) => (T)o;
        public static Vector2 ToV2(this Vector3 vector) => (Vector2)vector;
        public static Vector3 ToV3(this Vector2 vector) => (Vector3)vector;
        [Obsolete] public static Vector3 ToV3(this Quaternion q) => 2 * new Vector3((q.x * q.w) + (q.y * q.z), (q.x * q.z) - (q.y * q.w), (q.y * q.w) + (q.x * q.z));
        public static Quaternion ToQ(this Vector3 vector) => Quaternion.Euler(vector);
        public static Quaternion ToQ(this Vector2 vector) => Quaternion.Euler(vector);
        public static string ToStr(this bool boolean) => boolean ? "true" : "false";
        public static bool ToBool(this int i) => i switch { 0 => false, 1 => true };
        public static bool ToBool(this string str) => str.ToLower() switch { "true" => true, "false" => false };
        public static int ToInt(this bool boolean) => boolean ? 1 : 0;
        public static int ToInt(this string str) => int.Parse(str);
        public static int ToInt(this float single) => MF.Cutail(single);
        public static float ToFloat(this bool boolean) => boolean ? 1f : 0f;
        public static float ToFloat(this string str) => float.Parse(str);
        public static T ToEnum<T>(int index) where T : Enum => (T)Enum.ToObject(typeof(T), index);
    }
}
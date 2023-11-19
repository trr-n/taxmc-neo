using System;
using UnityEngine;

namespace trrne.Box
{
    public static class TypeCasting
    {
        public static Vector2 ToVec2(this Vector3 vector) => (Vector2)vector;

        public static Vector3 ToVec3(this Vector2 vector) => (Vector3)vector;
        public static Vector3 ToVec3(this Quaternion q) => q.eulerAngles;

        public static Quaternion ToQ(this Vector3 vector) => Quaternion.Euler(vector);
        public static Quaternion ToQ(this Vector2 vector) => Quaternion.Euler(vector);

        public static string ToString(this bool boo) => boo ? "true" : "false";

        public static int Cast(this object a, int? b = null) => (int)a;
        public static float Cast(this object a, float? b = null) => (float)a;
        public static string Cast(this object a, string b = null) => (string)a;
    }
}
using UnityEngine;

namespace trrne.Box
{
    public static class TypeCasting
    {
        public static Vector2 ToVec2(this Vector3 vector) => (Vector2)vector;

        public static Vector3 ToVec3(this Vector2 vector) => (Vector3)vector;
        public static Vector3 ToVec3(this Quaternion q)
        => new(x: 2 * ((q.x * q.w) + (q.y * q.z)), y: 2 * ((q.x * q.z) - (q.y * q.w)), z: 2 * ((q.y * q.w) + (q.x * q.z)));

        public static Quaternion ToQ(this Vector3 vector) => Quaternion.Euler(vector);
        public static Quaternion ToQ(this Vector2 vector) => Quaternion.Euler(vector);

        // from bool
        public static string ToString(this bool boolean) => boolean ? "true" : "false";
        public static int ToInt(this bool boolean) => boolean ? 1 : 0;
        public static float ToSingle(this bool boolean) => boolean ? 1f : 0f;

        // from string
        public static int ToInt(this string str) => int.Parse(str);
        public static float ToSingle(this string str) => float.Parse(str);
        public static bool ToString(this string str) => str.ToLower() switch
        {
            "true" => true,
            "false" => false,
            _ => default
        };

        // from float
        public static int ToInt(this float single) => Maths.Cutail(single);
    }
}
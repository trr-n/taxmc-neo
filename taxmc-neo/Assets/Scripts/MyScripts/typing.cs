using System;
using UnityEngine;

namespace trrne.Bag
{
    public static class Typing
    {
        #region Casting
        public static T Cast<T>(this object obj) => (T)obj;
        public static Vector2 ToVec2(this Vector3 vector) => (Vector2)vector;
        #endregion

        /// <summary>
        /// 置換したい文字をスペース二つはさんで記述<br/><br/>
        /// Ex(XYZ -> BBB):<br/>
        /// ("XYZ").ReplaceLump("X  Y  Z", B);<br/>
        /// output: "BBB"
        /// </summary>
        public static string ReplaceLump(this string target, string before, string after)
        {
            string[] pres = before.Split("  ");

            for (int count = 0; count < pres.Length; count++)
            {
                target = target.Replace(pres[count], after);
            }

            return target;
        }

        [Obsolete]
        public static bool Subclass(this object obj, Type t) => obj.GetType().IsSubclassOf(t);
        public static bool Subclass(this object[] objs, Type t) => objs.GetType().IsSubclassOf(t);

        public static string Join(this object[] objs, string sep) => string.Join(sep, objs);
        public static string Link(this char[] objs) => string.Join("", objs);
    }
}

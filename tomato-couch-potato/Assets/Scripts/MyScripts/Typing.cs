using System;
using UnityEngine;

namespace Chickenen.Pancreas
{
    public static class Typing
    {
        public static T Cast<T>(this object obj)
        {
            return (T)obj;
        }

        public static Vector2 ToVec2(this Vector3 vector)
        {
            return (Vector2)vector;
        }

        public static Vector3 ToVec3(this Vector2 vector)
        {
            return (Vector3)vector;
        }

        /// <summary>
        /// 置換したい文字をスペース二つはさんで記述<br/><br/>
        /// Ex(XYZ -> BBB):<br/>
        /// ("XYZ").ReplaceLump("X  Y  Z", B);<br/>
        /// output: "BBB"
        /// </summary>
        [Obsolete]
        public static string ReplaceLump(this string target, in string before, string after)
        {
            string[] pres = before.Split("  ");

            for (int i = 0; i < pres.Length; i++)
            {
                target = target.Replace(pres[i], after);
            }

            return target;
        }

        public static string ReplaceLump(this string target, in string[] befores, string after)
        {
            befores.ForEach(before => target = target.Replace(before, after));
            return target;
        }

        /// <summary>
        /// 文字列から指定の文字を削除する
        /// </summary>
        public static string Delete(this string target, string be)
        {
            return target.Replace(be, "");
        }

        /// <summary>
        /// basisからtargetsを削除
        /// </summary>
        public static string DeleteLump(this string basis, params string[] targets)
        {
            targets.ForEach(target => basis = basis.Delete(target));
            return basis;
        }

        public static bool Subclass(this object obj, Type t)
        {
            return obj.GetType().IsSubclassOf(t);
        }

        public static bool Subclass(this object[] objs, Type t)
        {
            return objs.GetType().IsSubclassOf(t);
        }

        public static string Join(this object[] objs, string sep)
        {
            return string.Join(sep, objs);
        }

        public static string Link(this char[] objs)
        {
            return string.Join("", objs);
        }
    }
}

using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace trrne.Box
{
    public static class Shorthand
    {
        public static void If(this bool boolean, Action action) { if (boolean) { action(); } }
        public static void If<T>(this bool boolean, Func<T> func) { if (boolean) { func(); } }

        /// <summary>1 line</summary>
        public static T L1ne<T>(Func<T> func) => func();

        /// <summary>
        /// t1とt2をがっちゃんこする
        /// </summary>
        public static IEnumerable<(T1, T2)> Zip<T1, T2>(T1[] t1, T2[] t2)
        => t1.SelectMany(t11 => t2.Select(t22 => (t11, t22)));
        public static IEnumerable<(T1, T2)> Zip<T1, T2>(List<T1> t1, List<T2> t2)
        => Zip(t1.ToArray(), t2.ToArray());

        public static void ForEach<T>(this T[] array, Action<T> action) => Array.ForEach(array, action);
        public static void ForEach<T>(this IEnumerable<T> array, Action<T> action) => Array.ForEach(array.ToArray(), action);

        public static bool None(params object[] objs)
        => (from obj in objs where obj is null select obj).ToArray().Length >= 1;

        /// <summary>
        /// obj != nullだったらactionを実行する
        /// </summary>
        public static void NotVacant(this object obj, Action action) => If(obj != null, action);

        public static T[] Set<T>(this T[] array, T t)
        {
            for (int i = 0; i < array.Length; ++i)
            {
                array[i] = t;
            }
            return array;
        }

        public static void DrawRay(this Ray ray, float length, Color color)
        => Debug.DrawRay(ray.origin, ray.direction * length, color);
    }
}

// https://baba-s.hatenablog.com/entry/2020/01/10/090000
// https://qiita.com/t_takahari/items/6dc72f48b1ebdfed93b7
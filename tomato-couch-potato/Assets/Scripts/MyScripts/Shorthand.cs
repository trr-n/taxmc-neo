using System;
using System.Linq;
using System.Collections.Generic;

namespace trrne.Box
{
    public static partial class Shorthand
    {
        public static void If(this bool boolean, Action action) { if (boolean) { action(); } }
        public static void If<T>(this bool boolean, Func<T> func) { if (boolean) { func(); } }

        /// <summary>
        /// write a line
        /// </summary>
        public static T L1ne<T>(Func<T> func) => func();

        public static void Loop<T>(int loop, Func<T> func)
        {
            for (int i = 0; i < loop; ++i)
            {
                func();
            }
        }

        /// <summary>
        /// t1とt2をがっちゃんこする
        /// </summary>
        public static IEnumerable<(T1, T2)> Zip<T1, T2>(T1[] a, T2[] b) => a.SelectMany(t11 => b.Select(t22 => (t11, t22)));
        public static IEnumerable<(T1, T2)> Zip<T1, T2>(List<T1> a, List<T2> b) => Zip(a.ToArray(), b.ToArray());

        public static void ForEach<T>(this T[] array, Action<T> action) => Array.ForEach(array, action);
        public static void ForEach<T>(this IEnumerable<T> array, Action<T> action) => Array.ForEach(array.ToArray(), action);

        public static bool None(params object[] objs) => (from obj in objs where obj is null select obj).ToArray().Length >= 1;

        /// <summary>
        /// obj != nullだったらactionを実行する
        /// </summary>
        public static void NotVacant(this object obj, Action action) => If(obj != null, action);

        /// <summary>
        /// 要素一括置換 -> <b>T[]</b><br/>
        /// </summary>
        /// <typeparam name="T">型</typeparam>
        /// <param name="array">置換したい配列</param>
        /// <param name="t">置換したい要素</param>
        public static T[] Set<T>(this T[] array, T t)
        {
            for (int i = 0; i < array.Length; ++i)
            {
                array[i] = t;
            }
            return array;
        }
        public static List<T> Set<T>(this List<T> list, T t) => list.ToArray().Set(t).ToList();
        public static bool not(this bool boo) => !boo;
    }
}

// https://baba-s.hatenablog.com/entry/2020/01/10/090000
// https://qiita.com/t_takahari/items/6dc72f48b1ebdfed93b7
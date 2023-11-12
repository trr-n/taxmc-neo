using System;
using System.Linq;
using System.Collections.Generic;

namespace trrne.Box
{
    public static class Shorthand
    {
        public static void If(this bool boo, Action action) { if (boo) { action(); } }
        public static void If<T>(this bool boo, Func<T> func) { if (boo) { func(); } }

        /// <summary>
        /// t1とt2をがっちゃんこする
        /// </summary>
        public static IEnumerable<(T1, T2)> Zip<T1, T2>(T1[] t1, T2[] t2)
        => t1.SelectMany(t11 => t2.Select(t22 => (t11, t22)));

        public static IEnumerable<(T1, T2)> Zip<T1, T2>(List<T1> t1, List<T2> t2) => Zip(t1.ToArray(), t2.ToArray());

        public static void ForEach<T>(this T[] array, Action<T> action) => Array.ForEach(array, action);
        public static void ForEach<T>(this IEnumerable<T> array, Action<T> action) => Array.ForEach(array.ToArray(), action);

        public static bool None(params object[] objs) => (from o in objs where o is null select o).ToArray().Length >= 1;

        /// <summary>
        /// obj != nullだったらactionを実行する
        /// </summary>
        public static void NotVacant(this object obj, Action action) => If(obj != null, action);
    }
}

// https://baba-s.hatenablog.com/entry/2020/01/10/090000
// https://qiita.com/t_takahari/items/6dc72f48b1ebdfed93b7
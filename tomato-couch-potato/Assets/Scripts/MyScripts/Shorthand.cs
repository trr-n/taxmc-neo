using System;
using System.Linq;
using System.Collections.Generic;

namespace trrne.Box
{
    public static class Shorthand
    {
        public static void BoolAction(this bool boo, Action action) { if (boo) { action(); } }
        public static void BoolFunc<T>(this bool boo, Func<T> func) { if (boo) { func(); } }

        [Obsolete]
        public static T Function<T>(Func<T> func) => func();

        /// <summary>
        /// ｵｫｰﾝ…ｫｫｵｵｫｫｫﾝﾝ‼‼‼(ｶﾞﾁｬｺﾝ)
        /// </summary>
        // https://baba-s.hatenablog.com/entry/2020/01/10/090000
        public static IEnumerable<(T1, T2)> Zip<T1, T2>(T1[] t1, T2[] t2)
        => t1.SelectMany(t11 => t2.Select(t22 => (t11, t22)));

        public static IEnumerable<(T1, T2)> Zip<T1, T2>(List<T1> t1, List<T2> t2) => Zip(t1.ToArray(), t2.ToArray());

        // リスト以外でも使えるように
        public static void ForEach<T>(this T[] array, Action<T> action) => Array.ForEach(array, action);

        // https://qiita.com/t_takahari/items/6dc72f48b1ebdfed93b7
        public static bool IsNull(params object[] objs)
        {
            // nullだけつまみだす
            var nones = (from o in objs where o is null select o).ToArray();
            // 要素数が1以上か
            return nones.Length >= 1;
        }
    }
}
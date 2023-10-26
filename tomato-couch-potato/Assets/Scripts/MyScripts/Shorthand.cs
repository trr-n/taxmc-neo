using System;
using System.Linq;
using System.Collections.Generic;

namespace trrne.Pancreas
{
    public static class Shorthand
    {
        public static void BoolAction(bool boo, Action action)
        {
            if (boo)
            {
                action();
            }
        }

        public static T Function<T>(Func<T> func) => func();

        /// <summary>
        /// ｵｫｰﾝ…ｫｫｵｵｫｫｫﾝﾝ‼‼‼(ｶﾞﾁｬｺﾝ)
        /// </summary>
        // https://baba-s.hatenablog.com/entry/2020/01/10/090000
        public static IEnumerable<(T1, T2)> Merge<T1, T2>(T1[] t1, T2[] t2)
        => t1.SelectMany(t11 => t2.Select(t22 => (t11, t22)));

        /// <summary>
        /// リスト以外でも使えるように
        /// </summary>
        public static void ForEach<T>(this T[] array, Action<T> action) => Array.ForEach(array, action);

        public static bool IsNull(params object[] objs)
        {
            foreach (var o in objs)
            {
                if (o == null)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
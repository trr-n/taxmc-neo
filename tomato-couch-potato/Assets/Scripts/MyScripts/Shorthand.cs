using System;
using System.Linq;
using System.Collections.Generic;

namespace Chickenen.Pancreas
{
    public struct Shorthand
    {
        public static void BoolAction(bool boo, Action action)
        {
            if (boo)
            {
                action();
            }
        }

        public static T BoolAction<T>(bool boo, Func<T> action)
        {
            return boo ? action() : default;
        }

        public static T Function<T>(Func<T> func)
        {
            return func();
        }

        /// <summary>
        /// ｵｫｰﾝ…ｫｫｵｵｫｫｫﾝﾝ‼‼‼(ｶﾞﾁｬｺﾝ)
        /// </summary>
        // https://baba-s.hatenablog.com/entry/2020/01/10/090000
        public static IEnumerable<(T1, T2)> Merge<T1, T2>(T1[] t1, T2[] t2)
        {
            return t1.SelectMany(t11 => t2.Select(t22 => (t11, t22)));
        }
    }

    public static class ShorthandXT
    {
        /// <summary>
        /// リスト以外でも使えるように
        /// </summary>
        public static void ForEach<T>(this T[] array, Action<T> action)
        {
            Array.ForEach(array, action);
        }
    }
}
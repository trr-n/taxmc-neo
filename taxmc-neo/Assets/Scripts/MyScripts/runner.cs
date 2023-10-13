using System;
using System.Collections.Generic;
using System.Linq;

namespace trrne.Bag
{
    public class Runner
    {
        bool runonce_flag;
        /// <summary>
        /// actionを一回実行
        /// </summary>
        public void RunOnce(params Action[] actions)
        {
            Shorthand.BoolAction(!runonce_flag, () =>
            {
                actions.ForEach(action => action());
                runonce_flag = true;
            });
        }

        readonly static Stopwatch bookingSW = new(true);
        public static void Book(float time, Action action)
        {
            if (bookingSW.sf >= time)
            {
                action();
                bookingSW.Rubbish();
            }
        }
    }

    public static class Shorthand
    {
        public static void BoolAction(bool boo, Action action)
        {
            if (boo)
            {
                action();
            }
        }

        public static void BoolAction(bool boo, Action o, Action x)
        {
            BoolAction(boo, o);
            BoolAction(!boo, x);
        }

        public static T Function<T>(Func<T> func)
        {
            return func();
        }

        /// <summary>
        /// リスト以外でも使えるように
        /// </summary>
        public static void ForEach<T>(this T[] array, Action<T> action)
        {
            Array.ForEach(array, action);
        }

        /// <summary>
        /// ｵｫｰﾝ…ｫｫｵｵｫｫｫﾝﾝ‼‼‼(ｶﾞﾁｬｺﾝ)
        /// </summary>
        // https://baba-s.hatenablog.com/entry/2020/01/10/090000
        public static IEnumerable<(T1, T2)> Merge<T1, T2>(this T1[] t1, T2[] t2)
        {
            return t1.SelectMany(t11 => t2.Select(t22 => (t11, t22)));
        }

        // /// <summary>
        // /// さんこがっちゃんこ
        // /// </summary>
        // public static IEnumerable<(T1, T2, T3)> Merge<T1, T2, T3>(this T1[] t1, T2[] t2, T3[] t3)
        // {
        //     return t1.SelectMany(name => t2.SelectMany(age => t3.Select(gender => (name, age, gender))));
        // }
    }
}
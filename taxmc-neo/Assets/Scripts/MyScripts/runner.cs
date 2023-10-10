using System;
using System.Collections.Generic;
using System.Linq;

namespace trrne.Bag
{
    public class Runner
    {
        bool flag0;
        /// <summary>
        /// actionを一回実行
        /// </summary>
        public void RunOnce(params Action[] actions)
        {
            Shorthand.BoolAction(!flag0, () =>
            {
                Array.ForEach(actions, action => action());
                flag0 = true;
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

        public static void NothingSpecial() { return; }
    }

    public static class Shorthand
    {
        public static void BoolAction(bool boo, Action action) { if (boo) { action(); } }
        public static T Function<T>(Func<T> func) => func();
        public static void ForEach<T>(this T[] array, Action<T> action) => Array.ForEach(array, action);

        /// <summary>
        /// ｵｫｰﾝ…ｫｫｵｵｫｫｫﾝﾝ‼‼‼
        /// </summary>
        // https://baba-s.hatenablog.com/entry/2020/01/10/090000
        public static IEnumerable<(OO, QQ)> Merge<OO, QQ>(this OO[] oo, QQ[] qq) => oo.SelectMany(o => qq.Select(q => (o, q)));
    }
}
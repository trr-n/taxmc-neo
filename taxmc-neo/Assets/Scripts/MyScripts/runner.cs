using System;
using System.Collections;
using UnityEngine;

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
            SimpleRunner.BoolAction(!flag0, () =>
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

    public static class SimpleRunner
    {
        public static void BoolAction(bool pass, Action action) { if (pass) { action(); } }
        public static T Function<T>(Func<T> func) => func();

        public static void ForEach<T>(this T[] array, Action<T> action) => Array.ForEach(array, action);
    }
}
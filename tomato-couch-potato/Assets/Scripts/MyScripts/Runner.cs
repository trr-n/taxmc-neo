using System;

namespace trrne.Box
{
    public sealed class Runner
    {
        bool runonce_flag;
        /// <summary>
        /// actionを一回実行
        /// </summary>
        public void RunOnce(params Action[] actions)
        {
            Simple.If(!runonce_flag, () =>
            {
                actions.ForEach(action => action());
                runonce_flag = true;
            });
        }

        readonly static Stopwatch bookingSW = new(true);
        public static void Book(float time, Action action)
        {
            if (bookingSW.Sf >= time)
            {
                action();
                bookingSW.Rubbish();
            }
        }
    }
}
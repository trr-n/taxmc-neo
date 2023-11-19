using System;

namespace trrne.Box
{
    public sealed class Runner
    {
        bool runonce_flag;
        /// <summary>
        /// actionを一回実行
        /// </summary>
        public void RunOnce(params Action[] actions) =>
        (!runonce_flag).If(() =>
        {
            actions.ForEach(action => action());
            runonce_flag = true;
        });

        readonly static Stopwatch bookingSW = new(true);
        public static void Book(float time, Action action)
        => (bookingSW.Secondf() >= time).If(() =>
        {
            action();
            bookingSW.Rubbish();
        });
    }
}
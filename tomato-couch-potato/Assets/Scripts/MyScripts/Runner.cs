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
            if (!runonce_flag)
            {
                actions.ForEach(action => action());
                runonce_flag = true;
            }
        }
    }
}
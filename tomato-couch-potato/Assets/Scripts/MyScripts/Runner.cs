using System;

namespace trrne.Box
{
    public sealed class Runner
    {
        bool runonce_flag;

        /// <summary>
        /// 一度だけ実行する
        /// </summary>
        /// <param name="actions">実行する処理</param>
        public void RunOnce(params Action[] actions)
        {
            if (runonce_flag)
            {
                return;
            }
            actions.ForEach(action => action());
            runonce_flag = true;
        }
    }
}
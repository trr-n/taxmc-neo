using trrne.Box;
using UnityEngine;

namespace trrne.Brain
{
    public class TimeManager : MonoBehaviour
    {
        readonly Stopwatch sw = new();

        /// <summary>
        /// 現在のタイム
        /// </summary>
        public (int Minutes, int Seconds) Current => (sw.M, sw.S);
        public string CurrentSTR => sw.Spent(StopwatchOutput.MS);

        /// <summary>
        /// 動いているか
        /// </summary>
        public bool IsRunning() => sw.IsRunning();

        /// <summary>
        /// タイマーをスタート
        /// </summary>
        public void Start() => sw.Start();

        /// <summary>
        /// タイマーをストップ
        /// </summary>
        public void Stop() => sw.Stop();

        /// <summary>
        /// タイマーをリスタート
        /// </summary>
        public void Restart() => sw.Restart();

        /// <summary>
        /// タイマーをリセット
        /// </summary>
        public void Reset() => sw.Reset();
    }
}

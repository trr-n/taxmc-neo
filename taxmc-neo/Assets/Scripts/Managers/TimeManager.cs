using trrne.Body;
using trrne.Bag;
using UnityEngine;

namespace trrne.Brain
{
    public class TimeManager : MonoBehaviour
    {
        readonly Stopwatch sw = new();

        /// <summary>
        /// 現在のタイム
        /// </summary>
        public (int minutes, int seconds) Current => (sw.m, sw.s);
        public string CurrentSTR => sw.Spent(StopwatchOutput.MS);

        /// <summary>
        /// 動いているか
        /// </summary>
        public bool IsRunning() => sw.isRunning;

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

        /// <summary>
        /// 記録を出力
        /// </summary>
        public void Record()
        {
            Stop();

            Save.Write(
                data: new SaveData { time = (Current.minutes, Current.seconds) },
                password: "rid456",
                path: Application.dataPath + "/save" + Temps.raw + ".sav"
            );
        }
    }
}

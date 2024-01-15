using trrne.Box;
using UnityEngine;

namespace trrne.Brain
{
    public class TimeManager : MonoBehaviour
    {
        readonly Stopwatch stopwatch = new();

        public int CurrentTimeMinutes => stopwatch.minute;
        public int CurrentTimeSeconds => stopwatch.second;
        public string CurrentTimeStr => stopwatch.Spent(StopwatchOutputFormat.MS);

        /// <summary> 動いているか </summary>
        public bool IsRunning() => stopwatch.IsRunning();

        /// <summary> タイマーをスタート </summary>
        public void Start() => stopwatch.Start();

        /// <summary> タイマーをストップ </summary>
        public void Stop() => stopwatch.Stop();

        /// <summary> タイマーをリスタート </summary>
        public void Restart() => stopwatch.Restart();

        /// <summary> タイマーをリセット </summary>
        public void Reset() => stopwatch.Reset();
    }
}

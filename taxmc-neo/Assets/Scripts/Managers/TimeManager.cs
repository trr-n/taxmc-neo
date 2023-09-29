using System.Collections;
using System.Collections.Generic;
using trrne.Bag;
using UnityEngine;

namespace trrne.Body
{
    public class TimeManager : MonoBehaviour
    {
        readonly Stopwatch sw = new();

        /// <summary>
        /// 現在のタイム
        /// </summary>
        public (int minutes, int seconds) Current => (sw.m, sw.s);
        public string Currentstr => sw.Spent(StopwatchSpentOutput.MS);

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

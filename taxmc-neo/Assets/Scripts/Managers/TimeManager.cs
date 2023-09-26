using System.Collections;
using System.Collections.Generic;
using trrne.Bag;
using UnityEngine;

namespace trrne.Body
{
    public class TimeManager : MonoBehaviour
    {
        readonly Stopwatch stopwatch = new();

        /// <summary>
        /// 現在のタイム
        /// </summary>
        public float current => stopwatch.s;

        /// <summary>
        /// タイマーをスタート
        /// </summary>
        public void Start() => stopwatch.Start();

        /// <summary>
        /// タイマーをストップ
        /// </summary>
        public void Stop() => stopwatch.Stop();

        /// <summary>
        /// タイマーをリスタート
        /// </summary>
        public void Restart() => stopwatch.Restart();

        /// <summary>
        /// タイマーをリセット
        /// </summary>
        public void Reset() => stopwatch.Reset();
    }
}

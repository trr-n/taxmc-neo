using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace trrne.Heart
{
    public class Health : MonoBehaviour
    {
        [SerializeField, Min(1)]
        int max = 1;
        int left;

        /// <summary>
        /// 最大、現在
        /// </summary>
        public (int max, int left) lives => (max, left);

        /// <summary>
        /// 残機が0か
        /// </summary>
        public bool isZero => left <= 0;

        /// <summary>
        /// amount分残機数を変動させる
        /// </summary>
        public void Fluctuation(int amount)
        {
            if (isZero) { return; }

            left += amount;
            left = Mathf.Clamp(left, 0, max);
        }

        /// <summary>
        /// 残機を最大に設定
        /// </summary>
        public void Reset() => left = max;

        /// <summary>
        /// 残機数の上限をlatestに変更
        /// </summary>
        public void ChangeMax(int latest) => max = latest;
    }
}

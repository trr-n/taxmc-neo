using UnityEngine;

namespace trrne.Core
{
    public class Health : MonoBehaviour
    {
        [SerializeField, Min(1)]
        int max = 1;

        /// <summary>
        /// 最大、現在
        /// </summary>
        public int Max => max;
        public int Left { get; private set; }

        /// <summary>
        /// 残機が0か
        /// </summary>
        public bool IsZero => Left <= 0;

        /// <summary>
        /// amount分残機数を変動させる
        /// </summary>
        public void Fluctuation(int amount)
        {
            if (!IsZero)
            {
                Left += amount;
                Left = Mathf.Clamp(Left, 0, max);
            }
        }

        /// <summary>
        /// 残機を最大に設定
        /// </summary>
        public void Reset() => Left = max;

        /// <summary>
        /// 残機数の上限をlatestに変更
        /// </summary>
        public void ChangeMax(int latest) => max = latest;
    }
}

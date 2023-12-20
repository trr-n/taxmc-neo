using UnityEngine;

namespace trrne.Core
{
    public class LeverFlag : MonoBehaviour
    {
        /// <summary>
        /// プレイヤーが範囲内にいるか
        /// </summary>
        public bool Hit { get; private set; }

        void OnTriggerEnter2D(Collider2D info)
        {
            if (info.TryGetComponent(out Player _))
            {
                Hit = true;
            }
        }

        void OnTriggerExit2D(Collider2D info)
        {
            if (info.TryGetComponent(out Player _))
            {
                Hit = false;
            }
        }
    }
}

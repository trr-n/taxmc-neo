using UnityEngine;

namespace trrne.Core
{
    public class LeverFlag : MonoBehaviour
    {
        /// <summary>
        /// プレイヤーが範囲内にいるか
        /// </summary>
        public bool IsHitting { get; private set; }

        void OnTriggerEnter2D(Collider2D info)
        {
            if (info.TryGetComponent(out Player _))
            {
                IsHitting = true;
            }
        }

        void OnTriggerExit2D(Collider2D info)
        {
            if (info.TryGetComponent(out Player _))
            {
                IsHitting = false;
            }
        }
    }
}

using UnityEngine;

namespace trrne.Core
{
    public class CarrotFlag : MonoBehaviour
    {
        public int Count { get; private set; }
        public void ResetCount() => Count = 0;

        void OnTriggerEnter2D(Collider2D info)
        {
            if (info.CompareTag(Config.Tags.PLAYER))
            {
                ++Count;
            }
        }
    }
}
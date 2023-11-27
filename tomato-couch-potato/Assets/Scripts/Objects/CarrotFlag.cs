using UnityEngine;

namespace trrne.Core
{
    public class CarrotFlag : MonoBehaviour
    {
        public int Count { get; set; }

        void OnTriggerEnter2D(Collider2D info)
        {
            if (info.CompareTag(Constant.Tags.Player))
            {
                ++Count;
            }
        }
    }
}
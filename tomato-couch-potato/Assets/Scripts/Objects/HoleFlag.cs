using trrne.Pancreas;
using UnityEngine;

namespace trrne.Heart
{
    public class HoleFlag : MonoBehaviour
    {
        public int Count { get; set; }

        void OnTriggerEnter2D(Collider2D info)
        {
            if (info.CompareTag(Constant.Tags.Player))
            {
                Count++;
            }
        }
    }
}
using trrne.Box;
using UnityEngine;

namespace trrne.Core
{
    public class ButtonFlag : MonoBehaviour
    {
        public bool IsHit { get; private set; }

        void OnTriggerEnter2D(Collider2D info)
        {
            if (info.CompareLayer(Config.Layers.Player))
            {
                IsHit = true;
            }
        }

        void OnTriggerExit2D(Collider2D info)
        {
            if (info.CompareLayer(Config.Layers.Player))
            {
                IsHit = false;
            }
        }
    }
}

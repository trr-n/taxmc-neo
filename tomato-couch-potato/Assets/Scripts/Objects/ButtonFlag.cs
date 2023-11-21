using trrne.Box;
using UnityEngine;

namespace trrne.Core
{
    public class ButtonFlag : MonoBehaviour
    {
        public bool IsHitting { get; private set; }

        void OnTriggerEnter2D(Collider2D info)
        {
            if (info.CompareLayer(Constant.Layers.Player))
                IsHitting = true;
        }

        void OnTriggerExit2D(Collider2D info)
        {
            if (info.CompareLayer(Constant.Layers.Player))
                IsHitting = false;
        }
    }
}

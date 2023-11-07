using trrne.Box;
using UnityEngine;

namespace trrne.Core
{
    public class ButtonFlag : MonoBehaviour
    {
        public bool Hit { get; private set; }

        void OnTriggerEnter2D(Collider2D info)
        {
            if (info.CompareLayer(Constant.Layers.Player))
            {
                Hit = true;
            }
        }

        void OnTriggerExit2D(Collider2D info)
        {
            if (info.CompareLayer(Constant.Layers.Player))
            {
                Hit = false;
            }
        }
    }
}

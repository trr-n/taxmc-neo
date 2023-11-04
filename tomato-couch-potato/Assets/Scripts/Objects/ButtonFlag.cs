using trrne.Box;
using UnityEngine;

namespace trrne.Core
{
    public class ButtonFlag : MonoBehaviour
    {
        bool hit;
        public bool Hit => hit;

        void OnTriggerEnter2D(Collider2D info)
        {
            if (info.CompareLayer(Constant.Layers.Player))
            {
                hit = true;
            }
        }

        void OnTriggerExit2D(Collider2D info)
        {
            if (info.CompareLayer(Constant.Layers.Player))
            {
                hit = false;
            }
        }
    }
}

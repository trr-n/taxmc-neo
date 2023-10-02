using trrne.Bag;
using UnityEngine;

namespace trrne.Body
{
    public class PlayerFlag : MonoBehaviour
    {
        bool hitting;
        public bool isHit => hitting;

        bool ice;
        public bool onIce => ice;

        void OnTriggerEnter2D(Collider2D info)
        {
            hitting = true;

            if (info.CompareLayer(Constant.Layers.Ground) && info.CompareTag(Constant.Tags.Ice))
            {
                ice = true;
            }
        }

        void OnTriggerExit2D(Collider2D info)
        {
            hitting = false;

            if (info.CompareLayer(Constant.Layers.Ground) && info.CompareTag(Constant.Tags.Ice))
            {
                ice = false;
            }
        }
    }
}
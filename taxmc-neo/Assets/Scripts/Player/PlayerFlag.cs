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

        void OnTriggerExit2D(Collider2D info)
        {
            Boolean(info, false);
        }

        void OnTriggerStay2D(Collider2D info)
        {
            Boolean(info, true);
        }

        void Boolean(Collider2D info, bool boo)
        {
            hitting = boo;

            if (info.CompareLayer(Constant.Layers.Ground) && info.CompareTag(Constant.Tags.Ice))
            {
                ice = boo;
            }
        }
    }
}
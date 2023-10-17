using trrne.Pancreas;
using UnityEngine;

namespace trrne.Heart
{
    public class PlayerFlag : MonoBehaviour
    {
        bool isHit;
        public bool IsHit => isHit;

        bool onIce;
        public bool OnIce => onIce;

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
            isHit = boo;

            if (info.CompareLayer(Constant.Layers.Ground) && info.CompareTag(Constant.Tags.Ice))
            {
                onIce = boo;
            }
        }
    }
}
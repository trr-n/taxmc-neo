using trrne.Box;
using UnityEngine;

namespace trrne.Core
{
    public class PlayerFlag : MonoBehaviour
    {
        public bool OnGround { get; private set; }
        public bool OnIce { get; private set; }

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
            if (info.CompareLayer(Constant.Layers.Jumpable))
                OnGround = boo;
            if (info.CompareTag(Constant.Tags.Ice))
                OnIce = boo;
        }
    }
}
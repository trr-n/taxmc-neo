using trrne.Box;
using UnityEngine;

namespace trrne.Core
{
    public class PlayerFlag : MonoBehaviour
    {
        public bool OnGround { get; private set; }
        public bool OnIce { get; private set; }

        Player player;

        void Start()
        {
            player = Gobject.GetComponentWithTag<Player>(Constant.Tags.Player);
        }

        void OnTriggerExit2D(Collider2D info) => Boolean(info, false);

        void OnTriggerStay2D(Collider2D info) => Boolean(info, true);

        void Boolean(Collider2D info, bool boo)
        {
            OnGround = boo;
            if (info.CompareTag(Constant.Tags.Ice))
            {
                OnIce = boo;
            }
        }
    }
}
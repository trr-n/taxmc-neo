using UnityEngine;
using trrne.Box;

namespace trrne.Core
{
    public class FeedLeft : Item
    {
        [SerializeField]
        bool up = true;

        protected override void Receive()
        {
            if (Gobject.BoxCast2D(out var hit, transform.position, SR.bounds.size, Constant.Layers.Player)
                && hit.TryGet(out Health health))
            {
                // 残機+1
                health.Fluctuation(up ? +1 : -1);
            }

            // 破壊
            Destroy(gameObject);
        }
    }
}

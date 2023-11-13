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
            if (Gobject.BoxCast(out var hit, transform.position, SR.bounds.size, Constant.Layers.Player)
                && hit.TryGetComponent(out Health health))
            {
                // 残機+1
                health.Fluctuation(up ? +1 : -1);
            }

            // 破壊
            Destroy(gameObject);
        }
    }
}

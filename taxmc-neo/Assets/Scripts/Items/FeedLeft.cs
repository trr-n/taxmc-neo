using UnityEngine;
using trrne.WisdomTeeth;

namespace trrne.Body
{
    public class FeedLeft : Item
    {
        [SerializeField]
        bool up = true;

        protected override void Receive()
        {
            if (Gobject.BoxCast2D(out var hit, transform.position, sr.bounds.size, Constant.Layers.Player)
                && hit.TryGet<Health>(out var health))
            {
                // 残機+1
                health.Fluctuation(up ? +1 : -1);
            }

            // 破壊
            Destroy(gameObject);
        }
    }
}

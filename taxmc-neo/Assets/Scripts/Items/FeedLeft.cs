using UnityEngine;
using trrne.Bag;

namespace trrne.Body
{
    public class FeedLeft : Item
    {
        [SerializeField]
        [Tooltip("")]
        bool up = true;

        protected override void Receive()
        {
            if (Gobject.BoxCast2D(out var hit, transform.position, sr.bounds.size, Fixed.Layers.Player)
                && hit.Try<Health>(out var health))
            {
                // 残機+1
                health.Fluctuation(up ? +1 : -1);
            }

            // 破壊
            Destroy(gameObject);
        }
    }
}

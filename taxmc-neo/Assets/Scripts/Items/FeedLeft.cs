using System.Collections;
using System.Collections.Generic;
using trrne.utils;
using UnityEngine;

namespace trrne.Game
{
    public class FeedLeft : Item
    {
        protected override void Receive()
        {
            if (Gobject.BoxCast2D(out var hit, transform.position, sr.bounds.size, Constant.Layers.Player))
            {
                if (hit.Try<Health>(out var health))
                {
                    health.Fluctuation(+1);
                }
            }
        }
    }
}

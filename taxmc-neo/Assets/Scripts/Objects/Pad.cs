using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using trrne.Bag;

namespace trrne.Body
{
    public class Pad : Objectt
    {
        protected override void Behavior()
        {
            if (Gobject.BoxCast2D(out var hit, transform.position, size, Fixed.Layers.Player | Fixed.Layers.Creature))
            {
                var rb = hit.Get<Rigidbody2D>();
                float power = rb.mass * 3;

                rb.AddForce(power * Coordinate.y, ForceMode2D.Impulse);

                // hit.Get<Rigidbody2D>().AddForce(power * Coordinate.y, ForceMode2D.Impulse);
            }
        }
    }
}
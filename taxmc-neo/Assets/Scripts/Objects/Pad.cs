using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using trrne.Bag;

namespace trrne.Body
{
    public class Pad : Objectt
    {
        readonly float power = 5f;

        protected override void Behavior()
        {
            if (Gobject.BoxCast2D(out var hit, transform.position, size, Fixed.Layers.Player | Fixed.Layers.Entity))
            {
                // print("hit.");
                hit.Get<Rigidbody2D>().AddForce(power * Coordinate.y, ForceMode2D.Impulse);
            }
        }
    }
}
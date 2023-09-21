using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using trrne.utils;

namespace trrne.Game
{
    public class Pad : Objectt
    {
        float power = 5f;

        protected override void Behaviour()
        {
            if (Gobject.BoxCast2D(out var hit, transform.position, size))
            {
                var rb = hit.Get<Rigidbody2D>();
                rb.AddForce(power * Coordinate.y, ForceMode2D.Impulse);
            }
        }
    }
}
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using trrne.Bag;
using Unity.Collections;

namespace trrne.Body
{
    public class Pad : Objectt
    {
        [SerializeField]
        [Range(5f, float.MaxValue)]
        float power = 10f;

        protected override void Behavior() => Runner.NothingSpecial();

        void OnCollisionEnter2D(Collision2D info)
        {
            if (info.Try(out Rigidbody2D rb))
            {
                rb.AddForce(rb.mass * power * Coordinate.y, ForceMode2D.Impulse);
            }
        }
    }
}
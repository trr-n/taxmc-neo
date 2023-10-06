using UnityEngine;
using trrne.Bag;

namespace trrne.Body
{
    public class Pad : Objectt
    {
        PadCore core;

        protected override void Start()
        {
            base.Start();
            core = transform.GetFromParent<PadCore>();
        }

        protected override void Behavior() { }

        void OnCollisionEnter2D(Collision2D info)
        {
            if (info.TryGet(out Rigidbody2D rb))
            {
                rb.velocity += rb.mass * core.power * Vector100.y2d * Time.fixedDeltaTime;
            }
        }
    }
}
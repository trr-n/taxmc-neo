using UnityEngine;
using trrne.Pancreas;

namespace trrne.Heart
{
    public class Pad : Object
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
                // rb.velocity += rb.mass * core.power * Vector100.y2d * Time.fixedDeltaTime;
                rb.velocity += core.Power * rb.mass * Time.fixedDeltaTime * transform.up.ToVec2();
            }
        }
    }
}
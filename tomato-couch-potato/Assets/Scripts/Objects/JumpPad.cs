using trrne.Box;
using UnityEngine;

namespace trrne.Core
{
    public class JumpPad : Object
    {
        [SerializeField]
        float jumpPower = 10f;

        protected override void Behavior() { }

        void OnTriggerEnter2D(Collider2D info)
        {
            if (info.TryGetComponent(out Rigidbody2D rb))
            {
                rb.velocity = Vector2.zero;
                rb.velocity += jumpPower * transform.up.ToV2();
                PlayOneShot(ses.Choice());
            }
        }
    }
}
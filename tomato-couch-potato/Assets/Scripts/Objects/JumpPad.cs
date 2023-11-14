using UnityEngine;

namespace trrne.Core
{
    public class JumpPad : MonoBehaviour
    {
        [SerializeField]
        float jumpPower = 10f;

        const float DivRatio = 0.1f;

        void OnTriggerEnter2D(Collider2D info)
        {
            if (info.TryGetComponent(out Rigidbody2D rb))
            {
                // rb.velocity = Vector2.zero;
                rb.velocity += jumpPower * rb.mass * DivRatio * Vector2.up;
            }
        }
    }
}
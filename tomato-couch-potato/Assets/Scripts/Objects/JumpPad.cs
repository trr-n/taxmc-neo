using UnityEngine;

namespace trrne.Core
{
    public class JumpPad : MonoBehaviour
    {
        [SerializeField]
        float jumpPower = 10f;

        void OnTriggerEnter2D(Collider2D info)
        {
            if (info.TryGetComponent(out Rigidbody2D rb))
            {
                rb.velocity = Vector2.zero;
                rb.velocity += jumpPower * Vector2.up;
            }
        }
    }
}
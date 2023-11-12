using trrne.Box;
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

                // TODO jumpPowerの値を上げても一定以上飛べない
                rb.velocity += Time.unscaledDeltaTime * jumpPower * rb.mass * transform.up.ToVec2();
            }
        }
    }
}
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

                // ! /////////////////////////////////////////////////////////////////////////////////////
                // FIXME jumpPowerの値を大きくしても一定以上高く跳ばない, 一回目のジャンプは高いこともある
                // ! /////////////////////////////////////////////////////////////////////////////////////
                var jumpPower = this.jumpPower * rb.mass;
                print(jumpPower);
                rb.velocity += jumpPower * Vector2.up;
            }
        }
    }
}
using trrne.Box;
using trrne.Brain;
using UnityEngine;

namespace trrne.Core
{
    public class JumpPad : MonoBehaviour
    {
        [SerializeField]
        float jumpPower = 10f;

        [SerializeField]
        AudioClip se;

        void OnTriggerEnter2D(Collider2D info)
        {
            if (info.TryGetComponent(out Rigidbody2D rb))
            {
                rb.velocity = Vector2.zero;
                rb.velocity += jumpPower * transform.up.ToV2();
                Recorder.Instance.PlayOneShot(se);
            }
        }
    }
}
using trrne.Body;
using trrne.Bag;
using UnityEngine;

namespace trrne.Arm
{
    public class Player : MonoBehaviour//, IPlayer
    {
        public bool ctrlable { get; set; }
        readonly (float basis, float limit) speed = (5, 7.5f);
        Rigidbody2D rb;

        void Start()
        {
            ctrlable = false;
            rb = GetComponent<Rigidbody2D>();
        }

        void Update()
        {
            Move();
        }

        void Move()
        {
            if (!ctrlable)
            {
                return;
            }

            if (!(Inputs.Pressed(Constant.Keys.Horizontal) || Inputs.Pressed(Constant.Keys.Vertical)))
            {
                rb.velocity *= 0.5f;
                return;
            }

            // 速度制限
            var vel = rb.velocity;
            rb.velocity = new(
                Mathf.Clamp(vel.x, -speed.limit, speed.limit), Mathf.Clamp(vel.y, -speed.limit, speed.limit));
            rb.velocity += speed.basis * Time.fixedDeltaTime * Inputs.AxisRaw();
        }
    }
}
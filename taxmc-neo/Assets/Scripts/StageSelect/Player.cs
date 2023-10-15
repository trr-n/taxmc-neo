using UnityEngine;
using trrne.Body;
using trrne.WisdomTeeth;
using trrne.Brain;

namespace trrne.Arm
{
    public class Player : MonoBehaviour //, IPlayer
    {
        public bool controllable { get; set; }
        readonly (float basis, float limit) speed = (5, 7.5f);
        Rigidbody2D rb;

        PauseMenu menu;

        void Start()
        {
            controllable = false;
            rb = GetComponent<Rigidbody2D>();
            menu = Gobject.GetWithTag<PauseMenu>(Constant.Tags.Manager);
        }

        void Update()
        {
            Move();
        }

        void Move()
        {
            if (menu.isPausing || !controllable)
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
                Mathf.Clamp(vel.x, -speed.limit, speed.limit),
                Mathf.Clamp(vel.y, -speed.limit, speed.limit));

            rb.velocity += speed.basis * Time.fixedDeltaTime * Inputs.AxisRaw();
        }
    }
}
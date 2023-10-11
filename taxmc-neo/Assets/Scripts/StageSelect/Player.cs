using trrne.Bag;
using UnityEngine;

namespace trrne.Body.Select
{
    public class Player : MonoBehaviour
    {
        public bool controllable { get; set; }

        float speed = 5;
        float limit = 7.5f;
        Rigidbody2D rb;

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        void Update()
        {
            Move();
        }

        void Move()
        {
            if (!controllable)
            {
                return;
            }

            if (!(Inputs.Pressed(Constant.Keys.Horizontal) || Inputs.Pressed(Constant.Keys.Vertical)))
            {
                rb.velocity *= 0.5f;
                return;
            }

            rb.velocity = new(
                Mathf.Clamp(rb.velocity.x, -limit, limit), Mathf.Clamp(rb.velocity.y, -limit, limit));
            rb.velocity += Inputs.AxisRaw() * speed * Time.fixedDeltaTime;
            // transform.Translate(Inputs.AxisRaw() * speed * Time.unscaledDeltaTime);
        }
    }
}
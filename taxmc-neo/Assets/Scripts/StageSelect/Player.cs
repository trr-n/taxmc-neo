using trrne.Bag;
using UnityEngine;

namespace trrne.Body.Select
{
    public class Player : MonoBehaviour
    {
        float speed = 5;

        public bool controllable { get; set; }

        void Start()
        {
            controllable = true;
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

            transform.Translate(Inputs.AxisRaw() * speed * Time.unscaledDeltaTime);
        }
    }
}
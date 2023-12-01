using UnityEngine;

namespace trrne.test
{
    public class Rolling : MonoBehaviour
    {
        Vector2 o;

        void Start()
        {
            o = transform.position;
        }

        void Update()
        {
            float d = Time.time,
                sin = Mathf.Sin(d), cos = Mathf.Cos(d);
            Vector2 p = transform.position;
            transform.position = new(
                x: cos * o.x - sin * o.y,
                y: sin * o.x + cos * o.y
            );
        }
    }
}

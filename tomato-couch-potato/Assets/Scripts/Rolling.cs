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
            float theta = Time.time * 0.1f,
                sin = Mathf.Sin(theta), 
                cos = Mathf.Cos(theta);
            var x = cos * o.x - sin * o.y;
            var y = sin * o.x + cos * o.y;
            transform.position = new(x, y);
        }
    }
}

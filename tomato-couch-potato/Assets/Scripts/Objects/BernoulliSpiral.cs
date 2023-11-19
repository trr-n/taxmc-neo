using UnityEngine;

namespace trrne.Core
{
    public class BernoulliSpiral : Object
    {
        [SerializeField]
        float size = 0.1f, b = 0.5f, speed = 1f;

        float angle = 0f;

        protected override void Start()
        {
            print("hello world");
        }

        protected override void Behavior()
        {
            // r=ae^(bθ)
            angle += Time.deltaTime * speed;
            float r = size * Mathf.Exp(b * angle);
            float x = r * Mathf.Cos(angle), y = r * Mathf.Sin(angle);
            transform.position = new Vector2(x, y);
        }
    }
}
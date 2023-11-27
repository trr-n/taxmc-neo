using UnityEngine;

namespace trrne.Core
{
    public class BernoulliSpiral : Object
    {
        [SerializeField]
        float a = 0.1f, b = 0.5f, speed = 1f;

        float angle = 0f;

        protected override void Behavior()
        {
            // r=ae^(bθ)
            angle += Time.deltaTime * speed;
            float r = a * Mathf.Exp(b * angle);
            float x = r * Mathf.Cos(angle), y = r * Mathf.Sin(angle);
            transform.position = new(x, y);
        }
    }
}
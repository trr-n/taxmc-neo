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
            float zip = a * Mathf.Exp(b * (angle += Time.deltaTime * speed));
            transform.position = new(zip * Mathf.Cos(angle), zip * Mathf.Sin(angle));
        }
    }
}
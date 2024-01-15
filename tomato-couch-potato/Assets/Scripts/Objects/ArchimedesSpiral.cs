using UnityEngine;

namespace trrne.Core
{
    public class ArchimedesSpiral : Object
    {
        [SerializeField]
        float a = 0.1f, b = 0.5f, speed = 1f;

        float angle = 0f;

        protected override void Behavior()
        {
            // r=aθ
            float zip = a + b * (angle += Time.deltaTime * speed);
            float x = zip * Mathf.Cos(angle), y = zip * Mathf.Sin(angle);
            transform.position = new(x, y);
        }
    }
}
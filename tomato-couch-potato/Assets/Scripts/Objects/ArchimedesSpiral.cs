using UnityEngine;

namespace trrne.Core
{
    public class ArchimedesSpiral : Object
    {
        [SerializeField]
        float size = 0.1f, b = 0.5f, speed = 1f;

        float angle = 0f;

        protected override void Behavior()
        {
            // r=aθ
            angle += Time.deltaTime * speed;
            float angles = size + b * angle;
            float x = angles * Mathf.Cos(angle), y = angles * Mathf.Sin(angle);
            transform.position = new(x, y);
        }
    }
}
using System;
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
            double theta = Time.time * 0.1f, sin = Math.Sin(theta), cos = Math.Cos(theta);
            double x = cos * o.x - sin * o.y, y = sin * o.x + cos * o.y;
            transform.position = new((float)x, (float)y);
        }
    }
}

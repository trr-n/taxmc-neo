using System;
using UnityEngine;
using trrne.Box;

namespace trrne.test
{
    public class Rolling : MonoBehaviour
    {
        Vector2 o;

        void Start()
        {
            for (int i = 0; i < 10; ++i)
            {
                print(Rand.String(10, RandStringType.Mixed));
            }
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

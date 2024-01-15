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
            float sin = MathF.Sin(Time.time * .1f), cos = MathF.Cos(Time.time * .1f);
            transform.position = new(cos * o.x - sin * o.y, sin * o.x + cos * o.y);
        }
    }
}

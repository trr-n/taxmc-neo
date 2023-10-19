using System.Collections;
using System.Collections.Generic;
using Chickenen.Pancreas;
using UnityEngine;

namespace Chickenen.Heart
{
    public class Fan : Object
    {
        public float power;

        GameObject collision;

        protected override void Start()
        {
            base.Start();
            collision = transform.GetChildObject();
        }

        protected override void Behavior()
        {
        }
    }
}

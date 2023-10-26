using System.Collections;
using System.Collections.Generic;
using trrne.Pancreas;
using UnityEngine;

namespace trrne.Heart
{
    public class Fan : Object
    {
        public float power;

        // GameObject collision;

        protected override void Start()
        {
            base.Start();
            // collision = transform.GetChildObject();
        }

        protected override void Behavior() { }
    }
}

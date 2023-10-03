using System.Collections;
using System.Collections.Generic;
using trrne.Bag;
using UnityEngine;

namespace trrne.Body
{
    public class Fan : Objectt
    {
        public float power;

        GameObject collision;

        protected override void Start()
        {
            base.Start();
            collision = transform.GetChild();
        }

        protected override void Behavior()
        {
        }
    }
}

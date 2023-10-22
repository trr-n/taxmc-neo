using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Chickenen.Pancreas;
using UnityEngine;

namespace Chickenen.Heart
{
    public class Gunny : Creature, IMurderable
    {
        (GameObject obj, LineRenderer line, float lineLength) gun;

        Vector3 direction = Vector100.X;
        float speed = 10f;

        Rigidbody2D rb;

        protected override void Start()
        {
            rb = GetComponent<Rigidbody2D>();

            gun.obj = transform.GetChild(0).gameObject;
            gun.line = gun.obj.GetComponent<LineRenderer>();
        }

        protected override void Movement()
        {
            ;
        }

        protected override void Behavior()
        {
            ;
        }

        void ADS()
        {
            gun.line.startColor = gun.line.endColor = Color.HSVToRGB(Time.time % 1, 1, 1);
        }

        public override UniTask Die()
        {
            throw new System.NotImplementedException();
        }
    }
}

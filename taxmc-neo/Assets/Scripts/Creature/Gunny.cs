using System.Collections;
using System.Collections.Generic;
using trrne.Utils;
using UnityEngine;

namespace trrne.Game
{
    public class Gunny : Enemy
    {
        // GameObject gun;
        // LineRenderer gunLine;
        // readonly float gunLineLength = 5f;
        (GameObject obj, LineRenderer line, float length) gun;

        Vector3 direction = Coordinate.x;
        float speed = 10f;

        Rigidbody2D rb;

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();

            gun.obj = transform.GetChild(0).gameObject;
            gun.line = gun.obj.GetComponent<LineRenderer>();
        }

        void Update()
        {
            Move();
        }

        protected override void Move()
        {
            if (!enable)
            {
                rb.isKinematic = false;
                return;
            }

            rb.isKinematic = true;
        }

        protected override void DetectPlayer()
        {
        }

        void DetectWall()
        {
            Ray ray = new(transform.position, direction);
        }

        void ADS()
        {
            gun.line.startColor = gun.line.endColor = Color.HSVToRGB(Time.time % 1, 1, 1);
        }

        protected override void Die()
        {
            throw new System.NotImplementedException();
        }
    }
}

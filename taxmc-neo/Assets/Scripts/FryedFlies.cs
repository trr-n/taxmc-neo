using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Self.Utils;

namespace Self.Game
{
    public class FryedFlies : MonoBehaviour
    {
        [SerializeField]
        Sprite[] sprites;

        public bool Ctrlable { get; set; }

        readonly float speed = 10;

        (Vector3 offset, float dis, int layer) rays = (new(), 0.25f, 1 << Constant.Layers.Ground);
        readonly float power = 250;

        bool isFlying;
        public bool IsFlying => isFlying;

        float currentSpeed;
        public float CurrentSpeed => currentSpeed;

        SpriteRenderer selfSr;
        Rigidbody2D selfRb;

        [SerializeField]
        Text speedT;

        void Start()
        {
            selfSr = GetComponent<SpriteRenderer>();
            rays.offset = ((selfSr.bounds.size.y / 2) - 0.2f) * Coordinate.Y;

            selfRb = GetComponent<Rigidbody2D>();
            selfRb.mass = 60f;

            Ctrlable = true;
        }

        void Update()
        {
            // TestAnimation2(selfSr, 1, Color.red, Color.green, Color.blue);
            selfSr.Colour(0.33f, Color.red, Color.green, Color.blue);
            selfSr.Image(0.25f, sprites);
        }

        void FixedUpdate()
        {
            Movement();

            speedT.SetText(Numeric.Cutail(CurrentSpeed) + " km/h");
        }

        void Movement()
        {
            if (!Ctrlable)
                return;

            Vector2 move = Input.GetAxisRaw(Constant.Keys.Horizontal) * Coordinate.X;
            selfRb.position += Time.fixedDeltaTime * speed * move;
            currentSpeed = transform.Speed();

            Ray ray = new(transform.position - rays.offset, -Coordinate.Y);
            Debug.DrawRay(ray.origin, ray.direction * rays.dis);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, rays.dis, rays.layer);

            isFlying = !hit;

            if (hit && Inputs.Pressed(Constant.Keys.Jump))
            {
                selfRb.AddForce(Coordinate.Y * power, ForceMode2D.Impulse);
            }
        }

        public void Die()
        {
            print("die.");
        }
    }
}
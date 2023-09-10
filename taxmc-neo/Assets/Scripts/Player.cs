using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Self.Utils;

namespace Self.Game
{
    public class Player : MonoBehaviour
    {
        [SerializeField]
        Sprite[] sprites;

        public bool Ctrlable { get; set; }
        public bool Jumpable { get; set; }
        public bool Walkable { get; set; }

        [SerializeField]
        float climbingSpeed;
        // bool onLadder = false;
        Vector3 climbingDir;

        bool onWall = false;

        readonly float speed = 10;

        (Vector3 offset, float dis, int layer) rays = (new(), 0.25f, Constant.Layers.Ground);
        readonly float power = 250;

        bool isFloating;
        public bool IsFloating => isFloating;

        float currentSpeed;
        public float CurrentSpeed => currentSpeed;

        SpriteRenderer sr;
        Rigidbody2D rb;

        [SerializeField]
        Text speedT, livesT;

        Health health;

        void Start()
        {
            sr = GetComponent<SpriteRenderer>();
            rays.offset = ((sr.bounds.size.y / 2) - 0.2f) * Coordinate.Y;

            rb = GetComponent<Rigidbody2D>();
            rb.mass = 60f;

            health = GetComponent<Health>();

            Ctrlable = true;
        }

        void Update()
        {
            // sr.Colour(0.33f, Color.red, Color.green, Color.blue);
            livesT.SetText(health.Lives.current);
        }

        void FixedUpdate()
        {
            Movement();
        }

        void LateUpdate()
        {
            speedT.SetText(Numeric.Cutail(CurrentSpeed) + " km/h");
        }

        void Movement()
        {
            if (!Ctrlable) return;

            // Move
            Vector2 move = Input.GetAxisRaw(Constant.Keys.Horizontal) * Coordinate.X;
            rb.position += Time.fixedDeltaTime * speed * move;
            currentSpeed = transform.Speed();

            // Jump
            Ray jray = new(transform.position - rays.offset, -Coordinate.Y);
            Debug.DrawRay(jray.origin, jray.direction * rays.dis);

            isFloating = Gobject.Raycast2D(out var hit, jray.origin, jray.direction, rays.layer, rays.dis);
            if (hit)
            {
                if (isFloating && /*!onLadder &&*/ Inputs.Pressed(Constant.Keys.Jump))
                {
                    rb.AddForce(Coordinate.Y * power, ForceMode2D.Impulse);
                }
            }

            // Climb
            if (Gobject.BoxCast2D(out var lhit, transform.position, sr.bounds.size, Constant.Layers.Obstacle))
            {
                if (!lhit.Compare(Constant.Tags.Ladder))
                    return;

                if (Inputs.Pressed(Constant.Keys.Jump))
                {
                    climbingDir = Coordinate.Y;
                }
                else if (Inputs.Pressed(Constant.Keys.Down))
                {
                    climbingDir = -Coordinate.Y;
                }

                transform.Translate(Time.deltaTime * climbingSpeed * climbingDir);
            }
            else
            {
                if (Physics2D.gravity != (Vector2)Constant.DefaultGravity)
                {
                    Physics2D.gravity = Constant.DefaultGravity;
                }
            }

            // // Jump pad
            // Ray pray = new(transform.position, Vector2.right);
            // if (Gobject.Raycast2D(out var phit, pray.origin, pray.direction, Constant.Layers.Obstacle, sr.bounds.size.x * 1.1f))
            // {
            //     if (phit.Compare(Constant.Tags.Pad))
            //     {
            //     }
            // }
        }

        public void Die()
        {
            transform.position = Vector2.zero;
            health.Reset();
        }
    }
}
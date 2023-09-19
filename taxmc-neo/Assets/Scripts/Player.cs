using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using trrne.Utils;
using System.Runtime.InteropServices.WindowsRuntime;

namespace trrne.Game
{
    public class Player : MonoBehaviour
    {
        [SerializeField]
        Text leftText, velocityText;

        [SerializeField]
        Sprite[] sprites;

        public bool ctrlable { get; set; }
        public bool jumpable { get; set; }
        public bool walkable { get; set; }

        // readonly float speed = 10;
        readonly (float basis, float max) speed = (20, 10);

        (Vector3 offset, float dis, int layer) rays = (new(), 0.25f, Constant.Layers.Ground);
        readonly float power = 250;

        bool floating;
        public bool isFloating => floating;

        float vel;
        public float velocity => vel;

        SpriteRenderer sr;
        Rigidbody2D rb;

        /// <summary>
        /// ザンキ
        /// </summary>
        Health health;

        void Start()
        {
            sr = GetComponent<SpriteRenderer>();
            rays.offset = ((sr.bounds.size.y / 2) - 0.2f) * Coordinate.y;

            rb = GetComponent<Rigidbody2D>();
            rb.mass = 60f;

            health = GetComponent<Health>();
        }

        void Update()
        {
            leftText.SetText(health.lives.left);
        }

        void FixedUpdate()
        {
            Movement();
        }

        /// <summary>
        /// ゴキブリの動きぶり
        /// </summary>
        void Movement()
        {
            if (!ctrlable) { return; }

            Move();

            // ジャンプ
            Ray rjump = new(transform.position - rays.offset, -Coordinate.y);
#if DEBUG
            Debug.DrawRay(rjump.origin, rjump.direction * rays.dis);
#endif

            // 地に足がついていたらジャンプ可
            if (floating = (
                Gobject.Raycast2D(out var hit, rjump.origin, rjump.direction, rays.layer, rays.dis) &&
                Inputs.Pressed(Constant.Keys.Jump)
            ))
            {
                rb.AddForce(Coordinate.y * power, ForceMode2D.Impulse);
            }
        }

        /// <summary>
        /// 移動
        /// </summary>
        void Move()
        {
            Vector2 move = Input.GetAxisRaw(Constant.Keys.Horizontal) * Coordinate.x;
            velocityText.SetText(rb.velocity);

            if (move.magnitude <= 0.3f)
            {
                // rb.velocity = new(rb.velocity.x * 0.9f, rb.velocity.y);
                rb.SetVelocityX(rb.velocity.x * 0.9f);
            }

            rb.velocity = new(Mathf.Clamp(rb.velocity.x, -speed.max, speed.max), rb.velocity.y);
            rb.velocity += Time.fixedDeltaTime * (floating ? speed.basis / 2 : speed.basis) * move;
        }

        /// <summary>
        /// 成仏
        /// </summary>
        public void Die()
        {
            // 制御不可に
            ctrlable = false;

            // 座標リセット
            transform.SetPosition(Constant.Positions.Stage1);

            ctrlable = true;
        }
    }
}
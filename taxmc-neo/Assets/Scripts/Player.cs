using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using trrne.Utils;

namespace trrne.Game
{
    public class Player : MonoBehaviour
    {
        [SerializeField]
        Text leftText;

        [SerializeField]
        Sprite[] sprites;

        // FadingPanel fpanel;

        public bool ctrlable { get; set; }
        public bool jumpable { get; set; }
        public bool walkable { get; set; }

        readonly float speed = 10;

        (Vector3 offset, float dis, int layer) rays = (new(), 0.25f, Constant.Layers.Ground);
        readonly float power = 250;

        bool isFloating;
        public bool IsFloating => isFloating;

        float currentSpeed;
        public float CurrentSpeed => currentSpeed;

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

            // fpanel = Gobject.GetWithTag<FadingPanel>(Constant.Tags.Panel);

            ctrlable = true;
        }

        void Update()
        {
            // sr.Colour(0.33f, Color.red, Color.green, Color.blue);
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

            // 移動
            Vector2 move = Input.GetAxisRaw(Constant.Keys.Horizontal) * Coordinate.x;
            rb.position += Time.fixedDeltaTime * speed * move;
            currentSpeed = transform.Speed();

            // ジャンプ
            Ray jumpRay = new(transform.position - rays.offset, -Coordinate.y);
#if DEBUG
            Debug.DrawRay(jumpRay.origin, jumpRay.direction * rays.dis);
#endif

            // 地に足がついていたらジャンプ可
            if (isFloating = Gobject.Raycast2D(
                out var hit, jumpRay.origin, jumpRay.direction, rays.layer, rays.dis))
            {
                if (isFloating && Inputs.Pressed(Constant.Keys.Jump))
                {
                    rb.AddForce(Coordinate.y * power, ForceMode2D.Impulse);
                }
            }
        }

        /// <summary>
        /// 成仏
        /// </summary>
        public void Die()
        {
            transform.position = Vector2.zero;
            health.Reset();

            // fpanel.Fade(FadeStyle.Out);
        }
    }
}
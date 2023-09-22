using UnityEngine;
using UnityEngine.UI;
using trrne.utils;
using Cysharp.Threading.Tasks;

namespace trrne.Game
{
    public class Player : MonoBehaviour
    {
        [SerializeField]
        Text leftText, velocityText;

        [SerializeField]
        Sprite[] sprites;

        [SerializeField]
        GameObject dieFx;

        public bool ctrlable { get; set; }
        public bool jumpable { get; set; }
        public bool walkable { get; set; }

        /// <summary>
        /// 死亡処理中はtrue
        /// </summary>
        bool dying;

        // bool onIce = false;

        // readonly float speed = 10;
        readonly (float basis, float max) speed = (20, 10);

        (Vector3 offset, float dis, int layer) rays = (new(), 0.25f, Constant.Layers.Ground | Constant.Layers.Object);
        // readonly float jumpPower = 250;
        (bool during, float power) jump = (false, 250f);

        bool floating;
        public bool isFloating => floating;

        float vel;
        /// <summary>
        /// 移動速度
        /// </summary>
        public float velocity => vel;

        readonly float floatingReduction = 5f;

        SpriteRenderer sr;
        Rigidbody2D rb;

        // /// <summary>
        // /// ザンキ
        // /// </summary>
        // Health health;

        readonly float tolerance = 0.33f;

        /// <summary>
        /// 減速比
        /// </summary>
        readonly float reduction = 0.9f;

        Animator animator;

        void Start()
        {
            sr = GetComponent<SpriteRenderer>();
            rays.offset = ((sr.bounds.size.y / 2) - 0.2f) * Coordinate.y;

            rb = GetComponent<Rigidbody2D>();
            rb.mass = 60f;

            // health = GetComponent<Health>();
        }

        void Update()
        {
            // leftText.SetText(health.lives.left);
        }

        void FixedUpdate()
        {
            Movement();
        }

        /// <summary>
        /// 動きぶり
        /// </summary>
        void Movement()
        {
            if (!ctrlable) { return; }

            Move();
            Jump();
        }

        void Jump()
        {
            // ジャンプ
            Ray rjump = new(transform.position - rays.offset, -Coordinate.y);
#if DEBUG
            Debug.DrawRay(rjump.origin, rjump.direction * rays.dis);
#endif
            // 地に足がついていたらジャンプ可
            if (floating = Gobject.Raycast2D(out var hit, rjump.origin, rjump.direction, rays.layer, rays.dis) && Inputs.Pressed(Constant.Keys.Jump))
            {
                rb.AddForce(Coordinate.y * jump.power, ForceMode2D.Impulse);
            }
        }

        /// <summary>
        /// 移動
        /// </summary>
        void Move()
        {
            Vector2 move = Input.GetAxisRaw(Constant.Keys.Horizontal) * Coordinate.x;
            velocityText.SetText(rb.velocity);

            if (move.magnitude <= tolerance) // && !onIce)
            {
                // 入力されていなかったら速度を0.9倍する
                rb.SetVelocityX(rb.velocity.x * reduction);
            }

            // Xの速度を制限
            rb.velocity = new(Mathf.Clamp(rb.velocity.x, -speed.max, speed.max), rb.velocity.y);

            // 浮いていたら移動速度を1/2に
            rb.velocity += Time.fixedDeltaTime * (floating ? speed.basis / floatingReduction : speed.basis) * move;
        }

        /// <summary>
        /// 成仏
        /// </summary>
        // public void Die()
        public async UniTask Die()
        {
            if (dying) { return; }
            // 死亡中
            dying = true;

            // 制御不可に
            ctrlable = false;

            // エフェクト生成
            var fx = dieFx.Generate(transform.position);

            // エフェクトの長さ分だけちと待機
            // TODO 代替案: 「はあ。。」を録音して流す
            // fx.fxdurationをはあの長さに変更
            await UniTask.Delay(Numeric.Cutail(fx.FxDuration()) + 1);

            // 座標リセット
            transform.SetPosition(Constant.SpawnPositions.Stage1);

            // 制御可能に
            ctrlable = true;

            // 復活していいよ
            dying = false;
        }
    }
}
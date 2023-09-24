using UnityEngine;
using UnityEngine.UI;
using trrne.Appendix;
using Cysharp.Threading.Tasks;

namespace trrne.Body
{
    public class Player : MonoBehaviour
    {
        [SerializeField]
        Text velocityText;

        [SerializeField]
        Sprite[] sprites;

        [SerializeField]
        GameObject dieFx;

        public bool ctrlable { get; set; }
        public bool jumpable { get; set; }
        public bool walkable { get; set; }

        /// <summary>
        /// 氷の上に乗っていたらtrue
        /// </summary>
        bool onIce;

        /// <summary>
        /// 死亡処理中はtrue
        /// </summary>
        bool dying;

        // readonly float speed = 10;
        readonly (float basis, float max) speed = (20, 10);

        /// <summary>
        /// 始点<br/>
        /// 長さ<br/>
        /// 対象のレイヤー
        /// </summary>
        (Vector3 offset, float distance, int layers) rayconf = (new(), 0.25f, Fixed.Layers.Ground | Fixed.Layers.Object);

        /// <summary>
        /// ジャンプ中<br/>
        /// 
        /// </summary>
        (bool during, float power, float hitbox) jump = (false, 100f, 0.9f);

        bool floating;
        /// <summary>
        /// 地に足がついていなかったらtrue
        /// </summary>
        public bool isFloating => floating;

        float vel;
        /// <summary>
        /// 移動速度
        /// </summary>
        public float velocity => vel;

        readonly float floatingReduction = 10f;

        SpriteRenderer sr;
        Rigidbody2D rb;

        readonly float tolerance = 0.33f;

        /// <summary>
        /// 減速比
        /// </summary>
        readonly float reduction = 0.9f;

        // Animator animator;

        Vector3 checkpoint = Vector3.zero;
        public void SetCP(Vector3 cp) => checkpoint = cp;
        public void Mds() => transform.SetPosition(checkpoint);

        void Start()
        {
            sr = GetComponent<SpriteRenderer>();
            rayconf.offset = ((sr.bounds.size.y / 2) - 0.2f) * Coordinate.y;

            rb = GetComponent<Rigidbody2D>();
            rb.mass = 60f;
        }

        void FixedUpdate()
        {
            Movement();
        }

        void Update()
        {
            // スペース チェックポイントに戻る
            if (Inputs.Down(KeyCode.Space)) { transform.SetPosition(checkpoint); }

            if (Inputs.Down(KeyCode.A)) { sr.flipX = true; }
            if (Inputs.Down(KeyCode.D)) { sr.flipX = false; }
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
            (Vector2 origin, Vector2 size) box = (
                new(transform.position.x, transform.position.y - sr.bounds.size.y / 2),
                new(sr.bounds.size.x * jump.hitbox, rayconf.distance));

            if (floating = Gobject.BoxCast2D(out _, box.origin, box.size, layer: Fixed.Layers.Object | Fixed.Layers.Ground)
                && Inputs.Pressed(Fixed.Keys.Jump))
            {
                rb.AddForce(Coordinate.y * jump.power, ForceMode2D.Impulse);
            }
        }

        // void OnDrawGizmos()
        // {
        //     Gizmos.color = Color.HSVToRGB(Time.unscaledTime % 1, 1, 1);
        //     // 足元のヒットボックス表示
        //     Gizmos.DrawWireCube(new(transform.position.x, transform.position.y - sr.bounds.size.y / 2),
        //         new(sr.bounds.size.x * jump.hitbox, rayconf.distance));
        // }

        /// <summary>
        /// 移動
        /// </summary>
        void Move()
        {
            Vector2 move = Input.GetAxisRaw(Fixed.Keys.Horizontal) * Coordinate.x;
            velocityText.SetText(rb.velocity);

            // 入力されている値がtolerance以下、氷に乗っていない、浮いていない
            if (move.magnitude <= tolerance && !onIce && !floating)
            {
                // x軸の速度を0.9倍
                rb.SetVelocityX(rb.velocity.x * reduction);
            }

            // Xの速度を制限
            rb.velocity = new(Mathf.Clamp(rb.velocity.x, -speed.max, speed.max), rb.velocity.y);

            // 浮いていたら移動速度低下
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
            await UniTask.Delay(Numeric.Cutail(fx.FxDuration()));

            // 1/5フレーム待機
            await UniTask.DelayFrame(Numeric.Cutail(App.fps / 5));

            // 座標リセット
            transform.SetPosition(Fixed.SpawnPositions.Stage1);

            // 制御可能に
            ctrlable = true;

            // 復活していいよ
            dying = false;
        }

        void OnCollisionEnter2D(Collision2D info)
        {
            if (info.Compare(Fixed.Tags.Ice))
            {
                onIce = true;
            }
        }

        void OnCollisionExit2D(Collision2D info)
        {
            if (info.Compare(Fixed.Tags.Ice))
            {
                onIce = false;
            }
        }
    }
}
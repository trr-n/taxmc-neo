using UnityEngine;
using UnityEngine.UI;
using trrne.Bag;
using Cysharp.Threading.Tasks;
using System;
using System.Collections;

namespace trrne.Body
{
    public class Player : MonoBehaviour
    {
        [SerializeField]
        Text velT;

        [SerializeField]
        Sprite[] sprites;

        [SerializeField]
        GameObject diefx;

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
        public bool isDying { get; set; }

        // readonly float speed = 10;
        readonly (float basis, float max, float freduction, float reduction) speed = (20, 10, 0.1f, 0.9f);

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
        (bool during, float power, float hitbox) jump = (false, 67.5f, 0.9f);

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

        SpriteRenderer sr;
        Rigidbody2D rb;

        readonly float tolerance = 0.33f;

        Vector3 checkpoint = Vector3.zero;
        /// <summary>
        /// チェックポイントを設定する
        /// </summary>
        public void SetCheckpoint(Vector3 point) => checkpoint = point;

        /// <summary>
        /// チェックポイントに戻す
        /// </summary>
        public void Return2CP() => transform.SetPosition(checkpoint);

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
            // スペースでチェックポイントに戻る
            if (Inputs.Down(KeyCode.Space)) { Return2CP(); }

            Flip();
        }

        void LateUpdate()
        {
            // 速度表示
            velT.SetText(rb.velocity);
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

        void Flip()
        {
            if (!ctrlable) { return; }

            if (Inputs.Down(KeyCode.A)) { sr.flipX = true; }
            if (Inputs.Down(KeyCode.D)) { sr.flipX = false; }
        }

        void Jump()
        {
            (Vector2 origin, Vector2 size) hitbox = (
                new(transform.position.x, transform.position.y - sr.bounds.size.y / 2),
                new(sr.bounds.size.x * jump.hitbox, rayconf.distance));

            // オブジェクトか地面に足がついていて、ジャンプキーを押していたら
            if (floating = Gobject.BoxCast2D(out _, hitbox.origin, hitbox.size, layer: Fixed.Layers.Object | Fixed.Layers.Ground)
                && Inputs.Pressed(Fixed.Keys.Jump))
            {
                // ジャンプ
                rb.AddForce(Coordinate.y * jump.power, ForceMode2D.Impulse);
            }
        }

        /// <summary>
        /// 移動
        /// </summary>
        void Move()
        {
            Vector2 move = Input.GetAxisRaw(Fixed.Keys.Horizontal) * Coordinate.x;

            // 入力がtolerance以下、氷に乗っていない、浮いていない
            if (move.magnitude <= tolerance && !onIce && !floating)
            {
                // x軸の速度を0.9倍
                rb.SetVelocityX(rb.velocity.x * speed.reduction);
            }

            // 速度を制限
            rb.velocity = new(onIce ?
                // 氷に乗っていたら制限緩和
                Mathf.Clamp(rb.velocity.x, -speed.max * 2, speed.max * 2) :
                Mathf.Clamp(rb.velocity.x, -speed.max, speed.max),
                rb.velocity.y);

            // 浮いていたら移動速度低下
            rb.velocity += Time.fixedDeltaTime * (floating ? speed.basis * speed.freduction : speed.basis) * move;
        }

        /// <summary>
        /// 成仏
        /// </summary>
        public async UniTask Die()
        {
            if (isDying) { return; }

            // うごいちゃだめだよ
            isDying = true;
            ctrlable = false;

            // エフェクト生成
            // var fx = dieFx.Generate(transform.position);

            // // エフェクトの長さ分だけちと待機
            // await UniTask.Delay(Numeric.Cutail(diefx.FxDuration()));

            // // 1/5フレーム待機
            // await UniTask.DelayFrame(Numeric.Cutail(App.fps / 5));
            await UniTask.Delay(1000);

            StartCoroutine(functionName());
        }

        IEnumerator functionName()
        {
            yield return null;

            // 座標リセット
            Return2CP();

            // うごいていいよ
            ctrlable = true;
            isDying = false;
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
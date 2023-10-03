using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
using trrne.Bag;
using Cysharp.Threading.Tasks;
using System.Collections;

namespace trrne.Body
{
    public class Player : MonoBehaviour
    {
        [SerializeField]
        Text velT;

        [SerializeField]
        GameObject diefx;

        public bool ctrlable { get; set; }
        public bool jumpable { get; set; }
        public bool walkable { get; set; }

        /// <summary>
        /// 氷の上に乗っていたらtrue
        /// </summary>
        // bool onIce;

        /// <summary>
        /// 死亡処理中はtrue
        /// </summary>
        public bool isDieProcessing { get; set; }

        readonly (float basis, float max, float freduction, float reduction) speed = (20, 10, 0.1f, 0.9f);

        /// <summary>
        /// 始点<br/>
        /// 長さ<br/>
        /// 対象のレイヤー
        /// </summary>
        (Vector3 offset, float distance, int layers) rayconf = (new(), 0.25f, Constant.Layers.Ground | Constant.Layers.Object);

        (bool during, float power, float hitbox) jump = (false, 2f, 0.9f);

        bool isFloating;
        /// <summary>
        /// 地に足がついていなかったらtrue
        /// </summary>
        public bool IsFloating => isFloating;

        float velocity;
        /// <summary>
        /// 移動速度
        /// </summary>
        public float Velocity => velocity;

        // SpriteRenderer sr;
        Rigidbody2D rb;
        // new BoxCollider2D collider;
        Animator animator;
        PlayerFlag pjf;

        readonly float tolerance = 0.33f;

        Cam cam;

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
            pjf = transform.GetFromChild<PlayerFlag>(0);

            animator = GetComponent<Animator>();

            // collider = GetComponent<BoxCollider2D>();

            rb = GetComponent<Rigidbody2D>();
            rb.mass = 60f;

            cam = Gobject.GetWithTag<Cam>(Constant.Tags.MainCamera);
            cam.followable = true;
        }

        void FixedUpdate()
        {
            Move();
        }

        void Update()
        {
            Jump();
            Flip();
            Respawn();
        }

        void LateUpdate()
        {
            // 速度表示
            velT.SetText(rb.velocity);
        }

        /// <summary>
        /// スペースでリスポーンする
        /// </summary>
        void Respawn() => SimpleRunner.BoolAction(!isDieProcessing && Inputs.Down(KeyCode.Space), () => Return2CP());

        void Flip()
        {
            if (!ctrlable)
            {
                return;
            }

            if (Input.GetButtonDown(Constant.Keys.Horizontal))
            {
                var current = Mathf.Sign(transform.localScale.x);
                switch (Mathf.Sign(Input.GetAxisRaw(Constant.Keys.Horizontal)))
                {
                    case 1:
                        SimpleRunner.BoolAction(current != 1, () => transform.localScale *= new Vector2(-1, 1));
                        break;

                    case -1:
                        SimpleRunner.BoolAction(current != -1, () => transform.localScale *= new Vector2(-1, 1));
                        break;

                    default: break;
                }
            }
        }

        void Jump()
        {
            if (!ctrlable)
            {
                return;
            }

            if (pjf.isHit)
            {
                if (Inputs.Down(Constant.Keys.Jump))
                {
                    rb.velocity += jump.power * 3 * (Vector2)Coordinate.y;
                }
                animator.SetBool(Constant.Animations.Jump, false);
            }
            else
            {
                animator.SetBool(Constant.Animations.Jump, true);
            }
        }

        /// <summary>
        /// 移動
        /// </summary>
        void Move()
        {
            if (!ctrlable)
            {
                return;
            }

            animator.SetBool(Constant.Animations.Walk, Input.GetButton(Constant.Keys.Horizontal) && pjf.isHit);

            Vector2 move = Input.GetAxisRaw(Constant.Keys.Horizontal) * Coordinate.x;

            // 入力がtolerance以下、氷に乗っていない、浮いていない
            if (move.magnitude <= tolerance && !pjf.onIce && !isFloating)
            {
                // x軸の速度をspeed.reduction倍
                rb.SetVelocityX(rb.velocity.x * speed.reduction);
            }

            // 速度を制限
            rb.velocity = new(pjf.onIce ?
                Mathf.Clamp(rb.velocity.x, -speed.max * 2, speed.max * 2) :
                Mathf.Clamp(rb.velocity.x, -speed.max, speed.max),
                rb.velocity.y);

            float velocity = isFloating ? speed.basis * speed.freduction : speed.basis;

            // 浮いていたら移動速度低下
            rb.velocity += Time.fixedDeltaTime * velocity * move;
        }

        /// <summary>
        /// 成仏
        /// </summary>
        public async UniTask Die()
        {
            if (isDieProcessing)
            {
                return;
            }
            print("Die()");

            isDieProcessing = true;
            ctrlable = false;
            cam.followable = false;

            // エフェクト生成
            diefx.TryGenerate(transform.position);

            // // 1/5フレーム待機
            // await UniTask.DelayFrame(Numeric.Cutail(App.fps / 5));
            // 1秒待機
            await UniTask.Delay(1000);

            StartCoroutine(AfterDelay());
        }

        IEnumerator AfterDelay()
        {
            yield return null;

            // 座標リセット
            Return2CP();

            // うごいていいよ
            cam.followable = true;
            ctrlable = true;
            isDieProcessing = false;
        }

        // void OnCollisionEnter2D(Collision2D info)
        // {
        //     onIce = info.Compare(Constant.Tags.Ice);
        // }

        // void OnCollisionExit2D(Collision2D info)
        // {
        //     onIce = info.Compare(Constant.Tags.Ice);
        // }
    }
}
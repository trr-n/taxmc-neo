using UnityEngine;
using UnityEngine.UI;
using Chickenen.Pancreas;
using Cysharp.Threading.Tasks;
using System.Collections;
using Chickenen.Brain;

namespace Chickenen.Heart
{
    public enum CuzOfDeath
    {
        Venom,  // 毒死
        None    // 何もしない
    }

    public class Player : MonoBehaviour //, IPlayer
    {
        [SerializeField]
        Text velT;

        [SerializeField]
        GameObject diefx;

        public bool Controllable { get; set; }
        public bool Jumpable { get; set; }
        public bool Movable { get; set; }

        bool isKeyEntered = false;
        /// <summary>
        /// 移動キーが押されているか
        /// </summary>
        public bool IsKeyEntered => isKeyEntered;

        /// <summary>
        /// テレポート中か
        /// </summary>
        public bool IsTeleporting { get; set; }

        /// <summary>
        /// 死亡処理中はtrue
        /// </summary>
        public bool IsDieProcessing { get; set; }

        readonly (float basis, float max, float floatingReduction, float reduction) speed = (20, 10, 0.1f, 0.9f);
        readonly float jumpPower = 6f;

        bool isFloating;
        /// <summary>
        /// 地に足がついていなかったらtrue
        /// </summary>
        public bool IsFloating => isFloating;

        Vector2 velocity;
        /// <summary>
        /// 移動速度
        /// </summary>
        public Vector2 Velocity => velocity;

        Rigidbody2D rb;
        Animator animator;
        PlayerFlag flag;
        Cam cam;
        PauseMenu menu;

        readonly float inputTolerance = 0.33f;

        Vector2 cp = Vector2.zero;
        public Vector2 Checkpoint => cp;
        /// <summary>
        /// チェックポイントを設定する
        /// </summary>
        public void SetCheckpoint(Vector2 position) => cp = position;

        /// <summary>
        /// チェックポイントに戻す
        /// </summary>
        public void Return2CP() => transform.SetPosition(cp);

        void Start()
        {
            menu = Gobject.GetWithTag<PauseMenu>(Constant.Tags.Manager);

            flag = transform.GetFromChild<PlayerFlag>();

            animator = GetComponent<Animator>();

            rb = GetComponent<Rigidbody2D>();
            rb.mass = 60f;

            cam = Gobject.GetWithTag<Cam>(Constant.Tags.MainCamera);
            cam.Followable = true;

            Movable = true;
            Jumpable = true;
        }

        void FixedUpdate()
        {
            Move();
        }

        void Update()
        {
            DetectInput();
            Jump();
            Flip();
            Respawn();
            RBDisable();
        }

        void LateUpdate()
        {
#if DEBUG
            velocity = rb.velocity;
            // 速度表示
            velT.SetText(velocity);
#endif
        }

        void DetectInput()
        {
            if (!Controllable)
            {
                isKeyEntered = false;
                return;
            }

            isKeyEntered = Input.GetButton(Constant.Keys.Horizontal) || Input.GetButton(Constant.Keys.Jump);
        }

        /// <summary>
        /// スペースでリスポーンする
        /// </summary>
        void Respawn()
        {
            if (!IsDieProcessing && Inputs.Down(KeyCode.Space))
            {
                Return2CP();
            }
        }

        /// <summary>
        /// 反重力
        /// </summary>
        void RBDisable()
        {
            rb.isKinematic = IsTeleporting;
        }

        void Flip()
        {
            if (!Controllable || menu.IsPausing)
            {
                return;
            }

            if (Input.GetButtonDown(Constant.Keys.Horizontal))
            {
                var current = Mathf.Sign(transform.localScale.x);
                switch (Mathf.Sign(Input.GetAxisRaw(Constant.Keys.Horizontal)))
                {
                    case 1:
                        Shorthand.BoolAction(current != 1, () => transform.localScale *= new Vector2(-1, 1));
                        break;

                    case -1:
                        Shorthand.BoolAction(current != -1, () => transform.localScale *= new Vector2(-1, 1));
                        break;
                }
            }
        }

        void Jump()
        {
            // if (!(Controllable || Jumpable))
            if (!Controllable || !Jumpable)
            {
                return;
            }

            if (flag.IsHit)
            {
                if (Inputs.Down(Constant.Keys.Jump))
                {
                    rb.velocity += jumpPower * Vector100.Y2D;
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
            // if (!(Controllable || Movable))
            if (!Controllable || !Movable)
            {
                return;
            }

            animator.SetBool(Constant.Animations.Walk, Input.GetButton(Constant.Keys.Horizontal) && flag.IsHit);

            Vector2 move = Input.GetAxisRaw(Constant.Keys.Horizontal) * Vector100.X;

            // 入力がtolerance以下、氷に乗っていない、浮いていない
            if (move.magnitude <= inputTolerance && !flag.OnIce && !isFloating)
            {
                // x軸の速度をspeed.reduction倍
                rb.SetVelocityX(rb.velocity.x * speed.reduction);
            }

            // 速度を制限
            rb.velocity = new(flag.OnIce ?
                // 氷の上になら制限を緩和
                Mathf.Clamp(rb.velocity.x, -speed.max * 2, speed.max * 2) :
                Mathf.Clamp(rb.velocity.x, -speed.max, speed.max),
                rb.velocity.y
            );

            // 浮いていたら移動速度低下
            float velocity = isFloating ? speed.basis * speed.floatingReduction : speed.basis;
            rb.velocity += Time.fixedDeltaTime * velocity * move;
        }

        /// <summary>
        /// 成仏
        /// </summary>
        public async UniTask Die(CuzOfDeath cause)
        {
            if (IsDieProcessing)
            {
                return;
            }

            IsDieProcessing = true;
            Controllable = false;
            cam.Followable = false;

            diefx.TryGenerate(transform.position);

            switch (cause)
            {
                case CuzOfDeath.Venom:
                    rb.velocity = Vector2.zero;
                    animator.Play(Constant.Animations.Venomed);
                    break;

                default: break;
            }

            await UniTask.Delay(1250);

            // 落とし穴修繕
            foreach (var hole in FindObjectsByType<Hole>(FindObjectsSortMode.None))
            {
                if (hole.isBreaking)
                {
                    hole.Mending();
                }
            }

            StartCoroutine(AfterDelay());
        }

        public async UniTask Die()
        {
            await Die(CuzOfDeath.None);
        }

        IEnumerator AfterDelay()
        {
            yield return null;

            // 座標リセット
            Return2CP();

            // 死亡アニメーション停止
            animator.StopPlayback();

            // うごいていいよ
            cam.Followable = true;
            Controllable = true;
            IsDieProcessing = false;
        }
    }
}
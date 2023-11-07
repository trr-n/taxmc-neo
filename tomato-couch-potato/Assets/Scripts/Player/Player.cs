using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using trrne.Box;
using trrne.Brain;
using System.Collections;

namespace trrne.Core
{
    /// <summary>
    /// 死因
    /// </summary>
    public enum CuzOfDeath
    {
        Venom,  // 毒死
        None    // 何もしない
    }

    public enum PunishType
    {
        Mirror,
        Random
    }

    public class Player : MonoBehaviour, ICreature
    {
        [SerializeField]
        Text velT;

        [SerializeField]
        GameObject diefx;

        public bool Controllable { get; set; }
        public bool Jumpable { get; set; }
        public bool Movable { get; set; }

        /// <summary>
        /// 操作を反転するか
        /// </summary>
        public bool IsMirroring { get; set; }
        (float Duration, Stopwatch durationSW) mirror = (.33f, new());
        /// <summary>
        /// 反転可能か
        /// </summary>
        public bool Mirrorable { get; private set; }

        /// <summary>
        /// 移動キーが押されているか
        /// </summary>
        public bool IsPressedMovementKeys { get; private set; }

        /// <summary>
        /// テレポート中か
        /// </summary>
        public bool IsTeleporting { get; set; }

        /// <summary>
        /// 死亡処理中はtrue
        /// </summary>
        public bool IsDieProcessing { get; set; }

        (float basis, float max, float floatingReduction, float reduction) speed => (
            basis: 20,
            max: 10,
            floatingReduction: 0.95f, //! 要調整 ///////////////////////////////////////
            reduction: 0.9f
        );
        const float JumpPower = 6f;

        /// <summary>
        /// 地に足がついていなかったらtrue
        /// </summary>
        public bool IsFloating { get; private set; }

        /// <summary>
        /// 移動速度
        /// </summary>
        public Vector2 Velocity { get; private set; }

        Rigidbody2D rb;
        Animator animator;
        PlayerFlag flag;
        Cam cam;
        PauseMenu menu;
        new BoxCollider2D collider;

        public Vector3 CoreOffset => new(0, collider.bounds.size.y / 2);

        const float InputTolerance = 0.33f;

        public Vector2 Checkpoint { get; private set; } = Vector2.zero;

        /// <summary>
        /// チェックポイントを設定する
        /// </summary>
        public void SetCheckpoint(Vector2 position) => Checkpoint = position;

        /// <summary>
        /// チェックポイントに戻す
        /// </summary>
        public void ReturnToCheckpoint() => transform.position = Checkpoint;

        void Start()
        {
            menu = Gobject.GetWithTag<PauseMenu>(Constant.Tags.Manager);
            flag = transform.GetFromChild<PlayerFlag>();

            animator = GetComponent<Animator>();

            collider = GetComponent<BoxCollider2D>();

            rb = GetComponent<Rigidbody2D>();
            rb.mass = 60f;

            cam = Gobject.GetWithTag<Cam>(Constant.Tags.MainCamera);
            cam.Followable = true;

            Movable = true;
            Jumpable = true;
            IsMirroring = false;
        }

        void FixedUpdate()
        {
            Move();
        }

        void Update()
        {
            IsFloating = !flag.OnGround;
            DetectInput();
            Jump();
            Flip();
            Respawn();
            RBDisable();
            Punish();
        }

        void LateUpdate()
        {
#if DEBUG
            Velocity = rb.velocity;
            // 速度表示
            velT.SetText(Velocity);
#endif
        }

        void DetectInput()
        {
            if (!Controllable)
            {
                IsPressedMovementKeys = false;
                return;
            }
            IsPressedMovementKeys = Inputs.PressedOR(Constant.Keys.Horizontal, Constant.Keys.Jump);

            // if (Inputs.Released(Constant.Keys.Horizontal) || Inputs.Released(Constant.Keys.ReversedHorizontal))
            if (Inputs.ReleasedOR(Constant.Keys.Horizontal, Constant.Keys.ReversedHorizontal))
            {
                Mirrorable = false;
                mirror.durationSW.Restart();
            }

            if (mirror.durationSW.Sf >= mirror.Duration)
            {
                Mirrorable = true;
            }
        }

        /// <summary>
        /// スペースでリスポーンする
        /// </summary>
        void Respawn()
        {
            if (!IsDieProcessing && Inputs.Down(Constant.Keys.Respawn))
            {
                ReturnToCheckpoint();
            }
        }

        /// <summary>
        /// 反重力
        /// </summary>
        void RBDisable()
        {
            rb.isKinematic = IsTeleporting;
        }

        float currentMaxDuration = 0f;
        bool isPunishing = true;
        readonly Stopwatch punishSW = new();
        public async UniTask Punishment(float duration)
        {
            await UniTask.Yield();
            isPunishing = true;
            punishSW.Restart();
            currentMaxDuration = duration;
        }

        void Punish()
        {
            // パニッシュ中、パニッシュタイマーが最大パニッシュ時間/2より大きい
            if (isPunishing && punishSW.Sf >= currentMaxDuration / 2)
            {
                isPunishing = false;
                punishSW.Reset();
            }
        }

        void Flip()
        {
            if (!Controllable || menu.IsPausing || IsTeleporting)
            {
                return;
            }

            var horizontal = IsMirroring ? Constant.Keys.ReversedHorizontal : Constant.Keys.Horizontal;
            if (Input.GetButtonDown(horizontal))
            {
                var current = Mathf.Sign(transform.localScale.x);
                switch (Mathf.Sign(Input.GetAxisRaw(horizontal)))
                {
                    case 1:
                        if (current != 1)
                        {
                            transform.localScale *= new Vector2(-1, 1);
                        }
                        break;
                    case -1:
                        if (current != -1)
                        {
                            transform.localScale *= new Vector2(-1, 1);
                        }
                        break;
                }
            }
        }

        void Jump()
        {
            if (!Controllable || !Jumpable || IsTeleporting)
            {
                return;
            }

            if (flag.OnGround)
            {
                if (Inputs.Down(Constant.Keys.Jump))
                {
                    rb.velocity += JumpPower * Vector100.Y2D;
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
            if (!Controllable || !Movable || IsTeleporting)
            {
                return;
            }

            animator.SetBool(Constant.Animations.Walk, Input.GetButton(Constant.Keys.Horizontal) && flag.OnGround);

            string horizontal = IsMirroring ? Constant.Keys.ReversedHorizontal : Constant.Keys.Horizontal;
            var move = Input.GetAxisRaw(horizontal) * Vector100.X2D;

            // 入力がtolerance以下、氷に乗っていない、浮いていない
            if (move.magnitude <= InputTolerance && !flag.OnIce) // && !IsFloating)
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
            var velocity = IsFloating ? speed.basis * speed.floatingReduction : speed.basis;
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
            Gobject.Finds<Carrot>().ForEach(c => Shorthand.BoolAction(c.Mendable, c.Mend));

            ReturnToCheckpoint();
            animator.StopPlayback();

            cam.Followable = true;
            Controllable = true;
            IsDieProcessing = false;
        }

        public async UniTask Die() => await Die(CuzOfDeath.None);
    }
}
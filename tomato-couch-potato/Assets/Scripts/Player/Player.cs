using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using trrne.Box;
using trrne.Brain;
using System.Text;
using System.Linq;
using System;
using UnityEditor;
using System.Collections.Generic;

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
        (bool able, float Duration, Stopwatch durationSW) mirror = (false, .33f, new());
        /// <summary>
        /// 反転可能か
        /// </summary>
        public bool Mirrorable => mirror.able;

        bool isPressedMovementKeys = false;
        /// <summary>
        /// 移動キーが押されているか
        /// </summary>
        public bool IsPressedMovementKeys => isPressedMovementKeys;

        /// <summary>
        /// テレポート中か
        /// </summary>
        public bool IsTeleporting { get; set; }

        /// <summary>
        /// 死亡処理中はtrue
        /// </summary>
        public bool IsDieProcessing { get; set; }

        (float basis, float max, float floatingReduction, float reduction) speed => (20, 10, 0.1f, 0.9f);
        const float JumpPower = 6f;

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
        new BoxCollider2D collider;

        public Vector3 CoreOffset => new(0, collider.bounds.size.y / 2);

        const float InputTolerance = 0.33f;

        Vector2 checkpoint = Vector2.zero;
        public Vector2 Checkpoint => checkpoint;

        /// <summary>
        /// チェックポイントを設定する
        /// </summary>
        public void SetCheckpoint(Vector2 position) => checkpoint = position;

        /// <summary>
        /// チェックポイントに戻す
        /// </summary>
        public void ReturnToCheckpoint() => transform.SetPosition(checkpoint);

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

            LotteryPair<string, float> pairs = new(("karachan", 1), ("woko", 5), ("kuri", 44), ("goma", 50));
            for (int i = 0; i < 100; i++)
            {
                print(pairs.Weighted());
            }
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
                isPressedMovementKeys = false;
                return;
            }
            isPressedMovementKeys = Inputs.PressedOR(Constant.Keys.Horizontal, Constant.Keys.Jump);

            // if (Inputs.Released(Constant.Keys.Horizontal) || Inputs.Released(Constant.Keys.ReversedHorizontal))
            if (Inputs.ReleasedOR(Constant.Keys.Horizontal, Constant.Keys.ReversedHorizontal))
            {
                mirror.able = false;
                mirror.durationSW.Restart();
            }

            if (mirror.durationSW.Sf >= mirror.Duration)
            {
                mirror.able = true;
            }
        }

        /// <summary>
        /// スペースでリスポーンする
        /// </summary>
        void Respawn()
        {
            if (!IsDieProcessing && Inputs.Down(KeyCode.Space))
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
                        if (current != 1) { transform.localScale *= new Vector2(-1, 1); }
                        break;
                    case -1:
                        if (current != -1) { transform.localScale *= new Vector2(-1, 1); }
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

            if (flag.IsHit)
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

            animator.SetBool(Constant.Animations.Walk, Input.GetButton(Constant.Keys.Horizontal) && flag.IsHit);

            string horizontal = IsMirroring ? Constant.Keys.ReversedHorizontal : Constant.Keys.Horizontal;
            Vector2 move = Input.GetAxisRaw(horizontal) * Vector100.X;

            // 入力がtolerance以下、氷に乗っていない、浮いていない
            if (move.magnitude <= InputTolerance && !flag.OnIce && !isFloating)
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
            var velocity = isFloating ? speed.basis * speed.floatingReduction : speed.basis;
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
            Gobject.Finds<Carrot>().ForEach(c => c.IsBreaking.BoolAction(c.Mend));

            ReturnToCheckpoint();
            animator.StopPlayback();

            cam.Followable = true;
            Controllable = true;
            IsDieProcessing = false;
        }

        public async UniTask Die() => await Die(CuzOfDeath.None);
    }
}
using System.Collections;
using UnityEngine;
using Cysharp.Threading.Tasks;
using trrne.Box;
using trrne.Brain;

namespace trrne.Core
{
    /// <summary>
    /// 死因
    /// </summary>
    public enum CuzOfDeath
    {
        Venom,
        None
    }

    public enum EffectType
    {
        Mirror,
        Chain,
        Fetters
    }

    public class Player : MonoBehaviour, ICreature
    {
        [SerializeField]
        GameObject diefx;

        public bool Controllable { get; set; }
        // public bool Jumpable { get; set; }
        public bool Movable { get; set; }

        public bool IsMoveKeyDetecting { get; private set; }
        public bool IsTeleporting { get; set; }
        public bool IsDieProcessing { get; set; }

        int punishIdx = -1;
        public bool[] Effectables { get; private set; }
        public bool[] EffectFlags { get; set; }

        const float BaseSpeed = 20;
        const float MaxSpeed = 10;
        const float FloatingReductionRatio = 0.95f;
        const float MoveReductionRatio = 0.9f;
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
        BoxCollider2D hitbox;

        public Vector3 CoreOffset => new(0, hitbox.bounds.size.y / 2);

        const float InputTolerance = 0.33f;

        public Vector2 Checkpoint { get; private set; }
        public void SetCheckpoint(Vector2 position) => Checkpoint = position;
        public void ReturnToCheckpoint() => transform.position = Checkpoint;

        void Start()
        {
            Checkpoint = Vector2.zero;

            menu = Gobject.GetWithTag<PauseMenu>(Constant.Tags.Manager);
            flag = transform.GetFromChild<PlayerFlag>();
            cam = Gobject.GetWithTag<Cam>(Constant.Tags.MainCamera);

            animator = GetComponent<Animator>();
            hitbox = GetComponent<BoxCollider2D>();
            rb = GetComponent<Rigidbody2D>();
            rb.mass = 60f;

            Movable = true;
            // Jumpable = true;

            int length = new EffectType().EnumLength();
            EffectFlags = new bool[length];
            Effectables = new bool[length];
            for (int i = 0; i < EffectFlags.Length; i++)
            {
                EffectFlags[i] = false;
                Effectables[i] = true;
            }
        }

        void FixedUpdate()
        {
            Move();
        }

        void Update()
        {
            Flagger();
            Jump();
            Flip();
            Respawn();
            Punish();
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
        void Flagger()
        {
            IsFloating = !flag.OnGround;
            rb.isKinematic = IsTeleporting;
        }

        public IEnumerator Punishment(float duration, EffectType type)
        {
            if (punishIdx != -1 && Effectables[punishIdx = (int)type])
            {
                EffectFlags[punishIdx] = true;
                yield return new WaitForSeconds(duration);
                EffectFlags[punishIdx] = false;
                punishIdx = -1;
            }
        }

        void Punish()
        {
            for (int i = 0; i < EffectFlags.Length; i++)
            {
                Effectables[i] = !EffectFlags[i];
            }
        }

        void Flip()
        {
            if (!Controllable || menu.IsPausing || IsTeleporting)
            {
                return;
            }

            var horizontal = EffectFlags[(int)EffectType.Mirror] ? Constant.Keys.ReversedHorizontal : Constant.Keys.Horizontal;
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
            if (!Controllable || EffectFlags[(int)EffectType.Chain] || IsTeleporting)
            {
                return;
            }

            if (!flag.OnGround)
            {
                animator.SetBool(Constant.Animations.Jump, true);
                return;
            }

            if (Inputs.Down(Constant.Keys.Jump))
            {
                rb.velocity += JumpPower * Vector100.Y2D;
            }
            animator.SetBool(Constant.Animations.Jump, false);
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

            var horizontal = EffectFlags[(int)EffectType.Mirror] ? Constant.Keys.ReversedHorizontal : Constant.Keys.Horizontal;
            var move = Input.GetAxisRaw(horizontal) * Vector100.X2D;

            // 入力がtolerance以下、氷に乗っていない、浮いていない
            if (move.magnitude <= InputTolerance && !flag.OnIce) // && !IsFloating)
            {
                // x軸の速度をspeed.reduction倍
                rb.SetVelocityX(rb.velocity.x * MoveReductionRatio);
            }

            var limit = MaxSpeed;
            if (EffectFlags[(int)EffectType.Fetters])
            {
                limit = MaxSpeed / 2;
            }
            else if (flag.OnIce)
            {
                limit = MaxSpeed * 2;
            }
            var v = rb.velocity;
            rb.velocity = new(Mathf.Clamp(v.x, -limit, limit), Mathf.Clamp(v.y, -limit, limit));

            // 浮いていたら移動速度低下
            var velocity = IsFloating ? BaseSpeed * FloatingReductionRatio : BaseSpeed;
            rb.velocity += Time.fixedDeltaTime * velocity * move;
        }

        /// <summary>
        /// 成仏
        /// </summary>
        public async UniTask Die(CuzOfDeath cause = CuzOfDeath.None)
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

            // 落とし穴直す
            Gobject.Finds<Carrot>().ForEach(c => Simple.If(c.Mendable, c.Mend));

            ReturnToCheckpoint();
            animator.StopPlayback();

            cam.Followable = true;
            Controllable = true;
            IsDieProcessing = false;
        }

        public async UniTask Die() => await Die(CuzOfDeath.None);
    }
}
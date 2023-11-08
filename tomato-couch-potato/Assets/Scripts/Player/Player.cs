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

    public enum PunishType
    {
        Random = 0,
        Mirror = 1,
    }

    public class Player : MonoBehaviour, ICreature
    {
        [SerializeField]
        GameObject diefx;

        public bool Controllable { get; set; }
        public bool Jumpable { get; set; }
        public bool Movable { get; set; }

        public bool IsMoveKeyDetecting { get; private set; }
        public bool IsTeleporting { get; set; }
        public bool IsDieProcessing { get; set; }

        int punishIdx = -1;
        public bool[] Punishables { get; private set; }
        public bool[] PunishFlags { get; set; }

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
            cam.Followable = true;

            animator = GetComponent<Animator>();
            hitbox = GetComponent<BoxCollider2D>();
            rb = GetComponent<Rigidbody2D>();
            rb.mass = 60f;

            Movable = true;
            Jumpable = true;

            int length = new PunishType().EnumLength();
            PunishFlags = new bool[length];
            Punishables = new bool[length];
            for (int i = 0; i < PunishFlags.Length; i++)
            {
                PunishFlags[i] = false;
                Punishables[i] = true;
            }
        }

        void FixedUpdate()
        {
            Move();
        }

        void Update()
        {
            IsFloating = !flag.OnGround;
            Jump();
            Flip();
            Respawn();
            RBDisable();
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
        void RBDisable()
        {
            rb.isKinematic = IsTeleporting;
        }

        public IEnumerator Punishment(float duration, PunishType type)
        {
            if (Punishables[punishIdx = (int)type])
            {
                PunishFlags[punishIdx] = true;
                yield return new WaitForSeconds(duration);
                PunishFlags[punishIdx] = false;
                punishIdx = -1;
            }
        }

        void Punish()
        {
            print("punish type: " + punishIdx);
            for (int i = 0; i < PunishFlags.Length; i++)
            {
                Punishables[i] = !PunishFlags[i];
            }
        }

        void Flip()
        {
            if (!Controllable || menu.IsPausing || IsTeleporting)
            {
                return;
            }

            var horizontal = PunishFlags[(int)PunishType.Mirror] ? Constant.Keys.ReversedHorizontal : Constant.Keys.Horizontal;
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

            // string horizontal = IsMirroring ? Constant.Keys.ReversedHorizontal : Constant.Keys.Horizontal;
            string horizontal = PunishFlags[(int)PunishType.Mirror] ? Constant.Keys.ReversedHorizontal : Constant.Keys.Horizontal;
            var move = Input.GetAxisRaw(horizontal) * Vector100.X2D;

            // 入力がtolerance以下、氷に乗っていない、浮いていない
            if (move.magnitude <= InputTolerance && !flag.OnIce) // && !IsFloating)
            {
                // x軸の速度をspeed.reduction倍
                rb.SetVelocityX(rb.velocity.x * MoveReductionRatio);
            }

            // 速度を制限
            rb.velocity = new(flag.OnIce ?
                // 氷の上になら制限を緩和
                Mathf.Clamp(rb.velocity.x, -MaxSpeed * 2, MaxSpeed * 2) :
                Mathf.Clamp(rb.velocity.x, -MaxSpeed, MaxSpeed),
                rb.velocity.y
            );

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

            // 落とし穴修繕
            Gobject.Finds<Carrot>().ForEach(c => Shorthand.If(c.Mendable, c.Mend));

            ReturnToCheckpoint();
            animator.StopPlayback();

            cam.Followable = true;
            Controllable = true;
            IsDieProcessing = false;
        }

        public async UniTask Die() => await Die(CuzOfDeath.None);
    }
}
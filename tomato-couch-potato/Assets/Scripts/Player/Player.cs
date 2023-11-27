using System.Collections;
using UnityEngine;
using Cysharp.Threading.Tasks;
using trrne.Box;
using trrne.Brain;

namespace trrne.Core
{
    public enum Cause
    {
        Muscarine,
        Fallen,
        None,
    }

    public enum Effect
    {
        Mirror,
        Chain,
        Fetters,
    }

    public class Player : MonoBehaviour, ICreature
    {
        [SerializeField]
        GameObject diefx;

        public bool Controllable { get; set; }
        public bool IsMoveKeyDetecting { get; private set; }
        public bool IsTeleporting { get; set; }
        public bool IsDying { get; private set; } = false;

        int fxidx = -1;
        public bool[] Effectables { get; private set; }
        public bool[] EffectFlags { get; set; }

        readonly (float basis, float max) speed = (20, 10);
        readonly (float fetters, float floating, float move) reduction = (0.5f, 0.95f, 0.9f);

        readonly (float floating, float basis) gscale = (2f, 1f);

        const float JumpPower = 10f;

        public bool IsFloating { get; private set; }
        public Vector2 Velocity { get; private set; }

        Rigidbody2D rb;
        Animator animator;
        PlayerFlag flag;
        Cam cam;
        PauseMenu menu;
        BoxCollider2D hitbox;

        public Vector3 CoreOffset { get; private set; }

        const float InputTolerance = 0.33f;

        public Vector2 Checkpoint { get; private set; } = Vector2.zero;
        public void SetCheckpoint(Vector2 position) => Checkpoint = position;
        public void SetCheckpoint(float x, float y) => Checkpoint = new(x, y);
        public void ReturnToCheckpoint() => transform.position = Checkpoint;

        Vector2 reverse => new(-1, 1);

        void Start()
        {
            flag = transform.GetFromChild<PlayerFlag>();
            menu = Gobject.GetWithTag<PauseMenu>(Constant.Tags.Manager);
            cam = Gobject.GetWithTag<Cam>(Constant.Tags.MainCamera);
            animator = GetComponent<Animator>();
            hitbox = GetComponent<BoxCollider2D>();
            rb = GetComponent<Rigidbody2D>();
            rb.gravityScale = gscale.basis;

            int length = new Effect().Length();
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
            Def();
            Jump();
            Flip();
            Respawn();
            PunishFlagsUpdater();
        }

        void Def()
        {
            CoreOffset = transform.position + new Vector3(0, hitbox.bounds.size.y / 2);
            rb.bodyType = IsTeleporting ? RigidbodyType2D.Static : RigidbodyType2D.Dynamic;
            rb.gravityScale = (IsFloating = !flag.OnGround) ? gscale.floating : gscale.basis;
        }

        /// <summary>
        /// スペースでリスポーンする
        /// </summary>
        void Respawn()
        {
            if (IsDying)
            {
                return;
            }

            if (Inputs.Down(Constant.Keys.Respawn))
            {
                ReturnToCheckpoint();
            }
        }

        public IEnumerator Punishment(float duration, Effect effect)
        {
            if (fxidx == -1 && Effectables[fxidx = (int)effect])
            {
                EffectFlags[fxidx] = true;
                yield return new WaitForSeconds(duration);
                EffectFlags[fxidx] = false;
                fxidx = -1;
            }
        }

        void PunishFlagsUpdater()
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
            string horizontal = EffectFlags[(int)Effect.Mirror] ?
                Constant.Keys.MirroredHorizontal : Constant.Keys.Horizontal;
            if (Input.GetButtonDown(horizontal))
            {
                int current = Maths.Sign(transform.localScale.x);
                switch (Mathf.Sign(Input.GetAxisRaw(horizontal)))
                {
                    case 1:
                        if (current != 1)
                            transform.localScale *= reverse;
                        break;
                    case -1:
                        if (current != -1)
                            transform.localScale *= reverse;
                        break;
                }
            }
        }

        void Jump()
        {
            if (!Controllable || EffectFlags[(int)Effect.Chain] || IsTeleporting)
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
                rb.velocity += JumpPower * Coordinate.V2Y;
            }
            animator.SetBool(Constant.Animations.Jump, false);
        }

        /// <summary>
        /// 移動
        /// </summary>
        void Move()
        {
            if (!Controllable || IsTeleporting)
            {
                return;
            }

            bool walk = Inputs.Pressed(Constant.Keys.Horizontal) && flag.OnGround;
            animator.SetBool(Constant.Animations.Walk, walk);

            string horizontal = EffectFlags[(int)Effect.Mirror] ?
                Constant.Keys.MirroredHorizontal : Constant.Keys.Horizontal;
            Vector2 move = Input.GetAxisRaw(horizontal) * Coordinate.V2X;

            // 入力がtolerance以下、氷に乗っていない
            if (move.magnitude <= InputTolerance && !flag.OnIce)
            {
                // x軸の速度をspeed.reduction倍
                rb.SetVelocity(x: rb.velocity.x * reduction.move);
            }

            float limit = Shorthand.L1ne(() =>
            {
                if (EffectFlags[(int)Effect.Fetters])
                {
                    return speed.max * reduction.fetters;
                }
                else if (flag.OnIce)
                {
                    return speed.max * 2;
                }
                return speed.max;
            });

            // x軸の速度を制限
            if (rb.bodyType != RigidbodyType2D.Static)
            {
                rb.ClampVelocity(x: (-limit, limit));
            }

            // 浮いていたら移動速度低下
            float scalar = IsFloating ? reduction.floating : 1;
            rb.velocity += Time.fixedDeltaTime * speed.basis * scalar * move;
        }

        /// <summary>
        /// 成仏
        /// </summary>
        public async UniTask Die(Cause cause = Cause.None)
        {
            if (IsDying)
            {
                return;
            }

            hitbox.excludeLayers += Constant.Layers.Creature;

            IsDying = true;
            Controllable = cam.Followable = false;

            diefx.TryInstantiate(transform.position);

            switch (cause)
            {
                case Cause.None: break;
                case Cause.Muscarine:
                    rb.velocity = Vector2.zero;
                    animator.Play(Constant.Animations.Venomed);
                    break;
                case Cause.Fallen:
                    animator.Play(Constant.Animations.Die);
                    break;
                default: break;
            }

            await UniTask.Delay(1250);

            // remove active fxs
            for (int i = 0; i < EffectFlags.Length; i++)
            {
                EffectFlags[i] = false;
            }

            // mend carrots
            Carrot[] carrots = Gobject.Finds<Carrot>();
            carrots.ForEach(carrot => carrot.Mendable.If(carrot.Mend));

            ReturnToCheckpoint();
            animator.StopPlayback();

            hitbox.excludeLayers -= Constant.Layers.Creature;

            cam.Followable = Controllable = true;
            IsDying = false;
        }

        public async UniTask Die() => await Die(Cause.None);
    }
}
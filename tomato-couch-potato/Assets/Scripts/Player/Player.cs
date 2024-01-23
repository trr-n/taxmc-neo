using System.Reflection;
using System;
using System.Collections;
using UnityEngine;
using Cysharp.Threading.Tasks;
using trrne.Box;
using trrne.Brain;
using UnityEditor.Rendering;

namespace trrne.Core
{
    public enum Cause
    {
        None,
        Hizakarakuzureotiru,
        Muscarine,  // 毒
        Fallen,     // 落下死
    }

    public enum PunishEffect
    {
        Mirror,     // 操作左右反転
        Chain,      // ジャンプ不可
        Fetters,    // 移動速度低下
    }

    public class Player : MonoBehaviour, ICreature
    {
        [SerializeField]
        GameObject diefx;

        [SerializeField]
        AudioClip[] jumpSEs;

        AudioSource speaker;

        /// <summary> 操作フラグ </summary>
        public bool Controllable { get; set; }

        /// <summary> テレポート中か </summary>
        public bool IsTeleporting { get; set; } = false;

        /// <summary> 死亡処理中か </summary>
        public bool IsDying { get; private set; } = false;

        /// <summary> 樽のなかにいるか </summary>
        bool inBarrel = false;
        public bool IsAfterBarrel { get; set; } = false;
        public void BarrelProcess(bool flag)
        {
            rb.velocity *= 0;
            inBarrel = rb.isKinematic = flag;
            rb.gravityScale = flag ? 0 : GRAVITY_SCALE.BASIS;
            transform.GetChildrenGameObject().ForEach(child => child.SetActive(!flag));
        }

        // punish effect
        int fxidx = -1;
        public bool[] Punishables { get; private set; }
        public bool[] PunishFlags { get; set; }
        int effectLength => new PunishEffect().Length();

        // (float BASIS, float MAX) speed => (20f, 10f);
        readonly struct SPEED { public const float BASIS = 20f, MAX = 10; }
        // (float FETTERS, float FLOATING, float MOVE) red => (0.5f, 0.95f, 0.9f);
        readonly struct REDUCTION { public const float FETTERS = 0.5f, FLOATING = 0.95f, MOVE = 0.9f; }
        // (float FLOATING, float BASIS) GRAVITY_SCALE => (3f, 1f);
        readonly struct GRAVITY_SCALE { public const float FLOATING = 3, BASIS = 1; }

        // Movement
        const float JUMP_POWER = 13f;

        public bool IsFloating { get; private set; }
        // (bool ICE, bool GROUND) on = (false, false);
        struct on { public static bool ice, ground; }

        Rigidbody2D rb;
        Animator animator;
        Cam cam;
        PauseMenu menu;
        BoxCollider2D hitbox;

        public Vector3 Core { get; private set; }

        const float INPUT_TOLERANCE = 1 / 3;

        public Vector2 Checkpoint { get; private set; } = default;
        public void SetCheckpoint(Vector2 position)
        {
            MendingDoableObjects();
            Checkpoint = position;
        }
        public void SetCheckpoint(float? x = null, float? y = null)
        => SetCheckpoint(new(x ?? transform.position.x, y ?? transform.position.y));

        public void ReturnToCheckpoint()
        {
            rb.velocity = Vector2.zero;
            transform.position = Checkpoint;
            inBarrel = false;
        }

        Vector2 reverse => new(-1, 1);

        Vector2 box;
        Vector2 hitboxSizeRatio => new(.8f, .1f);

        const int ANIMATION_DELAY = 1250;

        void Start()
        {
            menu = Gobject.GetWithTag<PauseMenu>(Constant.Tags.MANAGER);
            cam = Gobject.GetWithTag<Cam>(Constant.Tags.MAIN_CAMERA);
            animator = GetComponent<Animator>();
            hitbox = GetComponent<BoxCollider2D>();
            rb = GetComponent<Rigidbody2D>();
            rb.gravityScale = GRAVITY_SCALE.BASIS;
            speaker = GetComponent<AudioSource>();

            PunishFlags = new bool[effectLength].Set(false);
            Punishables = new bool[effectLength].Set(true);

            box = new(hitbox.bounds.size.x * hitboxSizeRatio.x, hitbox.bounds.size.y * hitboxSizeRatio.y);
        }

        void FixedUpdate()
        {
            Move();
        }

        void Update()
        {
            Def();
            Footer();
            Jump();
            Flip();
            Respawn();
            PunishFlagsUpdater();
#if DEBUG
            if (Inputs.Down(KeyCode.H))
            {
                transform.SetPosition(133f, 18f);
            }
#endif
        }

#if false
        [SerializeField]
        Text floatingT;

        void LateUpdate()
        {
            floatingT.SetText($"OnIce: {on.ice}\nOnGround: {on.ground}");
        }
#endif

        void Def()
        {
            if (!inBarrel)
            {
                Core = transform.position + Vec.Make3(y: hitbox.bounds.size.y / 2);
                rb.bodyType = IsTeleporting ? RigidbodyType2D.Static : RigidbodyType2D.Dynamic;
                rb.gravityScale = (IsFloating = !on.ground) ? GRAVITY_SCALE.FLOATING : GRAVITY_SCALE.BASIS;
            }
        }

        void Footer()
        {
            if (inBarrel)
            {
                return;
            }

            if (!Gobject.Boxcast(out var hit, transform.position, box, Constant.Layers.JUMPABLE))
            {
                on.ice = on.ground = false;
                return;
            }
            IsAfterBarrel = false;
            on.ground = hit.CompareLayer(Constant.Layers.JUMPABLE);
            on.ice = hit.CompareTag(Constant.Tags.ICE);
        }

#if DEBUG
        void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position, box);
        }
#endif

        /// <summary>
        /// スペースでリスポーンする
        /// </summary>
        void Respawn()
        {
            if (!IsDying && Inputs.Down(Constant.Keys.RESPAWN) && !inBarrel)
            {
                ReturnToCheckpoint();
            }
        }

        public IEnumerator Punishment(float duration, PunishEffect effect)
        {
            if (fxidx == -1 && Punishables[fxidx = (int)effect])
            {
                PunishFlags[fxidx] = true;
                yield return new WaitForSeconds(duration);
                PunishFlags[fxidx] = false;
                fxidx = -1;
            }
        }

        void PunishFlagsUpdater()
        {
            for (int i = 0; i < effectLength; ++i)
            {
                Punishables[i] = !PunishFlags[i];
            }
        }

        void Flip()
        {
            if (inBarrel || !Controllable || menu.IsPausing || IsTeleporting)
            {
                return;
            }

            var horizontal = PunishFlags[(int)PunishEffect.Mirror] ? Constant.Keys.MIRRORED_HORIZONTAL : Constant.Keys.HORIZONTAL;
            if (Inputs.Down(horizontal))
            {
                var pre = MathF.Sign(transform.localScale.x);
                var haxis = Input.GetAxisRaw(horizontal);
                if (MathF.Sign(haxis) != 0 && MathF.Sign(haxis) != pre)
                {
                    transform.localScale *= reverse;
                }
            }
        }

        void Jump()
        {
            if (!Controllable || PunishFlags[(int)PunishEffect.Chain] || IsTeleporting || inBarrel)
            {
                return;
            }

            if (!on.ground)
            {
                animator.SetBool(Constant.Animations.JUMP, true);
                return;
            }

            if (Inputs.Down(Constant.Keys.JUMP))
            {
                speaker.PlayOneShot(jumpSEs.Choice());
                rb.velocity += new Vector2(0, JUMP_POWER);
            }
            animator.SetBool(Constant.Animations.JUMP, false);
        }

        /// <summary>
        /// 移動
        /// </summary>
        void Move()
        {
            if (!Controllable || IsTeleporting || inBarrel || IsAfterBarrel)
            {
                return;
            }

            animator.SetBool(Constant.Animations.WALK, on.ground && Inputs.Pressed(Constant.Keys.HORIZONTAL));

            var horizon = PunishFlags[(int)PunishEffect.Mirror] ? Constant.Keys.MIRRORED_HORIZONTAL : Constant.Keys.HORIZONTAL;
            var move = Vec.Make2(x: Input.GetAxisRaw(horizon));

            // 入力がtolerance以下、氷に乗っていない
            if (move.magnitude <= INPUT_TOLERANCE && !on.ice)
            {
                // x軸の速度をspeed.reduction倍
                rb.SetVelocity(x: rb.velocity.x * REDUCTION.MOVE);
            }

            // x軸の速度を制限
            if (rb.bodyType != RigidbodyType2D.Static)
            {
                var limit = Shorthand.L1ne(() =>
                {
                    if (PunishFlags[(int)PunishEffect.Fetters])
                    {
                        return SPEED.MAX * REDUCTION.FETTERS;
                    }
                    else if (on.ice)
                    {
                        return SPEED.MAX * 2;
                    }
                    return SPEED.MAX;
                });
                rb.ClampVelocity(x: (-limit, limit));
            }
            rb.velocity += Time.fixedDeltaTime * SPEED.BASIS * (IsFloating ? REDUCTION.FLOATING : 1f) * move;
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

            // 敵への当たり判定は除外
            hitbox.excludeLayers += Constant.Layers.CREATURE;

            IsDying = true;
            Controllable = cam.Followable = false;

            diefx.TryInstantiate(transform.position);

            switch (cause)
            {
                case Cause.None:
                    break;
                case Cause.Muscarine:
                case Cause.Hizakarakuzureotiru:
                    rb.velocity = Vector2.zero;
                    animator.Play(Constant.Animations.VENOMED);
                    break;
                case Cause.Fallen:
                    animator.Play(Constant.Animations.DIE);
                    break;
            }

            await UniTask.Delay(ANIMATION_DELAY);

            // mend
            MendingDoableObjects();

            ReturnToCheckpoint();
            animator.StopPlayback();

            hitbox.excludeLayers -= Constant.Layers.CREATURE;

            cam.Followable = Controllable = true;
            IsDying = false;

            // エフェクトを削除
            PunishFlags.Set(false);
        }

        public async UniTask Die() => await Die(Cause.None);

        public void MendingDoableObjects()
        {
            Gobject.Finds<Carrot>().ForEach(carrot => carrot.Mendable.If(carrot.Mend));
            Gobject.Finds<MoroiFloor>().ForEach(moro => moro.Mendable.If(moro.Mend));
        }
    }
}
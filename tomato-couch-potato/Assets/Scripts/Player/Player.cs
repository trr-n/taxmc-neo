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
        Fallen,
        None,
    }

    /// <summary>
    /// エフェクト
    /// </summary>
    public enum EffectType
    {
        Mirror, // 操作左右反転
        Chain,  // ジャンプ不可
        Fetters, // 移動速度低下
    }

    public class Player : MonoBehaviour, ICreature
    {
        [SerializeField]
        GameObject diefx;

        /// <summary>
        /// 操作可能か
        /// </summary>
        public bool Controllable { get; set; }

        /// <summary>
        /// キーが入力されているか
        /// </summary>
        public bool IsMoveKeyDetecting { get; private set; }

        /// <summary>
        /// テレポート処理中か
        /// </summary>
        public bool IsTeleporting { get; set; }

        /// <summary>
        /// 死亡処理中か
        /// </summary>
        public bool IsDieProcessing { get; set; }

        /// <summary>
        /// 与えるエフェクト
        /// </summary>
        public int EffectIdx { get; private set; } = -1;

        /// <summary>
        /// エフェクトを与えられるか
        /// </summary>
        public bool[] Effectables { get; private set; }

        /// <summary>
        /// 与えられてるエフェクト
        /// </summary>
        public bool[] EffectFlags { get; set; }

        /// <summary>
        /// 足かせが付いているときの減速比
        /// </summary>
        const float FettersRed = 0.5f;

        /// <summary>
        /// 基礎移動速度
        /// </summary>
        const float BaseSpeed = 20;

        /// <summary>
        /// 上限移動速度
        /// </summary>        
        const float MaxSpeed = 10;

        /// <summary>
        /// 空中での減速比
        /// </summary>
        const float FloatingRed = 0.95f;

        /// <summary>
        /// 減速比
        /// </summary>
        const float MoveReductionRatio = 0.9f;

        /// <summary>
        /// ジャンプ力
        /// </summary>
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

        /// <summary>
        /// プレイヤーのへそあたりの座標
        /// </summary>
        public Vector3 CoreOffset { get; private set; }

        /// <summary>
        /// 入力の許容値
        /// </summary>
        const float InputTolerance = 0.33f;

        /// <summary>
        /// 現在のチェックポイントを取得(public)/更新(private)
        /// </summary>
        public Vector2 Checkpoint { get; private set; }

        /// <summary>
        /// チェックポイントを設定する
        /// </summary>
        public void SetCheckpoint(Vector2 position) => Checkpoint = position;

        /// <summary>
        /// チェックポイントに戻る
        /// </summary>
        public void ReturnToCheckpoint() => transform.position = Checkpoint;

        Vector2 Reverse => new(-1, 1);

        void Start()
        {
            Checkpoint = Vector2.zero;

            // Movable = true;
            // Jumpable = true;

            menu = Gobject.GetComponentWithTag<PauseMenu>(Constant.Tags.Manager);
            flag = transform.GetComponentFromChild<PlayerFlag>();
            cam = Gobject.GetComponentWithTag<Cam>(Constant.Tags.MainCamera);

            animator = GetComponent<Animator>();
            hitbox = GetComponent<BoxCollider2D>();
            rb = GetComponent<Rigidbody2D>();
            rb.mass = 60f;

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
            Def();
            Jump();
            Flip();
            Respawn();
            Punish();
        }

        void Def()
        {
            CoreOffset = transform.position + new Vector3(0, hitbox.bounds.size.y / 2);
            IsFloating = !flag.OnGround;
            rb.isKinematic = IsTeleporting;
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

        public IEnumerator Punishment(float duration, EffectType type)
        {
            if (EffectIdx == -1 && Effectables[EffectIdx = (int)type])
            {
                EffectFlags[EffectIdx] = true;
                yield return new WaitForSeconds(duration);
                EffectFlags[EffectIdx] = false;
                EffectIdx = -1;
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
                    case 0:
                        return;
                    case 1:
                        Shorthand.If(current != 1, () => transform.localScale *= Reverse);
                        break;
                    case -1:
                        Shorthand.If(current != -1, () => transform.localScale *= Reverse);
                        break;
                }
            }
        }

        void Jump()
        {
            if (!Controllable || EffectFlags[(int)EffectType.Chain] || IsTeleporting) // || Jumpable)
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
            if (!Controllable || IsTeleporting) // || !Movable)
            {
                return;
            }

            bool flag = Input.GetButton(Constant.Keys.Horizontal) && this.flag.OnGround;
            animator.SetBool(Constant.Animations.Walk, flag);

            string horizontal = EffectFlags[(int)EffectType.Mirror] ?
                Constant.Keys.ReversedHorizontal : Constant.Keys.Horizontal;
            var move = Input.GetAxisRaw(horizontal) * Coordinate.V2X;

            // 入力がtolerance以下、氷に乗っていない、浮いていない
            if (move.magnitude <= InputTolerance && !this.flag.OnIce) // && !IsFloating)
            {
                // x軸の速度をspeed.reduction倍
                rb.SetVelocity(x: rb.velocity.x * MoveReductionRatio);
            }

            float limit = Shorthand.L1ne(() =>
            {
                if (EffectFlags[(int)EffectType.Fetters])
                {
                    return MaxSpeed * FettersRed;
                }
                else if (this.flag.OnIce)
                {
                    return MaxSpeed * 2;
                }
                return MaxSpeed;
            });

            var v = rb.velocity;
            rb.velocity = new(Mathf.Clamp(v.x, -limit, limit), v.y);
            // rb.ClampVelocity(x: (-limit, limit));

            // 浮いていたら移動速度低下
            float velocity = IsFloating ? BaseSpeed * FloatingRed : BaseSpeed;
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

            diefx.TryInstantiate(transform.position);

            switch (cause)
            {
                case CuzOfDeath.Venom:
                    rb.velocity = Vector2.zero;
                    animator.Play(Constant.Animations.Venomed);
                    break;
                case CuzOfDeath.Fallen:
                    animator.Play(Constant.Animations.Die);
                    break;
                default: break;
            }

            await UniTask.Delay(1250);

            // 落とし穴直す
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
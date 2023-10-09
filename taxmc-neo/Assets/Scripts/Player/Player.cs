using UnityEngine;
using UnityEngine.UI;
using trrne.Bag;
using Cysharp.Threading.Tasks;
using System.Collections;

namespace trrne.Body
{
    public enum CauseOfDeath
    {
        Venom,  // 毒死
        None    // 何もしない
    }

    public class Player : MonoBehaviour
    {
        [SerializeField]
        Text velT;

        [SerializeField]
        GameObject diefx;

        public bool controllable { get; set; }
        public bool jumpable { get; set; }
        public bool walkable { get; set; }

        /// <summary>
        /// テレポート中か
        /// </summary>
        public bool isTeleporting { get; set; }

        /// <summary>
        /// 死亡処理中はtrue
        /// </summary>
        public bool isDieProcessing { get; set; }

        readonly (float basis, float max, float freduction, float reduction) speed = (20, 10, 0.1f, 0.9f);
        readonly float jumpPower = 6f;

        bool floating;
        /// <summary>
        /// 地に足がついていなかったらtrue
        /// </summary>
        public bool isFloating => floating;

        Vector2 vel;
        /// <summary>
        /// 移動速度
        /// </summary>
        public Vector2 velocity => vel;

        Rigidbody2D rb;
        Animator animator;
        PlayerFlag flag;
        Cam cam;

        readonly float tolerance = 0.33f;

        Vector2 cp = Vector2.zero;
        public Vector2 checkpoint => cp;
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
            flag = transform.GetFromChild<PlayerFlag>(0);

            animator = GetComponent<Animator>();

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
            RBDisable();
        }

        void LateUpdate()
        {
#if DEBUG
            vel = rb.velocity;
            // 速度表示
            velT.SetText(vel);
#endif
        }

        /// <summary>
        /// スペースでリスポーンする
        /// </summary>
        void Respawn()
        {
            if (!isDieProcessing && Inputs.Down(KeyCode.Space))
            {
                Return2CP();
            }
        }

        /// <summary>
        /// テレポート中はRB無効化
        /// </summary>
        void RBDisable()
        {
            rb.isKinematic = isTeleporting;
        }

        void Flip()
        {
            if (!controllable)
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

                    case 0:
                    default:
                        break;
                }
            }
        }

        void Jump()
        {
            if (!controllable)
            {
                return;
            }

            if (flag.isHit)
            {
                if (Inputs.Down(Constant.Keys.Jump))
                {
                    rb.velocity += jumpPower * (Vector2)Vector100.y;
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
            if (!controllable)
            {
                return;
            }

            animator.SetBool(Constant.Animations.Walk, Input.GetButton(Constant.Keys.Horizontal) && flag.isHit);

            Vector2 move = Input.GetAxisRaw(Constant.Keys.Horizontal) * Vector100.x;

            // 入力がtolerance以下、氷に乗っていない、浮いていない
            if (move.magnitude <= tolerance && !flag.onIce && !floating)
            {
                // x軸の速度をspeed.reduction倍
                rb.SetVelocityX(rb.velocity.x * speed.reduction);
            }

            // 速度を制限
            rb.velocity = new(flag.onIce ?
                // 氷の上になら制限を緩和
                Mathf.Clamp(rb.velocity.x, -speed.max * 2, speed.max * 2) :
                Mathf.Clamp(rb.velocity.x, -speed.max, speed.max),
                rb.velocity.y
            );

            // 浮いていたら移動速度低下
            float velocity = floating ? speed.basis * speed.freduction : speed.basis;
            rb.velocity += Time.fixedDeltaTime * velocity * move;
        }

        /// <summary>
        /// 成仏
        /// </summary>
        public async UniTask Die(CauseOfDeath cause)
        {
            if (isDieProcessing)
            {
                return;
            }

            isDieProcessing = true;
            controllable = false;
            cam.followable = false;

            switch (cause)
            {
                case CauseOfDeath.Venom:
                    rb.velocity = Vector2.zero;
                    animator.Play("venom_die");
                    break;

                default: break;
            }

            // 1秒待機
            await UniTask.Delay(1250);

            // 落とし穴修繕
            var holes = FindObjectsByType<Hole>(FindObjectsSortMode.None);
            foreach (var hole in holes)
            {
                if (hole.isBreaking)
                {
                    hole.Mending();
                }
            }

            StartCoroutine(AfterDelay());
        }

        public async UniTask Die() => await Die(CauseOfDeath.None);

        IEnumerator AfterDelay()
        {
            yield return null;

            // 座標リセット
            Return2CP();

            // 死亡アニメーション停止
            animator.StopPlayback();

            // うごいていいよ
            cam.followable = true;
            controllable = true;
            isDieProcessing = false;
        }
    }
}
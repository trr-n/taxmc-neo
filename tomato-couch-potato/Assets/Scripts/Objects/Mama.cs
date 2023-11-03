using System.Collections;
using System.Linq;
using trrne.Pancreas;
using UnityEngine;

namespace trrne.Heart
{
    public class Mama : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("気の短さ(秒)")]
        float pepperyLimit = 1;

        [SerializeField]
        float lineWidth = .5f;

        [SerializeField]
        float detectRange = 13f;

        [SerializeField]
        float punishDuration = 3f;

        float pepperyTimer = 0;

        public (float Limit, float Timer, float Ratio) Peppery
        => (
            Limit: pepperyLimit,
            Timer: pepperyTimer,
            Ratio: pepperyTimer / pepperyLimit
        );

        GameObject player;
        Vector3 _ofs;
        Vector3 offset;
        float distance;

        GameObject[] eyes;
        Vector3[] inits;
        Vector3[] directions;
        LineRenderer[] lines;

        /// <summary>
        /// 黒目の可動域
        /// </summary>
        readonly float bump = 0.25f;

        /// <summary>
        /// 範囲内にいるか
        /// </summary>
        bool isPlayerWithinDetectRange = false;

        /// <summary>
        /// お仕置き中か
        /// </summary>
        bool isPunishing = false;

        void Start()
        {
            player = Gobject.Find(Constant.Tags.Player);
            _ofs = new(0, player.GetComponent<BoxCollider2D>().GetSize().y / 2);

            eyes = transform.GetChildren();
            inits = new Vector3[eyes.Length];
            lines = new LineRenderer[eyes.Length];
            for (int i = 0; i < eyes.Length; i++)
            {
                inits[i] = eyes[i].transform.position;
                lines[i] = eyes[i].GetComponent<LineRenderer>();
            }
            // inits = new[] { eyes[0].transform.position, eyes[1].transform.position };
            // lines = new[] { eyes[0].GetComponent<LineRenderer>(), eyes[1].GetComponent<LineRenderer>() };
            lines.ForEach(line => line.startWidth = line.endWidth = lineWidth);
        }

        void Update()
        {
            offset = player.transform.position + _ofs;
            directions = new[] { offset - eyes[0].transform.position, offset - eyes[1].transform.position };
            distance = Maths.Average(
                Vector2.Distance(offset, eyes[0].transform.position),
                Vector2.Distance(offset, eyes[1].transform.position)
            );

            Line();
            Look();
            Detect();
            Punish();
        }

        void Look()
        {
            if (isPlayerWithinDetectRange)
            {
                for (int i = 0; i < eyes.Length; i++)
                {
                    eyes[i].transform.position = inits[i] + directions[i].normalized * bump;
                }
            }
        }

        /// <summary>
        /// プレイヤーを検知<br/>
        /// 範囲内にいたらタイマー増加
        /// </summary>
        void Detect()
        {
            pepperyTimer = Mathf.Clamp(pepperyTimer, 0, pepperyLimit);

            // 範囲内にいたら
            if (distance <= detectRange && this.player.TryGet(out Player player))
            {
                isPlayerWithinDetectRange = true;
                // キーが入力されていないか、反転可能なら増やす
                if (!player.IsPressedMovementKeys && player.Mirrorable)
                {
                    pepperyTimer += Time.deltaTime;
                }
                // 反転不可なら減らす
                else if (!player.Mirrorable)
                {
                    pepperyTimer -= Time.deltaTime;
                }
            }
            // 範囲外にいたら
            else if (distance > detectRange)
            {
                // タイマーが0以上なら減らす
                if (pepperyTimer >= 0)
                {
                    pepperyTimer -= Time.deltaTime;
                }
                isPlayerWithinDetectRange = false;
            }
        }

        void Line()
        {
            for (int i = 0; i < lines.Length; i++)
            {
                // 範囲内にいたらビーム発射
                if (distance <= detectRange)
                {
                    if (!lines[i].enabled)
                    {
                        lines[i].enabled = true;
                    }
                    lines[i].SetPositions(new[] { eyes[i].transform.position, offset });
                    return;
                }

                if (lines[i].enabled)
                {
                    lines[i].enabled = false;
                }
            }
        }

        /// <summary>
        /// 一定時間以上範囲内にいたら制裁
        /// </summary>
        void Punish()
        {
            // お仕置き中じゃなくて、範囲内に一定時間以上いたらお仕置き執行
            if (!isPunishing && pepperyTimer >= pepperyLimit)
            {
                StartCoroutine(Punishment());
            }
        }

        IEnumerator Punishment()
        {
            isPunishing = true;
            Stopwatch punishTimer = new(true);
            var player = Gobject.GetWithTag<Player>(Constant.Tags.Player);

            // お仕置き時間内なら
            while (punishTimer.Sf <= punishDuration)
            {
                // 操作反転
                player.IsMirroring = true;

                yield return null;
            }

            // 反転解除
            player.IsMirroring = false;
            isPunishing = false;
        }

#if DEBUG
        void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, detectRange);
        }
#endif
    }
}

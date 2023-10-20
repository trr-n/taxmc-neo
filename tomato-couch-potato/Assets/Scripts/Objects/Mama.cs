using System.Collections;
using System.Linq;
using Chickenen.Pancreas;
using UnityEngine;

namespace Chickenen.Heart
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
        float pepperyTimer = 0;
        readonly float punishDuration = 3f;

        public float PepperyLimit => pepperyLimit;
        public float PepperyTimer => pepperyTimer;
        public float PepperyRatio => pepperyTimer / pepperyLimit;

        GameObject player;
        Vector3 offset;
        Vector3 offseted;
        float distance;

        GameObject[] eyes;
        Vector2[] inits;
        float[] distances;
        Vector2[] directions;
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
            offset = Vector100.Y * player.GetComponent<BoxCollider2D>().GetSize().y / 2;
            eyes = transform.GetChildren();
            inits = new Vector2[] { eyes[0].transform.position, eyes[1].transform.position };

            lines = new LineRenderer[] { eyes[0].GetComponent<LineRenderer>(), eyes[1].GetComponent<LineRenderer>() };
            lines.ForEach(line => line.startWidth = line.endWidth = lineWidth);

        }

        void Update()
        {
            offseted = player.transform.position + offset;
            distances = new float[] { Vector2.Distance(offseted, eyes[0].transform.position), Vector2.Distance(offseted, eyes[1].transform.position) };
            directions = new Vector2[] { offseted - eyes[0].transform.position, offseted - eyes[1].transform.position };
            distance = Maths.Average(distances);

            Line();
            Look();
            Detect();
            Punish();
        }

        void Look()
        {
            if (isPlayerWithinDetectRange)
            {
                for (int index = 0; index < eyes.Length; index++)
                {
                    eyes[index].transform.position = inits[index] + directions[index].normalized * bump;
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
                if (!player.IsKeyEntered && player.Reversable)
                {
                    pepperyTimer += Time.deltaTime;
                }
                // 反転不可 == キーが入力されている状態なら減らす
                if (!player.Reversable)
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
            if (distance <= detectRange)
            {
                lines[0].enabled = lines[1].enabled = true;
                for (int i = 0; i < lines.Length; i++)
                {
                    lines[i].SetPositions(new Vector3[] { eyes[i].transform.position, offseted });
                }
            }
            else
            {
                lines[0].enabled = lines[1].enabled = false;
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
                yield return null;
                // 操作反転
                player.Reverse = true;
            }
            // 反転解除
            player.Reverse = false;
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

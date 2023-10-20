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
        float detect = 13f;
        float pepperyTimer = 0;
        readonly float punishDuration = 3f;

        public float PepperyLimit => pepperyLimit;
        public float PepperyTimer => pepperyTimer;
        public float PepperyRatio => pepperyTimer / pepperyLimit;

        GameObject player;
        Vector3 offset;

        GameObject[] eyes;
        Vector2[] inits;

        /// <summary>
        /// 黒目の可動域
        /// </summary>
        readonly float bump = 0.388f;

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
        }

        void Update()
        {
            Look();
            Detect();
            Punish();
        }

        float[] distances;
        void Look()
        {
            // プレイヤーの方向を見続ける
            var player = this.player.transform.position + offset;
            distances = new float[] { Vector2.Distance(player, eyes[0].transform.position), Vector2.Distance(player, eyes[1].transform.position) };
            var directions = new Vector2[] { player - eyes[0].transform.position, player - eyes[1].transform.position };
            var lines = new Ray[] { new(eyes[0].transform.position, directions[0]), new(eyes[1].transform.position, directions[1]) };

            foreach (var (distance, line) in distances.SelectMany(dis => lines.Select(line => (dis, line))))
            {
                // 目から怪光線
                Debug.DrawRay(line.origin, line.direction * distance);
            }

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

            // 左右の目の位置が若干違うから平均で
            var distance = Maths.Average(distances);

            // 範囲内にいたら
            if (distance <= detect && this.player.TryGet(out Player player))
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
            else if (distance > detect)
            {
                // タイマーが0以上なら減らす
                if (pepperyTimer >= 0)
                {
                    pepperyTimer -= Time.deltaTime;
                }
                isPlayerWithinDetectRange = false;
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
            Stopwatch timer = new(true);
            var player = Gobject.GetWithTag<Player>(Constant.Tags.Player);

            // お仕置き時間内なら
            while (timer.Sf <= punishDuration)
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
            Gizmos.DrawWireSphere(transform.position, detect);
        }
#endif
    }
}

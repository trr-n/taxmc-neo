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

        GameObject player;
        Vector3 offset;

        GameObject[] eyes;
        Vector2[] inits;

        /// <summary>
        /// 黒目の可動域
        /// </summary>
        readonly float bump = 0.388f;

        bool isTherePlayerWithinDetectRange = false;

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

            if (isTherePlayerWithinDetectRange)
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
            var distance = (distances[0] + distances[1]) / 2;
            if (distance <= detect && this.player.TryGet(out Player player))
            {
                isTherePlayerWithinDetectRange = true;
                if (!player.IsKeyEntered)
                {
                    pepperyTimer += Time.deltaTime;
                }
            }
            else
            {
                if (pepperyTimer != 0)
                {
                    pepperyTimer = 0;
                }
                isTherePlayerWithinDetectRange = false;
            }
        }

        /// <summary>
        /// 一定時間以上範囲内にいたら制裁
        /// </summary>
        void Punish()
        {
            if (pepperyTimer >= pepperyLimit)
            {
                print("punsuka punsuka siteorimasu watakusi");
            }
        }

#if DEBUG
        void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, detect);
        }
#endif
    }
}

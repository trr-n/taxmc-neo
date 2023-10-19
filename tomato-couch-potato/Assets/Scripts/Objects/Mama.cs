using System;
using System.Linq;
using Chickenen.Pancreas;
using UnityEngine;

namespace Chickenen.Heart
{
    public class Mama : MonoBehaviour
    {
        GameObject player;
        Vector3 offset;
        GameObject[] eyes;
        Vector2[] inits;

        readonly float bump = 0.4f;

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
        }

        void Look()
        {
            // プレイヤーの方向を見続ける
            var player = this.player.transform.position + offset;
            float[] distances = { Vector2.Distance(player, eyes[0].transform.position), Vector2.Distance(player, eyes[1].transform.position) };
            Vector2[] directions = { player - eyes[0].transform.position, player - eyes[1].transform.position };
            Ray[] lines = { new(eyes[0].transform.position, directions[0]), new(eyes[1].transform.position, directions[1]) };

            foreach (var (distance, line) in distances.SelectMany(dis => lines.Select(line => (dis, line))))
            {
#if DEBUG
                // 目から怪光線
                Debug.DrawRay(line.origin, line.direction * distance);
#endif

                if (Gobject.RaycastAll2D(out var hits, line.origin, line.direction, distance))
                {
                    if (hits[0].CompareBoth(Constant.Layers.Player, Constant.Tags.Player))
                    {
                        for (int index = 0; index < eyes.Length; index++)
                        {
                            eyes[index].transform.position = inits[index] + directions[index].normalized * bump;
                        }
                    }
                }
            }
        }
    }
}

using System;
using trrne.Bag;
using UnityEngine;

namespace trrne.Body
{
    // a rolling stone gathers no moss
    public class NoMoss : Objectt
    {
        public enum Spin { Left, Right }
        [SerializeField]
        Spin dir = Spin.Left;

        /// <summary>
        /// 足
        /// </summary>
        GameObject[] feet;

        /// <summary>
        /// 回転速度
        /// </summary>
        readonly float spinSpeed = 15f;

        public bool rotatable { get; set; }

        void Start()
        {
            rotatable = true;

            feet = new GameObject[transform.childCount];
            for (int i = 0; i < transform.childCount; i++)
            {
                feet[i] = transform.GetChild(i).gameObject;
            }
        }

        protected override void Behavior()
        {
            Rotate();
            // Detect();
        }

        async void Detect()
        {
            foreach (var foot in feet)
            {
                if (Gobject.BoxCast2D(out var hit,
                    foot.transform.position, foot.GetComponent<SpriteRenderer>().bounds.size, Fixed.Layers.Player | Fixed.Layers.Creature))
                {
                    switch (hit.GetLayer())
                    {
                        case Fixed.Layers.Player:
                            if (hit.Try(out Player player))
                            {
                                await player.Die();
                            }
                            break;

                        case Fixed.Layers.Creature:
                            if (hit.Try(out Enemy enemy))
                            {
                                await enemy.Die();
                            }
                            break;
                    }
                }
            }
        }

        // async void OnTriggerEnter2D(Collider2D info)
        // {
        //     switch (info.GetLayer())
        //     {
        //         case Fixed.Layers.Player:
        //             if (info.Try(out Player player))
        //             {
        //                 await player.Die();
        //             }
        //             break;

        //         case Fixed.Layers.Creature:
        //             if (info.Try(out Enemy enemy))
        //             {
        //                 await enemy.Die();
        //             }
        //             break;

        //         default:
        //             break;
        //     }
        // }

        void Rotate()
        {
            if (!rotatable) { return; }

            // 回転
            transform.Rotate(Time.deltaTime * spinSpeed * (dir == Spin.Left ? Coordinate.z : -Coordinate.z), Space.World);
        }
    }
}

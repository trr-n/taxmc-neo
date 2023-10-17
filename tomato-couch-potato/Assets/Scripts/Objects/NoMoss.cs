using System;
using trrne.Pancreas;
using UnityEngine;

namespace trrne.Heart
{
    public class NoMoss : Object
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

        protected override void Start()
        {
            base.Start();

            feet = new GameObject[transform.childCount];
            for (int i = 0; i < transform.childCount; i++)
            {
                feet[i] = transform.GetChildObject(i);
            }

            rotatable = true;
        }

        protected override void Behavior()
        {
            Rotate();
        }

        [Obsolete]
        async void Detect()
        {
            foreach (var foot in feet)
            {
                if (Gobject.BoxCast2D(out var hit,
                    foot.transform.position, foot.GetComponent<SpriteRenderer>().bounds.size, Constant.Layers.Player | Constant.Layers.Creature))
                {
                    switch (hit.GetLayer())
                    {
                        case Constant.Layers.Player:
                            if (hit.TryGet(out Player player))
                            {
                                await player.Die();
                            }
                            break;

                        case Constant.Layers.Creature:
                            if (hit.TryGet(out Creature enemy))
                            {
                                await enemy.Die();
                            }
                            break;
                    }
                }
            }
        }

        void Rotate()
        {
            if (rotatable)
            {
                // 回転
                transform.Rotate(Time.deltaTime * spinSpeed * (dir == Spin.Left ? Vector100.Z : -Vector100.Z), Space.World);
            }
        }
    }
}

using System;
using trrne.Box;
using UnityEngine;

namespace trrne.Core
{
    public class NoMoss : Object
    {
        [SerializeField]
        [Range(-1, 1)]
        int direction;

        /// <summary>
        /// 足
        /// </summary>
        GameObject[] feet;

        [SerializeField]
        float speed = 15f;

        public float Speed => speed;
        public void SetSpeed(float speed) => this.speed = speed;

        public bool Rotatable { get; set; }

        protected override void Start()
        {
            base.Start();

            feet = new GameObject[transform.childCount];
            for (int i = 0; i < feet.Length; i++)
            {
                feet[i] = transform.GetChildObject(i);
            }

            Rotatable = true;
        }

        protected override void Behavior()
        {
            if (Rotatable || direction != 0)
            {
                // 回転
                var rotate = direction * speed * -Vector100.Z;
                transform.Rotate(rotate * Time.deltaTime, Space.World);
            }
        }

        [Obsolete]
        async void Detect()
        {
            foreach (var foot in feet)
            {
                if (Gobject.BoxCast2D(out var hit,
                    foot.transform.position,
                    foot.GetComponent<SpriteRenderer>().bounds.size,
                    Constant.Layers.Player | Constant.Layers.Creature)
                )
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
    }
}

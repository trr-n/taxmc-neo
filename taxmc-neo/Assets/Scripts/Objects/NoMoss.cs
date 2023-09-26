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

        void Start()
        {
            feet = new GameObject[transform.childCount];
            for (int i = 0; i < transform.childCount; i++)
            {
                feet[i] = transform.GetChild(i).gameObject;
            }
        }

        protected override void Behavior()
        {
            Rotate();
            Detect();
        }

        async void Detect()
        {
            foreach (var i in feet)
            {
                if (Gobject.BoxCast2D(out var hit,
                    i.transform.position, i.GetComponent<SpriteRenderer>().bounds.size, Fixed.Layers.Player | Fixed.Layers.Creature))
                {
                    switch (hit.GetLayer())
                    {
                        case Fixed.Layers.Player:
                            if (hit.Compare(Fixed.Tags.Player))
                            {
                                await hit.Get<Player>().Die();
                            }
                            break;

                        case Fixed.Layers.Creature:
                            if (hit.GetType().IsSubclassOf(typeof(Enemy)))
                            {
                                print("creature is passing.");
                                await hit.Get<Enemy>().Die();
                            }
                            break;
                    }
                }
            }
        }

        void Rotate()
        {
            // 回転
            transform.Rotate(Time.deltaTime * spinSpeed * (dir == Spin.Left ? Coordinate.z : -Coordinate.z), Space.World);
        }
    }
}

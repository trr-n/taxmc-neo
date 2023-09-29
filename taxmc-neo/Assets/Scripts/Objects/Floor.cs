using System.Collections;
using System.Collections.Generic;
using trrne.Bag;
using UnityEngine;

namespace trrne.Body
{
    public class Floor : Objectt
    {
        [SerializeField]
        [Min(0.1f)]
        float speed, range;

        public enum MoveType { Fixed, Horizontal, Vertical }
        [SerializeField]
        MoveType style = MoveType.Fixed;

        Vector3 center;

        void Start()
        {
            center = transform.position;
        }

        protected override void Behavior()
        {
            Move();
        }

        /// <summary>
        /// 移動
        /// </summary>
        void Move()
        {
            // TODO たまに固まる
            switch (style)
            {
                // 固定
                case MoveType.Fixed: break;

                // 左右
                case MoveType.Horizontal:
                    var x = Coordinate.x * range;

                    // 可動域を超えたら速度反転
                    if (transform.position.x <= (center - x).x || transform.position.x >= (center + x).x)
                    {
                        speed *= -1;
                    }
                    transform.Translate(Time.deltaTime * speed * Coordinate.x, Space.World);
                    break;

                // 上下
                case MoveType.Vertical:
                    var y = Coordinate.y * range;

                    if (transform.position.y <= (center - y).y || transform.position.y >= (center + y).y)
                    {
                        speed *= -1;
                    }
                    transform.Translate(Time.deltaTime * speed * Coordinate.y, Space.World);
                    break;
            }
        }

        void OnCollisionEnter2D(Collision2D info)
        {
            if (info.Compare(Fixed.Tags.Player))
            {
                info.transform.SetParent(transform);
            }
        }

        void OnCollisionExit2D(Collision2D info)
        {
            if (info.Compare(Fixed.Tags.Player))
            {
                info.transform.SetParent(null);
            }
        }
    }
}

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

        public enum MovingType
        {
            Fixed,      // 固定
            Horizontal, // 左右
            Vertical    // 上下
        }
        [SerializeField]
        MovingType type = MovingType.Fixed;

        Vector3 center;

        protected override void Start()
        {
            base.Start();

            center = transform.position;
        }

        protected override void Behavior()
        {
            // 移動
            switch (type)
            {
                // 固定
                case MovingType.Fixed: break;

                // 左右
                case MovingType.Horizontal:
                    var x = Coord.x * range;

                    // 可動域を超えたら速度反転
                    if (transform.position.x <= (center - x).x || transform.position.x >= (center + x).x)
                    {
                        speed *= -1;
                    }
                    transform.Translate(Time.deltaTime * speed * Coord.x, Space.World);
                    break;

                // 上下
                case MovingType.Vertical:
                    var y = Coord.y * range;

                    if (transform.position.y <= (center - y).y || transform.position.y >= (center + y).y)
                    {
                        speed *= -1;
                    }
                    transform.Translate(Time.deltaTime * speed * Coord.y, Space.World);
                    break;
            }
        }

        void OnCollisionEnter2D(Collision2D info)
        {
            if (info.CompareTag(Constant.Tags.Player) && info.transform.parent != transform)
            {
                info.transform.parent = transform;
            }
        }

        void OnCollisionExit2D(Collision2D info)
        {
            if (info.CompareTag(Constant.Tags.Player) && info.transform.parent != null)
            {
                info.transform.parent = null;
            }
        }
    }
}

using Chickenen.Pancreas;
using UnityEngine;

namespace Chickenen.Heart
{
    public class MovingFloor : Object
    {
        [SerializeField]
        float speed, range;

        public enum MovingType
        {
            Fixed,      // 固定
            Horizontal, // 左右
            Vertical    // 上下
        }

        [SerializeField]
        MovingType type = MovingType.Fixed;

        /// <summary>
        /// 上下左右移動の中心座標
        /// </summary>
        Vector3 origin;

        protected override void Start()
        {
            base.Start();
            origin = transform.position;
        }

        (float left, float right) horizon;
        (float top, float bottom) vertical;
        protected override void Behavior()
        {
            // 移動
            switch (type)
            {
                // 固定
                case MovingType.Fixed:
                default:
                    break;

                // 左右
                case MovingType.Horizontal:
                    horizon.left = (origin - Vector100.X * range).x;
                    horizon.right = (origin + Vector100.X * range).x;

                    // 可動域を超えたら速度反転
                    if (transform.position.x <= horizon.left || transform.position.x >= horizon.right)
                    {
                        speed *= -1;
                    }
                    transform.Translate(Time.deltaTime * speed * Vector100.X, Space.World);
                    break;

                // 上下
                case MovingType.Vertical:
                    vertical.top = (origin - Vector100.Y * range).y;
                    vertical.bottom = (origin + Vector100.Y * range).y;

                    if (transform.position.y <= vertical.top || transform.position.y >= vertical.bottom)
                    {
                        speed *= -1;
                    }
                    transform.Translate(Time.deltaTime * speed * Vector100.Y, Space.World);
                    break;
            }
        }
    }
}

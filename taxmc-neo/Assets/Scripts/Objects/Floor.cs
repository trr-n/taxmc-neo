using trrne.WisdomTeeth;
using UnityEngine;

namespace trrne.Body
{
    public class Floor : Objectt
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
        Vector3 center;

        Rigidbody2D rb;

        protected override void Start()
        {
            base.Start();

            rb = GetComponent<Rigidbody2D>();
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
                    var x = Vector100.x * range;

                    // 可動域を超えたら速度反転
                    if (transform.position.x <= (center - x).x || transform.position.x >= (center + x).x)
                    {
                        speed *= -1;
                    }
                    transform.Translate(Time.deltaTime * speed * Vector100.x, Space.World);
                    break;

                // 上下
                case MovingType.Vertical:
                    var y = Vector100.y * range;

                    if (transform.position.y <= (center - y).y || transform.position.y >= (center + y).y)
                    {
                        speed *= -1;
                    }
                    transform.Translate(Time.deltaTime * speed * Vector100.y, Space.World);
                    break;
            }
        }
    }
}

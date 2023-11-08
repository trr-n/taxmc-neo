using trrne.Box;
using UnityEngine;

namespace trrne.Core
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

        readonly Stopwatch pingpong = new(true);

        protected override void Start()
        {
            base.Start();
            origin = transform.position;
        }

        protected override void Behavior()
        {
            // 移動
            switch (type)
            {
                case MovingType.Fixed:
                default:
                    return;
                case MovingType.Horizontal:
                    var x = origin.x - range / 2 + Mathf.PingPong(pingpong.Sf * speed, range);
                    transform.SetPosition(x: x);
                    break;
                case MovingType.Vertical:
                    var y = origin.y - range / 2 + Mathf.PingPong(pingpong.Sf * speed, range);
                    transform.SetPosition(y: y);
                    break;
            }
        }
    }
}

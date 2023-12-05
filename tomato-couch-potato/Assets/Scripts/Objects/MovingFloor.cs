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
            Fixed,
            Horizontal,
            Vertical
        }

        [SerializeField]
        MovingType type = MovingType.Fixed;

        Vector3 center;

        readonly Stopwatch pingpong = new(true);

        protected override void Start()
        {
            base.Start();
            center = transform.position;
        }

        protected override void Behavior()
        {
            if (type == MovingType.Fixed)
            {
                return;
            }

            var pp = range / 2 + Mathf.PingPong(pingpong.Sf() * speed, range) * Time.timeScale;
            switch (type)
            {
                case MovingType.Horizontal:
                    transform.SetPosition(x: pp - center.x);
                    break;
                case MovingType.Vertical:
                    transform.SetPosition(y: pp - center.y);
                    break;
            }
        }
    }
}

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
            if (App.TimeScale(0) || type == MovingType.Fixed)
            {
                return;
            }

            float pp(float xy) => xy - range / 2 + Mathf.PingPong(pingpong.Secondf() * speed, range);
            switch (type)
            {
                case MovingType.Horizontal:
                    transform.SetPosition(x: pp(center.x));
                    break;
                case MovingType.Vertical:
                    transform.SetPosition(y: pp(center.y));
                    break;
                default:
                    throw null;
            }
        }
    }
}

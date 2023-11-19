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
        float pp = 0f;

        protected override void Start()
        {
            base.Start();
            center = transform.position;
        }

        protected override void Behavior()
        {
            if (App.TimeScale(0))
            {
                return;
            }

            if (type != MovingType.Fixed)
            {
                pp = range / 2 + Mathf.PingPong(pingpong.Secondf() * speed, range);
            }

            switch (type)
            {
                case MovingType.Fixed:
                    return;
                case MovingType.Horizontal:
                    transform.SetPosition(x: center.x - pp);
                    break;
                case MovingType.Vertical:
                    transform.SetPosition(y: center.y - pp);
                    break;
            }
        }
    }
}

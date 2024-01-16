using System.Collections;
using System.Collections.Generic;
using trrne.Box;
using UnityEngine;

namespace trrne.Core
{
    public class ObjectMover : Object
    {
        public enum WaveType
        {
            Sin,
            Cos,
            Tan
        }
        [SerializeField]
        WaveType wave;

        public enum MovingStyle
        {
            Horizontal,
            Vertical
        }
        [SerializeField]
        MovingStyle style = MovingStyle.Horizontal;

        [SerializeField]
        float movingSpeed;

        [SerializeField]
        [Min(.5f)]
        float movingRange;

        Vector2 centre;
        readonly Stopwatch pp = new(true);

        protected override void Start()
        {
            centre = transform.position;
            base.Start();
        }

        protected override void Behavior()
        {
            // float pp(float centre) => centre - (movingRange / 2) + Mathf.PingPong(this.pp.secondf * movingSpeed, movingRange);
            // switch (style)
            // {
            //     case MovingStyle.Horizontal:
            //         transform.SetPosition(x: pp(centre.x));
            //         break;
            //     case MovingStyle.Vertical:
            //         transform.SetPosition(y: pp(centre.y));
            //         break;
            // }
            float pp2(float centre) => centre - (movingRange / 2) + wave switch
            {
                WaveType.Sin => Mathf.Sin(this.pp.secondf * movingSpeed) * movingRange,
                WaveType.Cos => Mathf.Cos(this.pp.secondf * movingSpeed) * movingRange,
                WaveType.Tan => Mathf.Tan(this.pp.secondf * movingSpeed) * movingRange,
                _ => 0
            };
            switch (style)
            {
                case MovingStyle.Horizontal:
                    transform.SetPosition(x: pp2(centre.x));
                    break;
                case MovingStyle.Vertical:
                    transform.SetPosition(y: pp2(centre.y));
                    break;
            }
        }

        public void FetchStyle(MovingStyle style) => this.style = style;
    }
}

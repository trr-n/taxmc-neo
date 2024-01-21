using System;
using UnityEngine;
using trrne.Box;

namespace trrne.Core
{
    [RequireComponent(typeof(Rigidbody2D))]
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

        readonly Stopwatch pp = new(true);

        new Rigidbody2D rigidbody;

        protected override void Start()
        {
            base.Start();
            rigidbody = GetComponent<Rigidbody2D>();
            rigidbody.isKinematic = true;
            rigidbody.gravityScale = 0;

            var pos = transform.position;
            var offset = (new V2(pos.x, pos.y) - movingRange / 2).vector2();
            switch (style)
            {
                case MovingStyle.Horizontal:
                    pos.x += offset.x;
                    break;
                case MovingStyle.Vertical:
                    pos.y += offset.y;
                    break;
            }
            transform.position = pos;
        }

        protected override void Behavior()
        {
            var pp2 = wave switch
            {
                WaveType.Sin => MathF.Sin(pp.secondf * movingSpeed),
                WaveType.Cos => MathF.Cos(pp.secondf * movingSpeed),
                WaveType.Tan => MathF.Tan(pp.secondf * movingSpeed),
                _ => 0
            };

            rigidbody.velocity = movingRange * style switch
            {
                MovingStyle.Horizontal => new Vector2(pp2, 0),
                MovingStyle.Vertical => new(0, pp2),
                _ => new()
            };
        }

        public void FetchStyle(MovingStyle style) => this.style = style;
    }
}

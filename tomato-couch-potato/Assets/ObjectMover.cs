using trrne.Box;
using UnityEngine;

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

        Rigidbody2D rb;

        protected override void Start()
        {
            base.Start();
            rb = GetComponent<Rigidbody2D>();
            rb.isKinematic = true;
            rb.gravityScale = 0;
        }

        protected override void Behavior()
        {
            var pp2 = movingRange * wave switch
            {
                WaveType.Sin => Mathf.Sin(pp.secondf * movingSpeed),
                WaveType.Cos => Mathf.Cos(pp.secondf * movingSpeed),
                WaveType.Tan => Mathf.Tan(pp.secondf * movingSpeed),
                _ => 0
            };

            switch (style)
            {
                case MovingStyle.Horizontal:
                    rb.SetVelocity(x: pp2);
                    break;
                case MovingStyle.Vertical:
                    rb.SetVelocity(y: pp2);
                    break;
            }
        }

        public void FetchStyle(MovingStyle style) => this.style = style;
    }
}

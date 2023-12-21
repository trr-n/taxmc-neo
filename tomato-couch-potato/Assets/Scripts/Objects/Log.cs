using trrne.Box;
using UnityEngine;

namespace trrne.Core
{
    public class Log : Object
    {
        public enum Direction
        {
            Left,
            Right,
            Random
        }

        [SerializeField]
        Direction direction = Direction.Left;

        [SerializeField]
        float baseSpeed = 5f;

        float speed;

        protected override void Start()
        {
            SetDirection(direction);
        }

        protected override void Behavior()
        {
            Move();
        }

        public void SetDirection(Direction direction)
        {
            this.direction = direction;
            speed = direction switch
            {
                Direction.Left => -baseSpeed,
                Direction.Right => baseSpeed,
                Direction.Random or _ => Rand.Int(max: 1) switch
                {
                    0 => -baseSpeed,
                    _ => baseSpeed
                }
            };
        }

        void Move()
        {
            float speed = Time.deltaTime * this.speed;
            transform.Translate(speed * Vec.X, Space.World);

            float rotate = Numcs.Abs(this.speed * 32f);
            transform.Rotate(-speed * rotate * Vec.Z, Space.Self);
        }
    }
}

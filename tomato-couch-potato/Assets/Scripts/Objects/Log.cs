using System;
using UnityEngine;
using trrne.Box;

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
            speed = (this.direction = direction) switch
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
            transform.Translate(Time.deltaTime * speed, 0, 0, Space.World);
            transform.Rotate(0, 0, -speed * MathF.Abs(speed * 32f), Space.Self);
        }
    }
}

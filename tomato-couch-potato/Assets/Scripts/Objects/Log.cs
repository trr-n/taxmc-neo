using System;
using UnityEngine;
using trrne.Box;
using UnityEngine.UIElements;

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
            transform.Translate(x: Time.deltaTime * speed, space: Space.World);
            transform.Rotate(z: -speed * MathF.Abs(speed * 32), space: Space.Self);
        }
    }
}

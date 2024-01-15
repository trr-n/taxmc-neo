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
            transform.Translate(x: Time.deltaTime * speed);
            transform.Rotate(z: -speed * MathF.Abs(speed * 32));
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
    }
}

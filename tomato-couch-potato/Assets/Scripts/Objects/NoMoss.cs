using System;
using trrne.Box;
using UnityEngine;

namespace trrne.Core
{
    /// <summary>
    /// A rolling stone gathers no moss
    /// </summary>
    public class NoMoss : Object
    {
        public enum RotateDirection
        {
            Left = -1,
            Right = 1,
            Fixed = 0
        }
        [SerializeField]
        RotateDirection direction;

        [SerializeField]
        float speed = 15f;
        public float Speed => speed;

        public void SetSpeed(float speed) => this.speed = speed;

        public bool Rotatable { get; set; } = true;

        protected override void Behavior()
        {
            if (Rotatable || direction != RotateDirection.Fixed)
                transform.Rotate(Time.deltaTime * (float)direction * speed * Coordinate.V3Z, Space.World);
        }
    }
}

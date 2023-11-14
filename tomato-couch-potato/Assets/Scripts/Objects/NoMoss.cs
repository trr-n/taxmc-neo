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
        [SerializeField]
        [Range(-1, 1)]
        int direction;

        [SerializeField]
        float speed = 15f;
        public float Speed => speed;
        public void SetSpeed(float speed) => this.speed = speed;

        public bool Rotatable { get; set; } = true;

        protected override void Behavior()
        {
            if (Rotatable || direction != 0)
            {
                // 回転
                var rotate = -direction * speed * -Coordinate.V3Z;
                transform.Rotate(rotate * Time.deltaTime, Space.World);
            }
        }
    }
}

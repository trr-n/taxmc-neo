using System;
using trrne.Box;
using UnityEngine;

namespace trrne.Core
{
    public class NoMoss : Object
    {
        [SerializeField]
        [Range(-1, 1)]
        int direction;

        /// <summary>
        /// 足
        /// </summary>
        GameObject[] feet;

        [SerializeField]
        float speed = 15f;
        public float Speed => speed;
        public void SetSpeed(float speed) => this.speed = speed;

        public bool Rotatable { get; set; } = true;

        protected override void Start()
        {
            base.Start();

            feet = new GameObject[transform.childCount];
            feet = transform.GetChildren();
        }

        protected override void Behavior()
        {
            if (Rotatable || direction != 0)
            {
                // 回転
                var rotate = direction * speed * -Vector100.Z;
                transform.Rotate(rotate * Time.deltaTime, Space.World);
            }
        }
    }
}

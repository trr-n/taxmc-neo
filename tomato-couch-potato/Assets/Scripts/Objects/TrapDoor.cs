using trrne.Box;
using DG.Tweening;
using UnityEngine;

namespace trrne.Core
{
    public class TrapDoor : Object, IGimmick
    {
        public enum RotateDirection
        {
            Left,
            Right
        }

        [SerializeField]
        RotateDirection direct;

        Vector3 rotation;

        /// <summary>
        /// 開いているか
        /// </summary>
        public bool IsOpen { get; private set; }

        readonly float rotationSpeed = 0.5f;

        /// <summary>
        /// 回転してるか
        /// </summary>
        public bool IsRotating { get; private set; }

        protected override void Start()
        {
            base.Start();
            rotation = transform.eulerAngles;
        }

        protected override void Behavior() { }

        /// <summary>
        /// ギミックを起動する
        /// </summary>
        public void On()
        {
            var rotation = direct switch
            {
                RotateDirection.Left => this.rotation + Coordinate.V3Z * 90,
                RotateDirection.Right => this.rotation - Coordinate.V3Z * 90,
                _ => throw null
            };

            transform.DORotate(rotation, rotationSpeed)
                .OnPlay(() => IsRotating = true)
                .OnComplete(() =>
                {
                    IsRotating = false;
                    IsOpen = true;
                });
        }

        /// <summary>
        /// 停止する
        /// </summary>
        public void Off()
        {
            transform.DORotate(rotation, rotationSpeed)
                .OnPlay(() => IsRotating = true)
                .OnComplete(() =>
                {
                    IsRotating = false;
                    IsOpen = false;
                });
        }
    }
}

// https://imagingsolution.net/math/rotate-around-point/

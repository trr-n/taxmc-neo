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

        (Vector3 rotation, Vector2 position) initial;

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
            initial.position = transform.position;
            initial.rotation = transform.eulerAngles;
        }

        protected override void Behavior() { }

        public void Active()
        {
            IsOpen = true;
            Vector3 rotinfo = new();
            switch (direct)
            {
                case RotateDirection.Left:
                    rotinfo = initial.rotation + Vector100.Z * 90;
                    break;
                case RotateDirection.Right:
                    rotinfo = initial.rotation - Vector100.Z * 90;
                    break;
            }

            transform.DORotate(rotinfo, rotationSpeed)
                .OnPlay(() => IsRotating = true)
                .OnComplete(() => IsRotating = false);
        }

        public void Inactive()
        {
            IsOpen = false;
            transform.DORotate(initial.rotation, rotationSpeed)
                .OnPlay(() => IsRotating = true)
                .OnComplete(() => IsRotating = false);
        }
    }
}

// https://imagingsolution.net/math/rotate-around-point/

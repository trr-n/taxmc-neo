using trrne.Pancreas;
using DG.Tweening;
using UnityEngine;

namespace trrne.Heart
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

        bool isOpen = false;
        /// <summary>
        /// 開いているか
        /// </summary>
        public bool IsOpen => isOpen;

        readonly float rotationSpeed = 0.5f;

        bool isRotating = false;
        /// <summary>
        /// 回転してるか
        /// </summary>
        public bool IsRotating => isRotating;

        protected override void Start()
        {
            base.Start();

            initial.position = transform.position;
            initial.rotation = transform.eulerAngles;
        }

        protected override void Behavior() { }

        public void Active()
        {
            isOpen = true;
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
                .OnPlay(() => isRotating = true)
                .OnComplete(() => isRotating = false);
        }

        public void Inactive()
        {
            isOpen = false;
            switch (direct)
            {
                case RotateDirection.Left:
                case RotateDirection.Right:
                    transform.DORotate(initial.rotation, rotationSpeed)
                        .OnPlay(() => isRotating = true)
                        .OnComplete(() => isRotating = false);
                    break;
            }
        }
    }
}

// https://imagingsolution.net/math/rotate-around-point/

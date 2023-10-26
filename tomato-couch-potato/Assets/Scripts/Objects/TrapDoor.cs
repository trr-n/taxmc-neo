using trrne.Pancreas;
using DG.Tweening;
using UnityEngine;

namespace trrne.Heart
{
    public class TrapDoor : Object, IUsable
    {
        public enum RotateDirection
        {
            Left, Right
        }

        [SerializeField]
        RotateDirection rdir;

        [SerializeField]
        bool left;

        (Vector3 rotation, Vector2 position) initial;

        bool isOpen = false;
        public bool IsOpen => isOpen;

        readonly float rotationSpeed = 0.5f;

        bool isRotating = false;
        public bool IsRotating => isRotating;

        Vector2 C => Size;

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
            switch (rdir)
            {
                case RotateDirection.Left:
                    rotinfo = initial.rotation - Vector100.Z * 90;
                    break;

                case RotateDirection.Right:
                    rotinfo = initial.rotation + Vector100.Z * 90;
                    break;
            }

            transform.DORotate(rotinfo, rotationSpeed)
                .OnPlay(() => isRotating = true)
                .OnComplete(() => isRotating = false);
        }

        public void Inactive()
        {
            isOpen = false;
            switch (rdir)
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

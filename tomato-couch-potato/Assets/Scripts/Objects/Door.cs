using UnityEngine;

namespace Chickenen.Heart
{
    public class Door : Object, IUsable
    {
        public enum Rotates
        {
            Left, Right
        }

        [SerializeField]
        Rotates rotates;

        Quaternion defaultRotation;

        protected override void Start()
        {
            base.Start();
            defaultRotation = transform.rotation;
        }

        protected override void Behavior() { }

        public void Active()
        {
            print("door is active!");
            // switch (rotates)
            // {
            //     case Rotates.Left:
            //         break;
            //     case Rotates.Right:
            //         break;
            //     default:
            //         break;
            // }
        }

        public void Inactive()
        {
            print("door is inactive!");
            // switch (rotates)
            // {
            //     case Rotates.Left:
            //         break;
            //     case Rotates.Right:
            //         break;
            //     default:
            //         break;
            // }
        }
    }
}

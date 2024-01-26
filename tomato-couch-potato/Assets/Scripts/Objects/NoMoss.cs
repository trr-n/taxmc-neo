using System.Collections;
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
            Left = 1,
            Right = -1,
            Fixed = 0
        }

        [SerializeField]
        RotateDirection direction;

        [SerializeField]
        bool isFixed = true;

        // [DisableVariable("isFixed")]
        [SerializeField]
        float rotSpeed = 64;

        public bool Rotatable { get; set; } = true;

        readonly LotteryPair<float> speeds = new((32, 0.8f), (48, 1), (64, 2), (128, 1));
        float speed;

        protected override void Start()
        {
            base.Start();

            if (!isFixed)
            {
                speed = speeds.Weighted();
                StartCoroutine(nameof(SpeedUpdater));
            }
            else
            {
                speed = rotSpeed;
            }
        }

        protected override void Behavior()
        {
            if (Rotatable || direction != RotateDirection.Fixed)
            {
                transform.Rotate(z: Time.deltaTime * (float)direction * speed, space: Space.Self);
            }
        }

        IEnumerator SpeedUpdater()
        {
            int index = 0;
            while (this != null)
            {
                yield return new WaitForSeconds(Rand.Int(2, 5));
                // speed = speeds.Weighted();
                speed = speeds.Subject(index);
                index = index + 1 >= speeds.Length() ? 0 : ++index;
            }
        }
    }
}

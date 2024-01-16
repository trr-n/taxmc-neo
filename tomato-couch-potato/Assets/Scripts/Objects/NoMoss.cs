using System.Collections;
using Cysharp.Threading.Tasks;
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

        // [SerializeField]
        // float speed = 15f;
        // public float Speed => speed;
        // public void SetSpeed(float speed) => this.speed = speed;


        public bool Rotatable { get; set; } = true;

        readonly LotteryPair<int> speeds = new((32, 1), (48, 1), (64, 2), (128, 1));
        int speed;

        protected override void Start()
        {
            base.Start();
            speed = speeds.Weighted();
            StartCoroutine(nameof(SpeedUpdater));
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
            while (true)
            {
                yield return new WaitForSeconds(Rand.Int(2, 5));
                speed = speeds.Weighted();
            }
        }
    }
}

using Chickenen.Pancreas;
using UnityEngine;

namespace Chickenen.Heart
{
    [RequireComponent(typeof(SpriteRenderer))]
    public abstract class Object : MonoBehaviour
    {
        [SerializeField]
        protected GameObject[] effects;

        [SerializeField]
        protected Sprite[] sprites;

        /// <summary>
        /// アニメーションの間隔
        /// </summary>
        protected float interval = 0.02f;

        /// <summary>
        /// アニメーションさせるか
        /// </summary>
        protected bool Animate { get; set; } = false;
        protected SpriteRenderer sr;
        protected Vector2 Size => sr.bounds.size;

        protected virtual void Start()
        {
            sr = GetComponent<SpriteRenderer>();
        }

        protected virtual void Update()
        {
            Behavior();
            Animation();
        }

        /// <summary>
        /// 振舞(Update)
        /// </summary>
        protected abstract void Behavior();

        readonly Anima anima = new();
        readonly Runner runner = new();
        void Animation()
        {
            if (!Animate || sprites.Length <= 0)
            {
                return;
            }

            switch (sprites.Length)
            {
                case 1:
                    runner.RunOnce(() => sr.sprite = sprites[0]);
                    break;

                case 2:
                default:
                    anima.Sprite(Animate, sr, interval, sprites);
                    break;
            }
        }
    }
}

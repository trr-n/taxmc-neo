using trrne.Box;
using UnityEngine;

namespace trrne.Core
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
        protected float animationInterval = 0.02f;

        /// <summary>
        /// アニメーションさせるか
        /// </summary>
        protected bool isAnimate { get; set; } = false;
        protected SpriteRenderer sr;
        protected Vector2 size => sr.bounds.size;

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
        readonly Runner set = new();
        void Animation()
        {
            if (!isAnimate)
            {
                return;
            }
            switch (sprites.Length)
            {
                case 0:
                    return;
                case 1:
                    set.RunOnce(() => sr.sprite = sprites[0]);
                    break;
                default:
                    anima.Sprite(isAnimate, sr, animationInterval, sprites);
                    break;
            }
        }
    }
}

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
        protected float interval = 0.02f;

        /// <summary>
        /// アニメーションさせるか
        /// </summary>
        protected bool Animate { get; set; }
        protected SpriteRenderer sr;
        protected Vector2 Size => sr.bounds.size;

        protected virtual void Start()
        {
            Animate = false;
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
            if (Animate)
            {
                if (sprites.Length == 1)
                {
                    set.RunOnce(() => sr.sprite = sprites[0]);
                }
                else if (sprites.Length >= 2)
                {
                    anima.Sprite(Animate, sr, interval, sprites);
                }
            }
        }
    }
}

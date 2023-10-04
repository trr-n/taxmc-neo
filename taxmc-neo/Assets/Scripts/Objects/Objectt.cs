using trrne.Bag;
using UnityEngine;

namespace trrne.Body
{
    [RequireComponent(typeof(SpriteRenderer))]
    public abstract class Objectt : MonoBehaviour
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
        protected bool animatable { get; set; }

        protected SpriteRenderer sr;
        // protected Vector2 size => sr.bounds.size;
        protected Vector2 here => transform.position;

        protected virtual void Start()
        {
            sr = GetComponent<SpriteRenderer>();
        }

        void Update()
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
            if (!animatable || sprites.Length <= 0)
            {
                return;
            }

            switch (sprites.Length)
            {
                case 1:
                    set.RunOnce(() => sr.sprite = sprites[0]);
                    break;

                case 2:
                default:
                    anima.Sprite(animatable, sr, interval, sprites);
                    break;
            }
        }
    }
}

using trrne.Teeth;
using UnityEngine;

namespace trrne.Body
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
        protected bool animatable { get; set; }
        protected SpriteRenderer sr;
        protected Vector2 size => sr.bounds.size;

        /// <summary>
        /// base.Start();<br/>
        /// ↑を忘れずに
        /// </summary>
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

        new readonly (Anima anima, Runner runner) animation = (new(), new());
        void Animation()
        {
            if (!animatable || sprites.Length <= 0)
            {
                return;
            }

            switch (sprites.Length)
            {
                case 1:
                    animation.runner.RunOnce(() => sr.sprite = sprites[0]);
                    break;

                case 2:
                default:
                    animation.anima.Sprite(animatable, sr, interval, sprites);
                    break;
            }
        }
    }
}

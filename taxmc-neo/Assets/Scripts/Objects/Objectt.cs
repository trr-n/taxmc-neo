using trrne.Appendix;
using UnityEngine;

namespace trrne.Body
{
    [RequireComponent(typeof(SpriteRenderer))]
    public abstract class Objectt : MonoBehaviour
    {
        public Sprite[] sprites;
        protected float interval = 0.02f;

        protected SpriteRenderer sr;

        protected Vector2 size => sr.bounds.size;

        void Start()
        {
            sr = GetComponent<SpriteRenderer>();
        }

        void Update()
        {
            Behavior();
            Animation();
        }

        /// <summary>
        /// 振舞
        /// </summary>
        protected abstract void Behavior();

        readonly Runner picset = new();
        void Animation()
        {
            switch (sprites.Length)
            {
                case 0:
                    // throw new Karappoyanke();
                    break;

                case 1:
                    picset.RunOnce(() => sr.sprite = sprites[0]);
                    break;

                case 2:
                default:
                    // Anima.Pic(sr, interval, sprites);
                    break;
            }
        }
    }
}

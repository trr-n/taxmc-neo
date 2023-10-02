using trrne.Bag;
using UnityEngine;

namespace trrne.Body
{
    [RequireComponent(typeof(SpriteRenderer))]
    public abstract class Objectt : MonoBehaviour
    {
        public Sprite[] sprites;
        protected float interval = 0.02f;
        protected bool animatable { get; set; }

        protected SpriteRenderer sr;
        protected Vector2 size => sr.bounds.size;
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
        /// 振舞
        /// </summary>
        protected abstract void Behavior();

        readonly Anima anima = new();
        readonly Runner picset = new();
        void Animation()
        {
            if (!animatable) { return; }

            switch (sprites.Length)
            {
                case 0: break;

                case 1:
                    picset.RunOnce(() => sr.sprite = sprites[0]);
                    break;

                case 2:
                default:
                    anima.Sprite(true, sr, interval, sprites);
                    break;
            }
        }
    }
}

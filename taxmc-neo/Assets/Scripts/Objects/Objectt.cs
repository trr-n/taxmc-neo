using trrne.utils;
using UnityEngine;

namespace trrne.Game
{
    [RequireComponent(typeof(SpriteRenderer))]
    public abstract class Objectt : MonoBehaviour
    {
        public Sprite[] pics;
        protected float interval = 0.02f;

        protected SpriteRenderer sr;

        protected Vector2 size => sr.bounds.size;

        void Start()
        {
            sr = GetComponent<SpriteRenderer>();
        }

        void Update()
        {
            Behaviour();
            Animation();
        }

        /// <summary>
        /// 振舞
        /// </summary>
        protected abstract void Behaviour();

        Runner picset = new();
        void Animation()
        {
            switch (pics.Length)
            {
                case 0:
                    // throw new Karappoyanke();
                    break;

                case 1:
                    picset.RunOnce(() => sr.sprite = pics[0]);
                    break;

                case 2:
                default:
                    Anima.Pic(sr, interval, pics);
                    break;
            }
        }
    }
}

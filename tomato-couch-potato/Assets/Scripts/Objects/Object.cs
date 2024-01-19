using trrne.Box;
using UnityEngine;

namespace trrne.Core
{
    [RequireComponent(typeof(SpriteRenderer))]
    // [RequireComponent(typeof(AudioSource))]
    public abstract class Object : MonoBehaviour
    {
        [SerializeField]
        protected GameObject[] effects;

        [SerializeField]
        protected Sprite[] sprites;

        [SerializeField]
        protected AudioClip[] ses;

        /// <summary>
        /// アニメーションの間隔
        /// </summary>
        protected float animationInterval = 0.02f;

        /// <summary>
        /// アニメーションさせるか
        /// </summary>
        protected bool isAnimate { get; set; } = false;
        protected SpriteRenderer sr;

        AudioSource speaker;

        protected virtual void Start()
        {
            sr = GetComponent<SpriteRenderer>();
            speaker = GetComponent<AudioSource>();
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

        readonly Runner set = new();
        void Animation()
        {
            if (!isAnimate && sprites.Length <= 0)
            {
                return;
            }

            switch (sprites.Length)
            {
                case 1:
                    set.RunOnce(() => sr.sprite = sprites[0]);
                    break;
                default:
                    sr.Animation(isAnimate, animationInterval, sprites);
                    break;
            }
        }

        protected void PlayOneShot() => speaker.PlayOneShot(ses.Choice());
        protected void PlayOneShot(AudioClip clip) => speaker.PlayOneShot(clip);
    }
}

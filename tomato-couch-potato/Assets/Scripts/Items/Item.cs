using trrne.Box;
using UnityEngine;

namespace trrne.Core
{
    public abstract class Item : MonoBehaviour
    {
        [SerializeField]
        protected GameObject effect;

        [Tooltip("アニメーション用の画像")]
        [SerializeField]
        protected Sprite[] sprites;

        protected SpriteRenderer SR { get; private set; }

        protected Vector2 Size => SR.bounds.size;

        /// <summary>
        /// アニメーションのインターバル<br/>初期値: 0.02
        /// </summary>
        protected float interval = 0.02f;

        /// <summary>
        /// アニメーションするか
        /// </summary>
        protected bool animatable = true;

        (int index, Stopwatch sw) anim = (0, new(true));
        readonly Anima anima = new();

        protected virtual void Start()
        {
            SR = GetComponent<SpriteRenderer>();
        }

        void Update()
        {
            Receive();
            Animation();

            anima.Sprite(animatable, SR, Time.deltaTime, sprites);
        }

        void Animation()
        {
            if (!animatable)
            {
                return;
            }

            if (anim.sw.Sf() >= interval)
            {
                anim.index = anim.index >= sprites.Length - 1 ? 0 : anim.index += 1;
                SR.sprite = sprites[anim.index];
                anim.sw.Restart();
            }
        }

        /// <summary>
        /// プレイヤーがアイテムにふれたら
        /// </summary>
        protected abstract void Receive();

        /// <summary>
        /// 画像を設定
        /// </summary>
        protected void SetSprite(Sprite _sprite) => SR.sprite = _sprite;
    }
}
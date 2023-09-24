using System.Globalization;
using System;
using trrne.Appendix;
using UnityEngine;

namespace trrne.Body
{
    public abstract class Item : MonoBehaviour
    {
        [Tooltip("アニメーション用の画像")]
        public Sprite[] itemSprites;

        SpriteRenderer srenderer;
        protected SpriteRenderer sr => srenderer;

        /// <summary>
        /// アニメーションのインターバル<br/>初期値: 0.02
        /// </summary>
        public float interval = 0.02f;

        /// <summary>
        /// アニメーションするか
        /// </summary>
        protected bool animatable = true;

        (int index, Stopwatch sw) anim = (0, new(true));
        readonly Anima anima = new();

        void Start()
        {
            srenderer = GetComponent<SpriteRenderer>();
        }

        void Update()
        {
            Receive();
            // Animation();

            anima.Sprite(animatable, sr, Time.deltaTime, itemSprites);
        }

        void Animation()
        {
            if (!animatable) { return; }

            if (anim.sw.sf >= interval)
            {
                anim.index = anim.index >= itemSprites.Length - 1 ? 0 : anim.index += 1;
                sr.sprite = itemSprites[anim.index];

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
        protected void SetSprite(Sprite sprite) => srenderer.sprite = sprite;
    }
}
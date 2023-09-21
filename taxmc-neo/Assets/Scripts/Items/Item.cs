using trrne.utils;
using UnityEngine;

namespace trrne.Game
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
        protected float interval = 0.02f;

        /// <summary>
        /// アニメーションするか
        /// </summary>
        protected bool animatable = true;

        void Start()
        {
            srenderer = GetComponent<SpriteRenderer>();
        }

        void Update()
        {
            if (animatable) { Anima.Pic(srenderer, interval, itemSprites); }

            Receive();
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
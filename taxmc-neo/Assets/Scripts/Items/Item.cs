using System.Globalization;
using System;
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
        public float interval = 0.02f;

        /// <summary>
        /// アニメーションするか
        /// </summary>
        protected bool animatable = true;

        (int index, Stopwatch sw) anima = (0, new());

        void Start()
        {
            srenderer = GetComponent<SpriteRenderer>();

            anima.sw.Start();
        }

        void Update()
        {
            Receive();
            Animation();
        }

        void Animation()
        {
            if (!animatable) { return; }

            if (anima.sw.sf >= interval)
            {
                anima.index = anima.index >= itemSprites.Length - 1 ? 0 : anima.index += 1;
                sr.sprite = itemSprites[anima.index];

                anima.sw.Restart();
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
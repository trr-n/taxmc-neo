using trrne.Utils;
using UnityEngine;

namespace trrne.Game
{
    public abstract class Item : MonoBehaviour
    {
        [Tooltip("アニメーション用の画像")]
        public Sprite[] itemSprites;

        SpriteRenderer _sr;
        protected SpriteRenderer sr => _sr;

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
            _sr = GetComponent<SpriteRenderer>();
        }

        void Update()
        {
            if (animatable)
            {
                Anima.Pic(_sr, interval, itemSprites);
            }

            Receive();
        }

        /// <summary>
        /// プレイヤーがアイテムにふれたら
        /// </summary>
        protected abstract void Receive();

        /// <summary>
        /// 画像を設定
        /// </summary>
        protected void SetSprite(Sprite sprite) => _sr.sprite = sprite;
    }
}
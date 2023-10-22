using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Chickenen.Heart
{
    public abstract class Creature : MonoBehaviour
    {
        [SerializeField]
        protected GameObject diefx;

        public bool Enable { get; set; }

        protected SpriteRenderer sr;

        /// <summary>
        /// 移動
        /// </summary>
        protected abstract void Movement();

        /// <summary>
        /// 振舞 / プレイヤー検知など
        /// </summary>
        protected abstract void Behavior();

        /// <summary>
        /// 死
        /// </summary>
        public abstract UniTask Die();

        protected virtual void Start()
        {
            sr = GetComponent<SpriteRenderer>();
        }

        void Update()
        {
            if (!Enable)
            {
                return;
            }

            Movement();
            Behavior();
        }
    }
}

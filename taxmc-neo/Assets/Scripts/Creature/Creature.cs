using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace trrne.Body
{
    public abstract class Creature : MonoBehaviour
    {
        public GameObject diefx;

        public bool enable;

        /// <summary>
        /// 移動
        /// </summary>
        protected abstract void Move();

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
        }

        void Update()
        {
            if (!enable) { return; }

            Move();
            Behavior();
        }
    }
}

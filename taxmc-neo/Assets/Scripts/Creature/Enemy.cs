using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace trrne.Game
{
    public abstract class Enemy : MonoBehaviour
    {
        public GameObject dieFX;

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
        protected abstract void Die();

        protected void Update()
        {
            if (!enable) { return; }

            Move();
            Behavior();
        }
    }
}

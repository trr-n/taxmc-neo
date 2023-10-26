using System;
using trrne.Pancreas;
using UnityEngine;

namespace trrne.Heart
{
    public class Carrot : Object
    {
        [SerializeField]
        [Tooltip("limit回踏んだらアウト")]
        int limitSteps = 2;

        HoleFlag flag;
        new BoxCollider2D collider;

        bool isBreaking = false;
        /// <summary>
        /// 耐久値ぜろだったらtrue
        /// </summary>
        public bool IsBreaking => isBreaking;

        public float Ratio => (float)flag.Count / limitSteps;

        protected override void Start()
        {
            base.Start();

            flag = transform.GetFromChild<HoleFlag>();
            flag.Count = 0;

            sr.sprite = sprites[0];

            collider = GetComponent<BoxCollider2D>();
        }

        protected override void Behavior()
        {
            if (!isBreaking && flag.Count >= limitSteps)
            {
                // ぽわっみたいなエフェクト
                effects.TryGenerate(transform.position);

                isBreaking = true;
                sr.enabled = false;
                collider.enabled = false;
            }

            sr.sprite = sprites[Ratio < 0.5f ? 0 : 1];
        }

        public void Mending()
        {
            isBreaking = false;
            flag.Count = 0;

            sr.enabled = true;
            collider.enabled = true;
        }
    }
}

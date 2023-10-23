using System;
using Chickenen.Pancreas;
using UnityEngine;

namespace Chickenen.Heart
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

        public float Ratio => (float)flag.count / limitSteps;

        protected override void Start()
        {
            base.Start();

            flag = transform.GetFromChild<HoleFlag>();
            flag.count = 0;

            sr.sprite = sprites[0];

            collider = GetComponent<BoxCollider2D>();
        }

        protected override void Behavior()
        {
            if (!isBreaking && flag.count >= limitSteps)
            {
                // ぽわっみたいなエフェクト
                effects.TryGenerate(transform.position);

                isBreaking = true;
                sr.enabled = false;
                collider.enabled = false;
            }

            sr.sprite = Ratio < 0.5f ? sprites[0] : sprites[1];
        }

        public void Mending()
        {
            print("now mending...");
            isBreaking = false;
            flag.count = 0;

            sr.enabled = true;
            collider.enabled = true;
        }
    }
}

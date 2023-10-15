using System;
using trrne.WisdomTeeth;
using UnityEngine;

namespace trrne.Body
{
    public class Hole : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("limit回踏んだらアウト")]
        int limitSteps = 2;

        [SerializeField]
        GameObject destroyEffect;

        [SerializeField]
        Sprite[] sprites;

        HoleFlag flag;

        SpriteRenderer sr;
        new BoxCollider2D collider;

        bool breaking = false;
        /// <summary>
        /// 耐久値ぜろだったらtrue
        /// </summary>
        public bool isBreaking => breaking;

        public float ratio => (float)flag.count / limitSteps;

        void Start()
        {
            flag = transform.GetFromChild<HoleFlag>();
            flag.count = 0;

            sr = GetComponent<SpriteRenderer>();
            sr.sprite = sprites[0];

            collider = GetComponent<BoxCollider2D>();
        }

        void Update()
        {
            if (!breaking && flag.count >= limitSteps)
            {
                // ぽわっ
                destroyEffect.TryGenerate(transform.position);

                breaking = true;
                sr.enabled = false;
                collider.enabled = false;
            }

            sr.sprite = ratio < 0.5f ? sprites[0] : sprites[1];
        }

        public void Mending()
        {
            print("now mending...");
            breaking = false;
            flag.count = 0;

            sr.enabled = true;
            collider.enabled = true;
        }
    }
}

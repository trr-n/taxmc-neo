using UnityEngine;
using Self.Utils;
using System.Collections;

namespace Self.Game
{
    public class Blower : MonoBehaviour
    {
        [SerializeField]
        Sprite[] blowerSprites;

        [SerializeField]
        Sprite[] flowSprites;

        /// <summary>
        /// アニメーションのインターバル
        /// </summary>
        const float AnimationInterval = 0.02f;

        GameObject flowObj;
        SpriteRenderer flowSr;
        SpriteRenderer blowerSr;

        /// <summary>
        /// 風量,圧
        /// </summary>
        readonly float pressure = 500f;

        void Start()
        {
            flowObj = transform.GetChildGameObject();
            flowSr = flowObj.GetComponent<SpriteRenderer>();
            blowerSr = GetComponent<SpriteRenderer>();

            StartCoroutine(Animation2(blowerSr, blowerSprites));
        }

        void Update()
        {
            Blowing();
            Animation();
        }

        void Animation()
        {
            // blowerSr.Pic(AnimationInterval, blowerSprites);
            // flowSr.Pic(Interval, flowSprites);
        }

        IEnumerator Animation2(SpriteRenderer sr, Sprite[] sprites)
        {
            int index = 0;

            while (true)
            {
                sr.sprite = sprites[index];

                index++;
                if (index >= sprites.Length)
                    index = 0;

                // yield return new WaitForSeconds(AnimationInterval);
                yield return new WaitForSeconds(Time.deltaTime);
            }
        }

        (Vector2 centre, Vector2 size) cube;
        void Blowing()
        {
            cube.centre = flowObj.transform.position;
            cube.size = flowSr.bounds.size;

            if (Gobject.BoxCast2D(out var hit, cube.centre, cube.size, Constant.Layers.Player))
            {
                // hit.Get<SpriteRenderer>().color = Color.HSVToRGB(Time.time % 1, 1, 1);
                hit.Get<Rigidbody2D>().AddForce(pressure * transform.up);
            }
        }

        // void OnDrawGizmos()
        // {
        //     Gizmos.color = Color.HSVToRGB(Time.time % 1, 1, 1);
        //     Gizmos.DrawWireCube(cube.centre, cube.size);
        // }
    }
}

using UnityEngine;
using trrne.Bag;
using System.Collections;

namespace trrne.Body
{
    public class Blower : Objectt
    {
        [SerializeField]
        Sprite[] blowerSprites, flowSprites;

        GameObject flowObj;
        SpriteRenderer flowSr;
        SpriteRenderer blowerSr;

        /// <summary>
        /// 風量,圧
        /// </summary>
        readonly float pressure = 500f;

        void Start()
        {
            flowObj = transform.GetChildGobject();
            flowSr = flowObj.GetComponent<SpriteRenderer>();
            blowerSr = GetComponent<SpriteRenderer>();

            StartCoroutine(Animation2(blowerSr, blowerSprites));
        }

        protected override void Behavior()
        {
            Blowing();
        }

        IEnumerator Animation2(SpriteRenderer sr, Sprite[] sprites)
        {
            int index = 0;

            while (true)
            {
                sr.sprite = sprites[index];

                index++;
                if (index >= sprites.Length) { index = 0; }

                // yield return new WaitForSeconds(AnimationInterval);
                yield return new WaitForSeconds(Time.deltaTime);
            }
        }

        void Blowing()
        {
            if (Gobject.BoxCast2D(out var hit, flowObj.Position(), flowSr.bounds.size, Fixed.Layers.Player))
            {
                // hit.Get<SpriteRenderer>().color = Color.HSVToRGB(Time.time % 1, 1, 1);
                hit.Get<Rigidbody2D>().AddForce(pressure * transform.up);
            }
        }
    }
}

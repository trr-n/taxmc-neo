using System.Collections;
using UnityEngine;

namespace trrne.Core
{
    public class FlashFloor : Object
    {
        [SerializeField]
        [Tooltip("yを0にすると両方にxの値が入る")]
        Vector2 cooltimes = new(0, default);

        BoxCollider2D hitbox;

        protected override void Start()
        {
            base.Start();
            hitbox = GetComponent<BoxCollider2D>();
            StartCoroutine(Flash());
        }

        /// <summary>
        /// cooltime.x秒アクティブ、cooltime.y秒非アクティブの繰り返し
        /// </summary>
        IEnumerator Flash()
        {
            while (true)
            {
                sr.enabled = hitbox.enabled = true;
                yield return new WaitForSeconds(cooltimes.x != 0 ? cooltimes.x : cooltimes.y);

                sr.enabled = hitbox.enabled = false;
                yield return new WaitForSeconds(cooltimes.y);
            }
        }

        protected override void Behavior() { }
    }
}

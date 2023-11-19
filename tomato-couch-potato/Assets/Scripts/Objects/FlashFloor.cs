using System.Collections;
using UnityEngine;

namespace trrne.Core
{
    public class FlashFloor : Object
    {
        [SerializeField]
        [Tooltip("yを0にすると両方にxの値が入る")]
        Vector2 cooltimes = new(0, default);

        new BoxCollider2D collider;

        protected override void Start()
        {
            base.Start();
            collider = GetComponent<BoxCollider2D>();
            StartCoroutine(Flash());
        }

        /// <summary>
        /// cooltime.x秒アクティブ、cooltime.y秒非アクティブの繰り返し
        /// </summary>
        IEnumerator Flash()
        {
            while (true)
            {
                sr.enabled = true;
                collider.enabled = true;
                yield return new WaitForSeconds(cooltimes.x != 0 ? cooltimes.x : cooltimes.y);

                sr.enabled = false;
                collider.enabled = false;
                yield return new WaitForSeconds(cooltimes.y);
            }
        }

        protected override void Behavior() { }
    }
}

using System.Collections;
using trrne.Box;
using UnityEngine;

namespace trrne.Core
{
    public class FlashFloor : Object
    {
        [SerializeField]
        // [Tooltip("yを0にすると両方にxの値が入る")]
        [Tooltip("inactiveを0にすると両方にactiveの値が入る")]
        // Vector2 cooltimes = new(0, default);
        float active = 2f, inactive = 0f;

        BoxCollider2D hitbox;

        GameObject[] children;

        protected override void Start()
        {
            base.Start();
            children = transform.GetChildrenGameObject();
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
                // sr.enabled = hitbox.enabled = true;
                children.ForEach(child => child.SetActive(sr.enabled = hitbox.enabled = true));
                yield return new WaitForSeconds(active != 0 ? active : inactive);

                // sr.enabled = hitbox.enabled = false;
                children.ForEach(child => child.SetActive(sr.enabled = hitbox.enabled = false));
                yield return new WaitForSeconds(inactive);
            }
        }

        protected override void Behavior() { }
    }
}

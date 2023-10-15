using System.Collections;
using UnityEngine;

namespace trrne.Body
{
    public class FlashFloor : Objectt
    {
        [SerializeField]
        [Tooltip("inactiveを0にすると両方にactiveの値が入る")]
        float active = 0, inactive;

        new BoxCollider2D collider;

        protected override void Start()
        {
            base.Start();
            collider = GetComponent<BoxCollider2D>();
            StartCoroutine(EEE());
        }

        /// <summary>
        /// active秒アクティブ、inactive秒非アクティブの繰り返し
        /// </summary>
        IEnumerator EEE()
        {
            while (true)
            {
                sr.enabled = true;
                collider.enabled = true;

                yield return new WaitForSeconds(active != 0 ? active : inactive);

                sr.enabled = false;
                collider.enabled = false;

                yield return new WaitForSeconds(inactive);
            }
        }

        protected override void Behavior() { }
    }
}

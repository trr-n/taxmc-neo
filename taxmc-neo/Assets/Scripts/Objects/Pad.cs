using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace trrne.Game
{
    public class Pad : MonoBehaviour
    {
        [SerializeField]
        Rigidbody2D[] debug4;

        readonly float detectRange = 2;

        void Update()
        {
            debug4 = Closers;
        }

        /// <summary>
        /// 近くにいるえんてぃてぃを取得
        /// </summary>
        Rigidbody2D[] Closers
        {
            get
            {
                var allRbs = FindObjectsByType<Rigidbody2D>(FindObjectsSortMode.None);

                var closers =
                    from c in allRbs
                    where Vector2.Distance(transform.position, c.gameObject.transform.position) < detectRange
                    select c;

                return closers.ToArray();
            }
        }
    }
}
using System;
using System.Diagnostics.Contracts;
using trrne.Box;
using UnityEngine;

namespace trrne.Core
{
    public class Mama : MonoBehaviour
    {
        [SerializeField]
        GameObject[] fires;

        [SerializeField]
        float fireRapidRate = 2f;
        float fireRapidTimer = 0;

        [SerializeField]
        float lineWidth = .5f;

        [SerializeField]
        float playerDetectRange = 13f;

        GameObject player;
        Vector3 ofs;

        GameObject[] eyes;
        Vector3[] inits;
        // LineRenderer[] lines;
        Vector3[] directions;

        /// <summary>
        /// 黒目の可動域
        /// </summary>
        const float EyeBump = 0.25f;

        /// <summary>
        /// 範囲内にいるか
        /// </summary>
        [Pure] public bool IsPlayerOnDetectRange { get; private set; }

        void Start()
        {
            player = Gobject.GetWithTag(Constant.Tags.Player);
            ofs = new(0, player.GetComponent<BoxCollider2D>().Size().y / 2);

            eyes = transform.GetChildrenGameObject();
            inits = new Vector3[eyes.Length];
            // inits = new[] { eyes[0].transform.position, eyes[1].transform.position };
            for (int i = 0; i < inits.Length; i++)
                inits[i] = eyes[i].transform.position;
        }

        void Update()
        {
            Vector3 ofs = player.transform.position + this.ofs;
            directions = new[] { ofs - eyes[0].transform.position, ofs - eyes[1].transform.position };
            float distance = Maths.Average(Vector2.Distance(ofs, eyes[0].transform.position), Vector2.Distance(ofs, eyes[1].transform.position));

            // player within range
            if (!(IsPlayerOnDetectRange = playerDetectRange >= distance))
                return;

            Vector2 ave = new(Maths.Average(eyes[0].transform.position.x, eyes[1].transform.position.x),
                Maths.Average(eyes[0].transform.position.y, eyes[1].transform.position.y));
            Ray infrared = new(ave, player.transform.position.ToVec2() - ave);

            // and there is not object between player and me
            if (!Gobject.TryGetRaycast<Player>(infrared, distance))
                return;

            for (int i = 0; i < eyes.Length; i++)
                eyes[i].transform.position = inits[i] + directions[i].normalized * EyeBump;

            // the fires generate if timer value is upper than rapid rate 
            if ((fireRapidTimer += Time.deltaTime) >= fireRapidRate)
            {
                fires.TryInstantiate(transform.position);
                fireRapidTimer = 0;
            }
        }

#if UNITY_EDITOR
        void OnDrawGizmos()
        {
            if (eyes == null || eyes.Length <= 0)
                return;

            Gizmos.color = Surface.Gaming;
            Vector2 ave = new(Maths.Average(eyes[0].transform.position.x, eyes[1].transform.position.x),
                Maths.Average(eyes[0].transform.position.y, eyes[1].transform.position.y));
            Gizmos.DrawWireSphere(ave, playerDetectRange / 2);
        }
#endif
    }
}
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
        LineRenderer[] lines;

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
            player = Gobject.GetGameObjectWithTag(Constant.Tags.Player);
            ofs = new(0, player.GetComponent<BoxCollider2D>().Size().y / 2);

            eyes = transform.GetChildrenGameObject();
            inits = new[] { eyes[0].transform.position, eyes[1].transform.position };
            lines = new[] { eyes[0].GetComponent<LineRenderer>(), eyes[1].GetComponent<LineRenderer>() };
            lines.ForEach(l => l.Width(lineWidth));
        }

        void Update()
        {
            Vector3 ofs = player.transform.position + this.ofs;
            Vector3[] directions = { ofs - eyes[0].transform.position, ofs - eyes[1].transform.position };
            float distance = Maths.Average(Vector2.Distance(ofs, eyes[0].transform.position), Vector2.Distance(ofs, eyes[1].transform.position));

            // プレイヤーが範囲内にいる
            if (!(IsPlayerOnDetectRange = playerDetectRange >= distance))
                return;

            Vector2 ave = new(Maths.Average(eyes[0].transform.position.x, eyes[1].transform.position.x),
                Maths.Average(eyes[0].transform.position.y, eyes[1].transform.position.y));
            Ray infrared = new(origin: ave, direction: player.transform.position.ToVec2() - ave);

            // 且つ、プレイヤーとの間に障害物がなかったら
            if (!Gobject.TryGetRaycast<Player>(infrared, distance))
                return;
            // if (Gobject.Raycast(out RaycastHit2D hit, infrared, distance))
            // ;

            for (int i = 0; i < eyes.Length; i++)
                eyes[i].transform.position = inits[i] + directions[i].normalized * EyeBump;

            if ((fireRapidTimer += Time.deltaTime) >= fireRapidRate)
            {
                fires.TryInstantiate(transform.position);
                fireRapidTimer = 0;
            }
        }
    }
}
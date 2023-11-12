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

        [SerializeField]
        float punishDuration = 3f;

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
            player = Gobject.Find(Constant.Tags.Player);
            var c = player.GetComponent<BoxCollider2D>();
            ofs = new(0, c.Size().y / 2);

            eyes = transform.GetChildren();
            inits = new[] { eyes[0].transform.position, eyes[1].transform.position };
            lines = new[] { eyes[0].GetComponent<LineRenderer>(), eyes[1].GetComponent<LineRenderer>() };
            lines.ForEach(l => l.Width(lineWidth));
        }

        void Update()
        {
            var ofs = player.transform.position + this.ofs;
            var directions = new[] { ofs - eyes[0].transform.position, ofs - eyes[1].transform.position };
            var distance = Maths.Average(
                Vector2.Distance(ofs, eyes[0].transform.position),
                Vector2.Distance(ofs, eyes[1].transform.position));

            if (IsPlayerOnDetectRange = playerDetectRange >= distance)
            {
                for (int i = 0; i < eyes.Length; i++)
                {
                    eyes[i].transform.position = inits[i] + directions[i].normalized * EyeBump;
                }

                if ((fireRapidTimer += Time.deltaTime) >= fireRapidRate)
                {
                    fires.TryGenerate(transform.position);
                    fireRapidTimer = 0;
                }
            }
        }
    }
}
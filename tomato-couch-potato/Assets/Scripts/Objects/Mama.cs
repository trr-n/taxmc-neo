using System.Collections;
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
        [Tooltip("気の短さ(秒)")]
        float pepperyLimit = 1;

        [SerializeField]
        float lineWidth = .5f;

        [SerializeField]
        float playerDetectRange = 13f;

        [SerializeField]
        float punishDuration = 3f;

        float pepperyTimer = 0;

        public (float Limit, float Timer, float Ratio) Peppery
        => (
            Limit: pepperyLimit,
            Timer: pepperyTimer,
            Ratio: pepperyTimer / pepperyLimit
        );

        GameObject player;
        Vector3 ofs, offset;
        float distance;

        GameObject[] eyes;
        Vector3[] inits;
        Vector3[] directions;
        LineRenderer[] lines;

        /// <summary>
        /// 黒目の可動域
        /// </summary>
        readonly float bump = 0.25f;

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
            lines.ForEach(line => line.Width(lineWidth));
        }

        void Update()
        {
            offset = player.transform.position + ofs;
            directions = new[] { offset - eyes[0].transform.position, offset - eyes[1].transform.position };
            distance = Maths.Average(Vector2.Distance(offset, eyes[0].transform.position),
                Vector2.Distance(offset, eyes[1].transform.position));

            if (IsPlayerOnDetectRange = playerDetectRange >= distance)
            {
                LookAtPlayer();
                FireShot();
            }
        }

        void LookAtPlayer()
        {
            for (int i = 0; i < eyes.Length; i++)
            {
                eyes[i].transform.position = inits[i] + directions[i].normalized * bump;
            }
        }

        void FireShot()
        {
            fireRapidTimer += Time.deltaTime;

            if (fireRapidTimer >= fireRapidRate)
            {
                print("mama fire!");
                fires.TryGenerate(transform.position);
                fireRapidTimer = 0;
            }
        }
    }
}

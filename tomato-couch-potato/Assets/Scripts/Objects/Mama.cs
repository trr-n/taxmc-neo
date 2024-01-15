using trrne.Box;
using UnityEngine;

namespace trrne.Core
{
    public class Mama : MonoBehaviour
    {
        [SerializeField]
        MamaFlag flag;

        [SerializeField]
        GameObject[] fires;

        [SerializeField]
        float fireRapidRate = 2f;
        float fireRapidTimer = 0;

        Player player;

        [SerializeField]
        GameObject[] eyes;
        Vector3[] inits;

        /// <summary>
        /// 黒目の可動域
        /// </summary>
        const float EYE_BUMP = 0.25f;

        void Start()
        {
            player = Gobject.GetWithTag<Player>(Constant.Tags.PLAYER);

            inits = new Vector3[eyes.Length];
            for (int i = 0; i < eyes.Length; ++i)
            {
                inits[i] = eyes[i].transform.position;
            }
        }

        void Update()
        {
            if (!flag.IsInsideRange)
            {
                return;
            }

            for (int i = 0; i < eyes.Length; ++i)
            {
                var offset = player.Core - eyes[i].transform.position;
                eyes[i].transform.position = inits[i] + offset.normalized * EYE_BUMP;
            }

            if ((fireRapidTimer += Time.deltaTime) >= fireRapidRate)
            {
                fires.TryInstantiate(transform.position);
                fireRapidTimer = 0;
            }
        }
    }
}

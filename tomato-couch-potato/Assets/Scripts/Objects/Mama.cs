using System;
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
        Vector3[] directions;

        /// <summary>
        /// 黒目の可動域
        /// </summary>
        const float EyeBump = 0.25f;

        void Start()
        {
            player = Gobject.GetWithTag<Player>(Config.Tags.Player);

            inits = new Vector3[eyes.Length];
            for (int i = 0; i < eyes.Length; i++)
            {
                inits[i] = eyes[i].transform.position;
            }
        }

        void Update()
        {
            print("OnRange: " + flag.OnRange);

            directions = new[] { player.Core - eyes[0].transform.position, player.Core - eyes[1].transform.position };

            // player within range
            if (!flag.OnRange)
            {
                return;
            }

            for (int i = 0; i < eyes.Length; i++)
            {
                eyes[i].transform.position = inits[i] + directions[i].normalized * EyeBump;
            }

            // the fires generate if timer value is upper than rapid rate 
            if ((fireRapidTimer += Time.deltaTime) >= fireRapidRate)
            {
                fires.TryInstantiate(transform.position);
                fireRapidTimer = 0;
            }
        }
    }
}

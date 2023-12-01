using trrne.Box;
using UnityEngine;

namespace trrne.Core
{
    public class NewbieSpawner : MonoBehaviour
    {
        [SerializeField]
        GameObject newbie;

        readonly (int range, int span) spawn = (20, 3);

        void Start()
        {
            InvokeRepeating(nameof(SpawnNewbie), 0, spawn.span);
        }

        void SpawnNewbie()
        {
            newbie.Instantiate(transform.position);
            // .GetComponent<Newbie>()
            // .facing = Newbie.Facing.Left;
        }
    }
}

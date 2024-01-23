using trrne.Box;
using UnityEngine;

namespace trrne.Core
{
    public class SpectreGenerator : MonoBehaviour
    {
        [SerializeField]
        GameObject spectre;

        GameObject alive;
        Transform player;

        void Start()
        {
            player = Gobject.GetWithTag(Constant.Tags.PLAYER).transform;
            alive = spectre.TryInstantiate(transform.position);
        }

        void Update()
        {
            transform.position = player.position + new Vector3(0, 12);
            if (alive == null)
            {
                alive = spectre.TryInstantiate(transform.position);
            }
        }
    }
}

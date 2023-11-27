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
            player = Gobject.GetWithTag(Constant.Tags.Player).transform;
            alive = spectre.TryInstantiate(transform.position);
        }

        void Update()
        {
            transform.position = player.position + new Vector3(0, 12f);

            if (alive != null)
            {
                return;
            }
            alive = spectre.TryInstantiate(transform.position);
        }
    }
}

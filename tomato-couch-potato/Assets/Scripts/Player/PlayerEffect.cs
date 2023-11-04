using trrne.Box;
using UnityEngine;
using UnityEngine.UI;

namespace trrne.Core
{
    public class PlayerEffect : MonoBehaviour
    {
        [SerializeField]
        Image mirror;

        Player player;

        void Start()
        {
            player = Gobject.GetWithTag<Player>(Constant.Tags.Player);
        }

        void Update()
        {
            mirror.enabled = player.IsMirroring;
        }
    }
}

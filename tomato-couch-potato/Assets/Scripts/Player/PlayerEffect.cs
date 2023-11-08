using trrne.Box;
using UnityEngine;
using UnityEngine.UI;

namespace trrne.Core
{
    public class PlayerEffect : MonoBehaviour
    {
        [SerializeField]
        Image mirrorI;

        [SerializeField]
        Text mirrorT;

        Player player;

        void Start()
        {
            player = Gobject.GetWithTag<Player>(Constant.Tags.Player);
        }

        void Update()
        {
            mirrorI.enabled = player.PunishFlags[(int)PunishType.Mirror];
        }
    }
}

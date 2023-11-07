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
            mirrorI.fillAmount = 0;
        }

        void Update()
        {
            // mirrorT.SetText(player.DurationTimer);
            // // mirror.enabled = player.IsMirroring;
            // if (player.DurationTimer <= 0)
            // {
            //     return;
            // }
            // var raw = 1 - player.DurationProgress;
            // mirrorI.fillAmount = raw; // player.DurationProgress; // 1 / player.DurationTimer;
        }
    }
}

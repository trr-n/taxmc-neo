using Chickenen.Pancreas;
using UnityEngine;
using UnityEngine.UI;

namespace Chickenen.Heart
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
            mirror.enabled = player.Mirror;
        }
    }
}

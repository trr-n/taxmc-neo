using Chickenen.Pancreas;
using UnityEngine;
using UnityEngine.UI;

namespace Chickenen.Heart
{
    public class PlayerEffect : MonoBehaviour
    {
        [SerializeField]
        Image reverse;

        Player player;

        void Start()
        {
            player = Gobject.GetWithTag<Player>(Constant.Tags.Player);
        }

        void Update()
        {
            reverse.enabled = player.Reverse;
        }
    }
}

using UnityEngine;
using trrne.Box;

namespace trrne.Core
{
    public class PlayerEffect : MonoBehaviour
    {
        GameObject[] icons;

        Player player;

        void Start()
        {
            icons = transform.GetChildrenGameObject();
            player = Gobject.GetWithTag<Player>(Constant.Tags.Player);
        }

        void Update()
        {
            // show active fx icon
            for (int i = 0; i < icons.Length; i++)
            {
                icons[i].SetActive(player.EffectFlags[i]);
            }
        }
    }
}
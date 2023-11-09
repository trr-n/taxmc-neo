using UnityEngine;
using trrne.Box;

namespace trrne.Core
{
    public class PlayerEffect : MonoBehaviour
    {
        [SerializeField]
        float Offset = 128f, Y = 64;

        GameObject[] icons;
        RectTransform[] iconRTs;

        Player player;

        void Start()
        {
            player = Gobject.GetWithTag<Player>(Constant.Tags.Player);
            icons = transform.GetChildren();
            iconRTs = GetComponentsInChildren<RectTransform>();
        }

        void Update()
        {
            for (int i = 0, j = 0; i < icons.Length; i++)
            {
                if (icons[i].activeInHierarchy) // && player.PunishFlags[i])
                {
                    iconRTs[i].position = new(j * Offset, Y);
                    j++;
                }
            }
        }
    }
}
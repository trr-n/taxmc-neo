using UnityEngine;
using trrne.Box;
using UnityEngine.UI;
using System.Linq;

namespace trrne.Core
{
    public class PlayerEffect : MonoBehaviour
    {
        [SerializeField]
        Text debugT;

        [SerializeField]
        float Offset = 100f, Y = 0;
        const float OffsetX = 64;

        GameObject[] icons;
        RectTransform[] iconRTs;

        Player player;
        RectTransform selfRT;

        void Start()
        {
            selfRT = GetComponent<RectTransform>();
            selfRT.transform.position = new(50, 50);

            icons = transform.GetChildren();
            iconRTs = GetComponentsInChildren<RectTransform>();

            player = Gobject.GetWithTag<Player>(Constant.Tags.Player);
        }

        void Update()
        {
            SetPosition();
            Draw();
        }

        void SetPosition()
        {
            for (int i = 0, j = 0; i < icons.Length; i++)
            {
                if (icons[i].activeInHierarchy && player.EffectFlags[i])
                {
                    iconRTs[i].transform.position = new(OffsetX + j * Offset, Y);
                    j++;
                }
            }
        }

        void Draw()
        {
            foreach (var (icon, flag) in icons.SelectMany(i => player.EffectFlags.Select(f => (i, f))))
            {
                icon.SetActive(flag);
            }

            // for (int i = 0; i < icons.Length; i++)
            // {
            //     icons[i].SetActive(player.EffectFlags[i]);
            // }
        }
    }
}
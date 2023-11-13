using UnityEngine;
using trrne.Box;
using UnityEngine.UI;

namespace trrne.Core
{
    public class PlayerEffect : MonoBehaviour
    {
        [SerializeField]
        Text debugT;

        const float Offset = 100f, Y = 0, OffsetX = 64;

        GameObject[] icons;
        RectTransform[] iconRTs;

        Player player;
        RectTransform selfRT;

        void Start()
        {
            selfRT = GetComponent<RectTransform>();
            selfRT.transform.position = new(50, 50);

            icons = transform.GetChildrenGameObject();
            iconRTs = GetComponentsInChildren<RectTransform>();

            player = Gobject.GetComponentWithTag<Player>(Constant.Tags.Player);
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
            for (int i = 0; i < icons.Length; i++)
            {
                icons[i].SetActive(player.EffectFlags[i]);
            }
        }
    }
}
using UnityEngine;
using trrne.Box;
using UnityEngine.UI;

namespace trrne.Core
{
    public class PlayerEffect : MonoBehaviour
    {
        GameObject[] icons;

        Player player;

        void Start()
        {
            icons = transform.GetChildrenGameObject();
            player = Gobject.GetComponentWithTag<Player>(Constant.Tags.Player);
        }

        void Update()
        {
            // アクティブなエフェクトのアイコンだけ表示
            for (int i = 0; i < icons.Length; i++)
                icons[i].SetActive(player.EffectFlags[i]);
        }
    }
}
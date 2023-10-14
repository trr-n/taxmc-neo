using System.ComponentModel.Design;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using trrne.Bag;
using trrne.Brain;
using System;

namespace trrne.Arm
{
    public class SelectManager : MonoBehaviour
    {
        [SerializeField]
        Image panel;

        [SerializeField]
        PauseMenu menu;

        [SerializeField]
        GameObject[] homes;
        BoxCollider2D[] hitboxes;
        readonly string prefix = "level";

        Player player;

        async void Start()
        {
            player = Gobject.GetWithTag<Player>(Constant.Tags.Player);

            Physics2D.gravity = Vector100.zero2d;

            hitboxes = new BoxCollider2D[homes.Length];
            for (int i = 0; i < hitboxes.Length; i++)
            {
                hitboxes[i] = homes[i].GetComponent<BoxCollider2D>();
            }
            panel.SetAlpha(0);

            await WhenStartGame();
        }


        void Update()
        {
            Welcome();
        }

        void Welcome()
        {
            foreach (var (home, hitbox) in Shorthand.Merge(homes, hitboxes))
            {
                if (Gobject.BoxCast2D(out _, home.Position2() + hitbox.offset, hitbox.bounds.size * 1.01f, Constant.Layers.Player))
                {
                    switch (int.Parse(home.name.Delete(prefix)))
                    {
                        case 0:
                            Shorthand.BoolAction(Inputs.Down(Constant.Keys.Button),
                                () => Recorder.Instance.Next(name: Constant.Scenes.Game1));
                            break;

                        case 1:
                        default:
                            print("not yet, not soon");
                            break;
                    }
                }
            }
        }

        async UniTask WhenStartGame()
        {
            // // 操作不可に
            // player.ctrlable = false;

            // パネルを表示
            menu.Active();

            // Buttonを押すまで待機
            await UniTask.WaitUntil(() => Inputs.Down(Constant.Keys.Button));

            // パネルを非表示に
            menu.Inactive();

            // 操作可能に
            player.controllable = true;
        }
    }
}
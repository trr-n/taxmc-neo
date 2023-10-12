using Cysharp.Threading.Tasks;
using trrne.Bag;
using UnityEngine;
using UnityEngine.UI;

namespace trrne.Body.Select
{
    public class SelectManager : MonoBehaviour
    {
        [SerializeField]
        Image escmenu, panel;

        [SerializeField]
        GameObject[] homes;
        BoxCollider2D[] hitboxes;
        readonly string prefix = "level";

        Player player;

        async void Start()
        {
            player = Gobject.GetWithTag<Player>(Constant.Tags.Player);
            await WhenStartGame();

            Physics2D.gravity = Vector100.zero2d;

            hitboxes = new BoxCollider2D[homes.Length];
            for (int i = 0; i < hitboxes.Length; i++)
            {
                hitboxes[i] = homes[i].GetComponent<BoxCollider2D>();
            }

            panel.SetAlpha(0);
        }


        void Update()
        {
            Welcome();
        }

        void Welcome()
        {
            foreach (var (home, hitbox) in homes.Merge(hitboxes))
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
            // 操作方法を記載されているパネルを表示
            escmenu.SetAlpha(1f);

            // 操作不可に
            player.controllable = false;

            // Buttonを押すまで表示
            await UniTask.WaitUntil(() => Inputs.Down(Constant.Keys.Button));

            // パネルを非表示に
            escmenu.SetAlpha(0);

            // 操作可能に
            player.controllable = true;
        }
    }
}
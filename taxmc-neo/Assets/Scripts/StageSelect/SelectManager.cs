using System.Reflection;
using trrne.Bag;
using UnityEngine;
using UnityEngine.UI;

namespace trrne.Body
{
    public class SelectManager : MonoBehaviour
    {
        [SerializeField]
        Image panel;

        [SerializeField]
        GameObject[] homes;
        BoxCollider2D[] hitboxes;
        readonly string prefix = "level";

        void Start()
        {
            Physics2D.gravity = Vector100.zero2d;

            hitboxes = new BoxCollider2D[homes.Length];
            for (int i = 0; i < homes.Length; i++)
            {
                hitboxes[i] = homes[i].GetComponent<BoxCollider2D>();
            }
        }


        void Update()
        {
            print(Physics2D.gravity);
            Welcome();
        }

        void Welcome()
        {
            foreach (var (home, hitbox) in homes.Merge(hitboxes))
            {
                if (Gobject.BoxCast2D(out _, home.Position2() + hitbox.offset, hitbox.bounds.size * 1.01f, Constant.Layers.Player))
                {
                    Fade(true);
                    switch (int.Parse(home.name.Delete(prefix)))
                    {
                        case 0:
                            Shorthand.BoolAction(Inputs.Down(Constant.Keys.Button), () => Recorder.Instance.Next(Constant.Scenes.Game1));
                            break;

                        case 1:
                        default:
                            print("not yet, not soon");
                            break;
                    }
                }
                else
                {
                    Fade(false);
                }
            }
        }

        bool fading = false;
        void Fade(bool fin)
        {
            if (fading)
            {
                return;
            }
            fading = true;

            // フェードの処理

            fading = false;
        }
    }
}
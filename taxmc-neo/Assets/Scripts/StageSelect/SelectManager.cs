using trrne.Bag;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace trrne.Body
{
    public class SelectManager : MonoBehaviour
    {
        [SerializeField]
        Text text;

        [SerializeField]
        GameObject[] homes;
        BoxCollider2D[] hitboxes;
        readonly string prefix = "level";

        GameObject player;

        void Start()
        {
            Physics2D.gravity = Vector100.zero2d;

            player = Gobject.Find(Constant.Tags.Player);

            hitboxes = new BoxCollider2D[homes.Length];
            for (int i = 0; i < homes.Length; i++)
            {
                hitboxes[i] = homes[i].GetComponent<BoxCollider2D>();
            }
        }

        void Update()
        {
            Welcome();
        }

        void Welcome()
        {
            // https://baba-s.hatenablog.com/entry/2020/01/10/090000
            foreach (var (home, hitbox) in homes.Merge(hitboxes))
            {
                if (Gobject.BoxCast2D(out var door,
                    home.Position2() + hitbox.offset, hitbox.bounds.size * 1.01f, Constant.Layers.Player))
                {
                    if (Inputs.Down(KeyCode.J))
                    {
                        switch (int.Parse(home.name.Split(prefix)[1]))
                        {
                            case 0:
                                Recorder.Instance.Next(Constant.Scenes.Game1);
                                break;

                            case 1:
                            default:
                                print("not yet");
                                break;
                        }
                    }
                }
            }
        }
    }
}
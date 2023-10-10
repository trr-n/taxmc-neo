using trrne.Bag;
using Unity.Mathematics;
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
        GameObject key;

        [SerializeField]
        GameObject[] homes;
        BoxCollider2D[] hitboxes;
        readonly string prefix = "level";

        GameObject player, switched;

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
            print(Physics2D.gravity);
            Welcome();
        }

        void Welcome()
        {
            foreach (var (home, hitbox) in homes.Merge(hitboxes))
            {
                if (Gobject.BoxCast2D(out _, home.Position2() + hitbox.offset, hitbox.bounds.size * 1.01f, Constant.Layers.Player))
                {
                    switch (int.Parse(home.name.Replace(prefix, "")))
                    {
                        case 0:
                            key.TryGenerate(transform.position);
                            Shorthand.BoolAction(Inputs.Down(Constant.Keys.Button), () => Recorder.Instance.Next(Constant.Scenes.Game1));
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
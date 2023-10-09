using trrne.Bag;
using UnityEngine;

namespace trrne.Body
{
    public class SelectManager : MonoBehaviour
    {
        [SerializeField]
        GameObject[] homes;
        BoxCollider2D[] hitboxes;

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
            In2Home();
        }

        void In2Home()
        {
            foreach (var hitbox in hitboxes)
            {
                foreach (var home in homes)
                {
                    if (Gobject.BoxCast2D(out _,
                        home.transform.position.ToVec2() + hitbox.offset, hitbox.bounds.size * 1.01f, Constant.Layers.Player))
                    {
                    }
                }
            }
        }
    }
}
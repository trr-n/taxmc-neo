using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
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
            Welcome();
        }

        void Welcome()
        {
            // https://baba-s.hatenablog.com/entry/2020/01/10/090000
            var houses1 = homes.Merge(hitboxes);

            foreach (var (home, hitbox) in houses1)
            {
                if (Gobject.BoxCast2D(out var hit,
                    home.Position2() + hitbox.offset, hitbox.bounds.size * 1.01f, Constant.Layers.Player))
                {
                    print(hit.collider.name);
                }
            }
        }
    }
}
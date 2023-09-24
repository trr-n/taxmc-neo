using System.Collections;
using System.Collections.Generic;
using trrne.Appendix;
using UnityEngine;

namespace trrne.Body
{
    public class Flag : MonoBehaviour
    {
        [SerializeField]
        Sprite[] flags;

        SpriteRenderer sr;
        Vector2 boxsize;

        Player player;
        bool used = false;

        void Start()
        {
            player = Gobject.GetWithTag<Player>(Fixed.Tags.Player);

            sr = GetComponent<SpriteRenderer>();
            sr.sprite = flags[0];

            boxsize = new(sr.bounds.size.x / 2, sr.bounds.size.y);
        }

        void Update()
        {
            if (used) { return; }

            // プレイヤーが触れたらおろす
            if (Gobject.BoxCast2D(out _, transform.position, boxsize, Fixed.Layers.Player, 0, 0))
            {
                sr.sprite = flags[1];

                print("ok");

                player.SetCP(new(transform.position.x, Numeric.Cutail(transform.position.y)));
                used = true;
            }
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.HSVToRGB(Time.unscaledDeltaTime % 1, 1, 1);
            Gizmos.DrawWireCube(transform.position, boxsize);
        }
    }
}

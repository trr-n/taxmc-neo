using System.Collections;
using System.Collections.Generic;
using trrne.Bag;
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
            player = Gobject.GetWithTag<Player>(Constant.Tags.Player);

            sr = GetComponent<SpriteRenderer>();
            sr.sprite = flags[0];

            boxsize = new(sr.bounds.size.x / 2, sr.bounds.size.y);
        }

        void Update()
        {
            // プレイヤーが触れたらおろす
            if (!used && Gobject.BoxCast2D(out var hit, transform.position, boxsize, Constant.Layers.Player, 0, 0))
            {
                if (!hit.Compare(Constant.Tags.Player)) { return; }
                if (hit.Get<Player>().IsDieProcessing) { return; }

                sr.sprite = flags[1];

                player.SetCheckpoint(new(transform.position.x, Numeric.Cutail(transform.position.y)));
                used = true;
            }
        }
    }
}

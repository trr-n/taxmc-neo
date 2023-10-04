using System.Collections;
using System.Collections.Generic;
using trrne.Bag;
using UnityEngine;

namespace trrne.Body
{
    public class Flag : Objectt
    {
        Vector2 hitbox;

        Player player;
        bool used = false;

        protected override void Start()
        {
            player = Gobject.GetWithTag<Player>(Constant.Tags.Player);

            sr = GetComponent<SpriteRenderer>();
            sr.sprite = sprites[0];

            hitbox = new(sr.bounds.size.x / 2, sr.bounds.size.y);
        }

        protected override void Behavior()
        {
            // プレイヤーが触れたらおろす
            if (!used && Gobject.BoxCast2D(out var hit, transform.position, hitbox, Constant.Layers.Player))
            {
                if (!hit.CompareTag(Constant.Tags.Player))
                {
                    return;
                }

                if (!hit.Get<Player>().isDieProcessing)
                {
                    sr.sprite = sprites[1];

                    player.SetCheckpoint(new(transform.position.x, Numeric.Cutail(transform.position.y)));
                    used = true;
                }
            }
        }
    }
}

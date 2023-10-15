using trrne.WisdomTeeth;
using UnityEngine;

namespace trrne.Body
{
    public class Flag : Objectt
    {
        // Vector2 hitbox;

        // Player player;
        bool isUsed = false;

        protected override void Start()
        {
            // player = Gobject.GetWithTag<Player>(Constant.Tags.Player);

            sr = GetComponent<SpriteRenderer>();
            sr.sprite = sprites[0];

            // hitbox = new(sr.bounds.size.x / 2, sr.bounds.size.y);
        }

        protected override void Behavior()
        {
            // // プレイヤーが触れたらおろす
            // if (!isUsed && Gobject.BoxCast2D(out var hit, transform.position, hitbox, Constant.Layers.Player))
            // {
            //     if (!hit.CompareTag(Constant.Tags.Player))
            //     {
            //         return;
            //     }

            //     if (!hit.Get<Player>().isDieProcessing)
            //     {
            //         sr.sprite = sprites[1];

            //         player.SetCheckpoint(new(transform.position.x, Numero.Cutail(transform.position.y)));
            //         isUsed = true;
            //     }
            // }
        }

        void OnTriggerEnter2D(Collider2D info)
        {
            if (!isUsed && info.TryGet<Player>(out var player))
            {
                isUsed = true;

                sr.sprite = sprites[1];
                player.SetCheckpoint(new(transform.position.x, Numero.Cutail(transform.position.y)));
            }
        }
    }
}

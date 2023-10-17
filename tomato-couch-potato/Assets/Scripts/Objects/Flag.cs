using trrne.Pancreas;
using UnityEngine;

namespace trrne.Heart
{
    public class Flag : Object
    {
        bool isUsed = false;

        protected override void Start()
        {
            base.Start();
            sr.sprite = sprites[0];
        }

        protected override void Behavior() { }

        void OnTriggerEnter2D(Collider2D info)
        {
            if (!isUsed && info.TryGet(out Player player))
            {
                isUsed = true;

                sr.sprite = sprites[1];
                player.SetCheckpoint(new(transform.position.x, Maths.Cutail(transform.position.y)));
            }
        }
    }
}

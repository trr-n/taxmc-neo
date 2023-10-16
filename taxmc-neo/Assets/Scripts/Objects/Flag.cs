using trrne.WisdomTeeth;
using UnityEngine;

namespace trrne.Body
{
    public class Flag : Objectt
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

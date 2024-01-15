using trrne.Box;
using UnityEngine;

namespace trrne.Core
{
    public class PC : Object
    {
        Color[] DisplayColors => new Color[] { Color.red, Color.green };
        const int UNUSED = 0;
        const int USED = 1;

        SpriteRenderer display;

        bool isLoading = false;

        protected override void Start()
        {
            base.Start();

            display = transform.GetFromChild<SpriteRenderer>();
            display.color = DisplayColors[UNUSED];
        }

        protected override void Behavior() { }

        void OnTriggerEnter2D(Collider2D info)
        {
            if (!isLoading && info.TryGetComponent(out Player player) && !player.IsDying)
            {
                isLoading = true;

                effects.TryInstantiate(transform.position);
                display.color = DisplayColors[USED];

                player.SetCheckpoint(transform.position.x, MF.Cutail(transform.position.y));
            }
        }
    }
}

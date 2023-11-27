using trrne.Box;
using UnityEngine;

namespace trrne.Core
{
    public class PC : Object
    {
        Color[] DisplayColors => new Color[] { Color.red, Color.green };
        const int Unused = 0, Used = 1;

        SpriteRenderer display;

        bool isLoading = false;

        protected override void Start()
        {
            base.Start();
            (display = transform.GetFromChild<SpriteRenderer>(0))
                .color = DisplayColors[Unused];
        }

        protected override void Behavior() { }

        void OnTriggerEnter2D(Collider2D info)
        {
            if (!isLoading && info.TryGetComponent(out Player player) && !player.IsDying)
            {
                isLoading = true;

                effects.TryInstantiate(transform.position);
                display.color = DisplayColors[Used];

                Vector2 self = transform.position;
                player.SetCheckpoint(new(self.x, Maths.Cutail(self.y)));
            }
        }
    }
}

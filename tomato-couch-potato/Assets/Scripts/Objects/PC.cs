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
            display = transform.GetFromChild<SpriteRenderer>(0);
            display.color = DisplayColors[Unused];
        }

        protected override void Behavior() { }

        void OnTriggerEnter2D(Collider2D info)
        {
            if (!isLoading && info.TryGetComponent(out Player player) && !player.IsDying)
            {
                isLoading = true;
                effects.TryInstantiate(transform.position);
                display.color = DisplayColors[Used];
                player.SetCheckpoint(
                    x: transform.position.x,
                    y: Maths.Cutail(transform.position.y)
                );
            }
        }
    }
}

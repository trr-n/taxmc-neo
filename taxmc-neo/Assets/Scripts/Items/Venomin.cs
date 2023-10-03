using UnityEngine;
using trrne.Bag;
using Cysharp.Threading.Tasks;

namespace trrne.Body
{
    public class Venomin : Objectt
    {
        protected override void Start()
        {
            base.Start();
            animatable = false;
        }

        protected override async void Behavior()
        {
            // プレイヤーが触れたら死
            if (Gobject.BoxCast2D(out var hit, transform.position, size - .5f * (Vector2)Coordinate.x, Constant.Layers.Player))
            {
                effects.TryGenerate(transform.position);
                await hit.Get<Player>().Die();
            }
        }
    }
}

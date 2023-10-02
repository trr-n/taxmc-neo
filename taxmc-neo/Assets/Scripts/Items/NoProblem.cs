using UnityEngine;
using trrne.Bag;
using Cysharp.Threading.Tasks;

namespace trrne.Body
{
    public class NoProblem : Item
    {
        [SerializeField]
        GameObject hitFX;

        protected override async void Receive()
        {
            // プレイヤーに触れたらblankRate%の確率で初期値に戻す
            if (Gobject.BoxCast2D(out var hit, transform.position, size - .5f * (Vector2)Coordinate.x, Constant.Layers.Player))
            {
                // hitFX.Generate(transform.position);

                // Blanc OR Negro
                await hit.Get<Player>().Die();
            }
        }
    }
}

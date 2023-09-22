using UnityEngine;
using trrne.utils;
using Cysharp.Threading.Tasks;

namespace trrne.Game
{
    public class NoProblem : Item
    {
        [SerializeField]
        GameObject hitFX;

        protected override async void Receive()
        {
            // プレイヤーに触れたらblankRate%の確率で初期値に戻す
            if (Gobject.BoxCast2D(out var hit, transform.position, sr.bounds.size, Constant.Layers.Player))
            {
                // Blanc OR Negro
                await hit.Get<Player>().Die();

                if (hitFX != null)
                {
                    // TODO add fx
                    hitFX.Generate(transform.position);
                }
            }
        }
    }
}

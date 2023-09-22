using UnityEngine;
using trrne.utils;

namespace trrne.Game
{
    public class NoProblem : Item
    {
        [SerializeField]
        GameObject hitFX;

        protected override void Receive()
        {
            // プレイヤーに触れたらblankRate%の確率で初期値に戻す
            if (Gobject.BoxCast2D(out var hit, transform.position, sr.bounds.size, Constant.Layers.Player))
            {
                // Blanc OR Negro
                hit.Get<Player>().Die();

                if (hitFX != null)
                {
                    // TODO add fx
                    hitFX.Generate(transform.position);
                }
            }
        }
    }
}

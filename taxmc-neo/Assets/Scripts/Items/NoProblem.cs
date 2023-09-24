using UnityEngine;
using trrne.Appendix;
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
            if (Gobject.BoxCast2D(out var hit, transform.position, sr.bounds.size, Fixed.Layers.Player))
            {
                try { hitFX.Generate(transform.position); } catch { }
                // Blanc OR Negro
                await hit.Get<Player>().Die();
            }
        }
    }
}

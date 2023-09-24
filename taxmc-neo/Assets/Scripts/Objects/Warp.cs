using Cysharp.Threading.Tasks;
using trrne.Appendix;
using UnityEngine;

namespace trrne.Body
{
    public class Warp : Objectt
    {
        [SerializeField]
        GameObject hitFX;

        [SerializeField]
        Vector2 to;

        bool warping = false;

        protected override async void Behavior()
        {
            if (Gobject.BoxCast2D(out var box, transform.position, size * 0.66f, Fixed.Layers.Player)
                && box.Compare(Fixed.Tags.Player)
                && !warping)
            {
                warping = true;

                // hitFX.Generate(transform.position);
                print("warp fxed");

                var hitSR = box.Get<SpriteRenderer>();
                hitSR.SetAlpha(0);

                await UniTask.DelayFrame(Numeric.Cutail(App.fps / 4));

                hitSR.SetAlpha(1);
                box.SetPosition(to);

                warping = false;
            }
        }
    }
}

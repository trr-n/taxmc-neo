using Cysharp.Threading.Tasks;
using trrne.Bag;
using UnityEngine;
using System.Collections;

namespace trrne.Body
{
    public class Warp : Objectt
    {
        [SerializeField]
        Vector2 to;

        bool warping = false;

        protected override async void Behavior()
        {
            if (!warping && Gobject.BoxCast2D(out var hit, transform.position, size * 0.66f, Constant.Layers.Player))
            {
                if (hit.Try(out Player player) && player.isDieProcessing)
                {
                    return;
                }
                warping = true;

                effects.TryGenerate(transform.position);

                await UniTask.DelayFrame(App.fpsint / 10);
                hit.SetPosition(to);

                StartCoroutine(AfterDelay());
            }

        }

        IEnumerator AfterDelay()
        {
            yield return null;
            warping = false;
        }
    }
}

using Cysharp.Threading.Tasks;
using trrne.Bag;
using UnityEngine;
using System.Collections;

namespace trrne.Body
{
    public class Warp : Objectt
    {
        [SerializeField]
        GameObject hitFX;

        [SerializeField]
        Vector2 to;

        bool warping = false;
        RaycastHit2D hit;

        protected override async void Behavior()
        {
            if (!warping && Gobject.BoxCast2D(out hit, transform.position, size * 0.66f, Fixed.Layers.Player))
            {
                warping = true;

                hitFX.Generate(transform.position);
                await UniTask.DelayFrame(App.fpsint / 10);
                hit.SetPosition(to);

                StartCoroutine(hoge());
            }

        }

        IEnumerator hoge()
        {
            // yield return new WaitForSecondsRealtime(0);
            yield return null;
            warping = false;
        }
    }
}

using Cysharp.Threading.Tasks;
using trrne.Appendix;
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
                // await UniTask.DelayFrame(Numeric.Cutail(App.fps / 10));
                await UniTask.Delay(1000);
                hit.SetPosition(to);

                //! 2回実行されちゃうからこるーちんで
                // warping = false;
                StartCoroutine(hoge());
            }

        }

        IEnumerator hoge()
        {
            print("yap");
            yield return new WaitForSecondsRealtime(0);
            warping = false;
        }
    }
}

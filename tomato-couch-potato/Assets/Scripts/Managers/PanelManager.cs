using System.Collections;
using Chickenen.Heart;
using Chickenen.Pancreas;
using UnityEngine;

namespace Chickenen.Brain
{
    public class PanelManager : MonoBehaviour
    {
        [SerializeField]
        CanvasGroup canvas;

        readonly float showingTime = 3;
        readonly float fadingSpeed = 10;
        readonly Stopwatch sw = new();

        Player player;

        void Start()
        {
            canvas.alpha = 1;
            sw.Start();

            player = Gobject.GetWithTag<Player>(Constant.Tags.Player);
            player.Controllable = false;
        }

        void Update()
        {
            if (sw.SecondF() >= showingTime)
            {
                sw.Reset();
                StartCoroutine(FadeOut());
            }
        }

        IEnumerator FadeOut()
        {
            float alpha = 1f;

            while (alpha >= 0)
            {
                yield return null;
                alpha -= Time.unscaledDeltaTime * fadingSpeed;
                canvas.alpha = alpha;
            }

            canvas.alpha = 0;

            yield return new WaitForSeconds(.5f);
            player.Controllable = true;
        }
    }
}
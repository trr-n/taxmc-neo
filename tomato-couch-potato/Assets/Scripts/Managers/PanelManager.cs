using System.Collections;
using trrne.Heart;
using trrne.Pancreas;
using UnityEngine;

namespace trrne.Brain
{
    public class PanelManager : MonoBehaviour
    {
        [SerializeField]
        CanvasGroup canvas;

        readonly float showing = 3, fadingSpeed = 10;
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
            if (sw.Sf >= showing)
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

// keychron k8 pro 青歯便利すぎて草
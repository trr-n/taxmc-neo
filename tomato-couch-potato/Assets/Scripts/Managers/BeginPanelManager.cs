using System.Collections;
using trrne.Core;
using trrne.Box;
using UnityEngine;

namespace trrne.Brain
{
    public class BeginPanelManager : MonoBehaviour
    {
        [SerializeField]
        CanvasGroup canvas;

        const float SHOWING_TIME = 3;
        const float FADING_SPEED = 10;
        readonly Stopwatch sw = new();

        Player player;

        void Start()
        {
            canvas.alpha = 1;
            sw.Start();

            player = Gobject.GetWithTag<Player>(Constant.Tags.PLAYER);
            player.Controllable = false;
        }

        void Update()
        {
            if (sw.sf >= SHOWING_TIME)
            {
                sw.Reset();
                StartCoroutine(FadeOut());
            }
        }

        IEnumerator FadeOut()
        {
            float alpha = 1f;
            while ((alpha -= Time.unscaledDeltaTime * FADING_SPEED) >= 0)
            {
                yield return null;
                canvas.alpha = alpha;
            }
            canvas.alpha = 0;
            yield return null; // new WaitForSeconds(.5f);
            player.Controllable = true;
        }
    }
}
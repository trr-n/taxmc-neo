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

        const float showingTime = 3;
        const float fadingSpeed = 10;
        readonly Stopwatch sw = new();

        Player player;

        void Start()
        {
            canvas.alpha = 1;
            sw.Start();

            player = Gobject.GetComponentWithTag<Player>(Constant.Tags.Player);
            player.Controllable = false;
        }

        void Update()
        {
            if (sw.Secondf() >= showingTime)
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
using System.Collections;
using trrne.Bag;
using UnityEngine;
using UnityEngine.UI;

namespace trrne.Brain
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField]
        GameObject panel;
        CanvasGroup canvas;

        bool pausing;
        public bool isPausing => pausing;

        (float speed, bool during) fade = (5f, false);

        void Start()
        {
            canvas = panel.GetComponent<CanvasGroup>();
        }

        void Update()
        {
            pausing = canvas.alpha >= .5f;
            PanelControl();
        }

        public void Active()
        {
            State(true);
        }

        public void Inactive()
        {
            State(false);
        }

        void State(bool active)
        {
            Fading(active);
            App.SetTimeScale(active ? 0 : 1);
        }

        void PanelControl()
        {
            if (Inputs.Down(Constant.Keys.Pause))
            {
                if (isPausing)
                {
                    Inactive();
                }
                else
                {
                    Active();
                }
            }
        }

        void Fading(bool fin)
        {
            if (fade.during)
            {
                return;
            }

            StartCoroutine(Fader(fin));
            // StartCoroutine(fin ? nameof(FadeIn) : nameof(FadeOut));
        }

        IEnumerator FadeIn()
        {
            fade.during = true;

            float alpha = 0;
            print("fadein called");

            while (true)
            {
                yield return null;

                alpha += Time.unscaledDeltaTime * fade.speed;
                canvas.alpha = alpha;

                if (alpha >= 1)
                {
                    break;
                }
            }

            fade.during = false;
        }

        IEnumerator FadeOut()
        {
            float alpha = 1;
            print("fadeout called");

            while (true)
            {
                yield return null;

                alpha -= Time.unscaledDeltaTime * fade.speed;
                canvas.alpha = alpha;

                if (alpha <= 0)
                {
                    break;
                }
            }

            fade.during = false;
        }

        // フェード処理
        IEnumerator Fader(bool fin)
        {
            yield return null;

            fade.during = true;
            float alpha = fin ? 0 : 1;

            // canvas.alpha = fin ? 1 : 0;

            while (true)
            {
                yield return null;

                alpha = fin ?
                    alpha += Time.unscaledDeltaTime * fade.speed :
                    alpha -= Time.unscaledDeltaTime * fade.speed;

                canvas.alpha = alpha;

                if (alpha.IsHardStucked(0, 1))
                {
                    break;
                }
            }

            // フェード処理終了
            fade.during = false;
        }
    }
}

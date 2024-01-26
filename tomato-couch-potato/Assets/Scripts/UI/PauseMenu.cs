using System.Collections;
using trrne.Box;
using UnityEngine;

namespace trrne.Brain
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField]
        GameObject panel;

        CanvasGroup canvas;

        public bool IsPausing { get; private set; }

        const float FADE_SPEED = 10f;
        bool isFading = false;

        void Start()
        {
            canvas = panel.GetComponent<CanvasGroup>();
        }

        void Update()
        {
            IsPausing = canvas.alpha >= .5f;
            PanelControl();
        }

        public void Active() => State(true);
        public void Inactive() => State(false);

        void State(bool active)
        {
            FaderHandle(active);
            // Time.timeScale = active ? 0 : 1;
        }

        void PanelControl()
        {
            if (Inputs.Down(Constant.Keys.PAUSE))
            {
                State(!IsPausing);
            }
        }

        void FaderHandle(bool fin)
        {
            if (!isFading)
            {
                StartCoroutine(Fader(fin));
            }
        }

        /// <summary>
        /// フェード処理
        /// </summary>
        IEnumerator Fader(bool fin)
        {
            isFading = true;
            float alpha = fin ? 0f : 1f;
            canvas.alpha = alpha;
            while (alpha >= 0 && alpha <= 1) // 0 >= alpha >= 1
            {
                if (fin)
                {
                    alpha += Time.unscaledDeltaTime * FADE_SPEED;
                }
                else
                {
                    alpha -= Time.unscaledDeltaTime * FADE_SPEED;
                }
                // print(alpha);
                canvas.alpha = alpha;
                yield return null;
            }
            isFading = false;
        }
    }
}

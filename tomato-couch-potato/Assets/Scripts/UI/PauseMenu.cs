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

        const float FadeSpeed = 10f;
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
            // FIXME タイムスケールいじるとキャンバスが上に飛んでいく
            // Time.timeScale = active ? 0 : 1;
        }

        void PanelControl()
        {
            if (!Inputs.Down(Constant.Keys.Pause))
            {
                return;
            }
            State(!IsPausing);
        }

        void FaderHandle(bool fin)
        {
            if (isFading)
            {
                return;
            }
            StartCoroutine(Fader(fin));
        }

        /// <summary>
        /// フェード処理
        /// </summary>
        IEnumerator Fader(bool fin)
        {
            isFading = true;
            float alpha = fin ? 0f : 1;
            while (alpha.IsCaged(0, 1))
            {
                canvas.alpha = FadeSpeed * fin switch
                {
                    true => alpha += Time.unscaledDeltaTime,
                    false or _ => alpha -= Time.unscaledDeltaTime
                };
                yield return null;
            }
            isFading = false;
        }
    }
}

using System.Runtime.CompilerServices;
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
            // FIXME タイムスケールいじるとエラー
            // Time.timeScale = active ? 0 : 1;
        }

        void PanelControl()
        {
            if (!Inputs.Down(Constant.Keys.Pause))
                return;

            if (IsPausing)
                State(false);
            else
                State(true);
        }

        void FaderHandle(bool fin)
        {
            if (isFading)
                return;
            StartCoroutine(Fader(fin));
        }

        /// <summary>
        /// フェード処理
        /// </summary>
        IEnumerator Fader(bool fin)
        {
            isFading = true;
            yield return null;
            float alpha = fin ? 0f : 1;
            while (alpha.IsCaged(0, 1))
            {
                canvas.alpha = (fin ?
                    alpha += Time.unscaledDeltaTime :
                    alpha -= Time.unscaledDeltaTime
                ) * FadeSpeed;
                yield return null;
            }
            isFading = false;
        }
    }
}

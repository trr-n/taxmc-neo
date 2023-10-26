using System.Collections;
using Cysharp.Threading.Tasks.Triggers;
using trrne.Pancreas;
using UnityEngine;

namespace trrne.Brain
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField]
        GameObject panel;
        CanvasGroup canvas;

        bool pausing;
        public bool IsPausing => pausing;

        (float speed, bool during) fade = (10f, false);

        void Start()
        {
            canvas = panel.GetComponent<CanvasGroup>();
        }

        void Update()
        {
            pausing = canvas.alpha >= .5f;
            PanelControl();
        }

        public void Active() => State(true);
        public void Inactive() => State(false);

        void State(bool active)
        {
            FadingHandle(active);
            App.SetTimeScale(active ? 0 : 1);
        }

        void PanelControl()
        {
            if (!Inputs.Down(Constant.Keys.Pause))
            {
                return;
            }

            if (IsPausing)
            {
                Inactive();
            }
            else
            {
                Active();
            }
        }

        void FadingHandle(bool fin)
        {
            if (!fade.during)
            {
                StartCoroutine(Fader(fin));
            }
        }

        // フェード処理
        IEnumerator Fader(bool fin)
        {
            fade.during = true;
            float alpha = fin ? 0f : 1;

            while (alpha.IsCaged(0, 1))
            {
                yield return null;

                alpha = fin ?
                    alpha += Time.unscaledDeltaTime * fade.speed :
                    alpha -= Time.unscaledDeltaTime * fade.speed;

                canvas.alpha = alpha;
            }

            // フェード処理終了
            fade.during = false;
        }
    }
}

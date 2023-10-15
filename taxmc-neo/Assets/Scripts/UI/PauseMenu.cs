using System.Collections;
using trrne.WisdomTeeth;
using UnityEngine;

namespace trrne.Brain
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField]
        GameObject panel;
        CanvasGroup canvas;

        bool pausing;
        public bool isPausing => pausing;

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
            FadingHandle(active);
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

        void FadingHandle(bool fin)
        {
            if (fade.during)
            {
                return;
            }

            StartCoroutine(Fader(fin));
        }

        // フェード処理
        IEnumerator Fader(bool fin)
        {
            fade.during = true;
            var alpha = fin ? 0f : 1;

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

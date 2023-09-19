using System;
using System.Collections;
using trrne.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace trrne.Game
{
    public enum FadeType { CutIn, CutOut }

    public class FadingPanel : MonoBehaviour
    {
        [SerializeField]
        float fadingSpeed = 1;

        float alfa;
        public float alpha => panel.GetAlpha();

        RectTransform recT;
        (Vector2 panel, Vector2 screen) size;

        Image panel;

        void Start()
        {
            panel = GetComponent<Image>();
            panel.color = Color.black;
            panel.SetAlpha(0);

            recT = GetComponent<RectTransform>();
        }

        void Update()
        {
            SyncingSize();

            // print("(10f).IsCaged(0, 9): " + 10f.IsCaged(0, 9));
        }

        /// <summary>
        /// パネルとシーンのサイズを同期
        /// </summary>
        void SyncingSize()
        {
            size = (recT.sizeDelta, new(Screen.width, Screen.height));

            if (size.panel != size.screen)
            {
                size.panel = size.screen;
            }
        }

        // public void Fade(FadeStyle style)
        // {
        //     switch (style)
        //     {
        //         case FadeStyle.In:
        //             break;

        //         case FadeStyle.Out:
        //             break;
        //     }
        // }

        IEnumerator Fader(FadeType cut)
        {
            // 補正
            alfa = cut == FadeType.CutOut ? 0 : 1;
            panel.SetAlpha(alfa);

            // alfaが0-1の間ループ
            while (true)
            {
                yield return null;

                switch (cut)
                {
                    case FadeType.CutIn:
                        panel.SetAlpha(alfa -= fadingSpeed * Time.unscaledDeltaTime);
                        break;

                    case FadeType.CutOut:
                        panel.SetAlpha(alfa += fadingSpeed * Time.unscaledDeltaTime);
                        break;
                }

                if (alfa >= 1 || alfa <= 0) { break; }
            }

            print("Faded.");
        }

        /// <summary>
        /// 複数回実行しない
        /// </summary>
        /// <param name="fstyle"></param>
        public void Fade(FadeType fstyle) => StartCoroutine(Fader(fstyle));
    }
}

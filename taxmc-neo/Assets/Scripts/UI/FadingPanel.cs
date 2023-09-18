using System;
using System.Collections;
using trrne.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace trrne.Game
{
    public enum FadeStyle { In, Out, InOut }

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

        IEnumerator _Fade(FadeStyle style)
        {
            // 補正
            alfa = style == FadeStyle.Out ? 0 : 1;
            panel.SetAlpha(alfa);

            while (true)
            {
                switch (style)
                {
                    case FadeStyle.In:
                        break;

                    case FadeStyle.Out:
                        alfa += fadingSpeed * Time.deltaTime;
                        break;
                }
            }
        }

        public void Fade(FadeStyle fstyle) => StartCoroutine(_Fade(fstyle));
    }
}

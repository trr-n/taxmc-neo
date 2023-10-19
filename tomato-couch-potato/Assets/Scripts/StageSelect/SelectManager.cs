using DG.Tweening;
using Chickenen.Brain;
using Chickenen.Pancreas;
using UnityEngine;
using UnityEngine.UI;

namespace Chickenen.Arm
{
    public class SelectManager : MonoBehaviour
    {
        [SerializeField]
        Text centerT;

        [SerializeField]
        Sprite[] backs;

        [SerializeField]
        RectTransform[] buttons;

        [SerializeField]
        RectTransform core;

        readonly string prefix = "stage";
        readonly float offset = 18.96f;
        Vector2 init => Vector100.X * offset;

        bool scrolling = false;
        readonly float scrollSpeed = 0.5f;

        float horizon;

        void Start()
        {
            core.position = init;
        }

        void Update()
        {
            centerT.text = CenterButton?.name ?? "null";
            Scroll();
            Transition();
        }

        void Transition()
        {
            if (CenterButton != null && int.TryParse(Typing.Delete(CenterButton.name, prefix), out int idx))
            {
                if (idx <= Recorder.Instance.Done && Inputs.Down(Constant.Keys.Button))
                {
                    print(Constant.Scenes.Prefix + idx);
                    Scenes.Load(Constant.Scenes.Prefix + idx);
                }
            }
        }

        /// <summary>
        /// xが一番0に近いボタンを取得
        /// </summary>
        GameObject CenterButton
        {
            get
            {
                foreach (var button in buttons)
                {
                    if (Maths.CutailedTwins(button.transform.position.x, 0))
                    {
                        return button.gameObject;
                    }
                }
                return null;
            }
        }

        void Scroll()
        {
            horizon = Input.GetAxisRaw(Constant.Keys.Horizontal);
            if (horizon.Twins(0))
            {
                return;
            }

            switch (horizon.Sign())
            {
                case 0:
                    return;

                // D
                case 1:
                    if (CenterButton != buttons[^1].gameObject)
                    {
                        Scroller(core.position.x - offset);
                    }
                    break;

                // A
                case -1:
                    if (CenterButton != buttons[0].gameObject)
                    {
                        Scroller(core.position.x + offset);
                    }
                    break;
            }
        }

        void Scroller(float targetx)
        {
            if (scrolling)
            {
                return;
            }

            scrolling = true;

            core.DOMoveX(targetx, scrollSpeed)
                .SetEase(Ease.InOutCubic)
                .OnComplete(() => scrolling = false);
        }
    }
}
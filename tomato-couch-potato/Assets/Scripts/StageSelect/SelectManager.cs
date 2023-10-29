using DG.Tweening;
using trrne.Brain;
using trrne.Pancreas;
using UnityEngine;
using UnityEngine.UI;

namespace trrne.Arm
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

        bool scrolling = false;
        readonly float scrollSpeed = 0.5f;

        float horizon;

        void Start()
        {
            core.position = Vector100.X * offset;
        }

        void Update()
        {
            centerT.text = CenterButton() != null ? CenterButton().name : null ?? "null";
            Scroll();
            Transition();
        }

        void Transition()
        {
            if (CenterButton() != null &&
                int.TryParse(Typing.Delete(CenterButton().name, prefix),
                out int idx)
            )
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
        GameObject CenterButton()
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

        void Scroll()
        {
            horizon = Input.GetAxisRaw(Constant.Keys.Horizontal);
            if (horizon.Twins(0))
            {
                return;
            }

            switch (horizon.Sign())
            {
                // right
                case 1:
                    if (CenterButton() != buttons[^1].gameObject)
                    {
                        Scroller(core.position.x - offset);
                    }
                    break;

                // left
                case -1:
                    if (CenterButton() != buttons[0].gameObject)
                    {
                        Scroller(core.position.x + offset);
                    }
                    break;
            }
        }

        void Scroller(float targetX)
        {
            if (scrolling)
            {
                return;
            }

            scrolling = true;

            core.DOMoveX(targetX, scrollSpeed)
                .SetEase(Ease.InOutCubic)
                .OnComplete(() => scrolling = false);
        }
    }
}
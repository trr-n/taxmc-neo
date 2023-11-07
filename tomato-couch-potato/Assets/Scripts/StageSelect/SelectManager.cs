using DG.Tweening;
using trrne.Brain;
using trrne.Box;
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

        const string prefix = "stage";
        const float offset = 18.96f;

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
            if ((horizon = Input.GetAxisRaw(Constant.Keys.Horizontal)).Twins(0))
            {
                return;
            }

            if (horizon.Sign(1))
            {
                Shorthand.BoolAction(CenterButton() != buttons[^1].gameObject,
                    () => Scroller(core.position.x - offset));
            }
            else
            {
                Shorthand.BoolAction(CenterButton() != buttons[0].gameObject,
                    () => Scroller(core.position.x + offset));
            }

            // if (horizon.Sign(1))
            // {
            //     Shorthand.BoolAction(CenterButton() != buttons[^1].gameObject,
            //         () => Scroller(core.position.x - offset));
            // }
            // else
            // {
            //     Shorthand.BoolAction(CenterButton() != buttons[0].gameObject,
            //         () => Scroller(core.position.x + offset));
            // }
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
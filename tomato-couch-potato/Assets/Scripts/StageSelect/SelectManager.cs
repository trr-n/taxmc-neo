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

        const string STAGE_NAME_PREFIX = "stage";
        const float BETWEEN_SPACE = 18.96f;

        bool isScrolling = false;
        const float BUTTON_SCROLL_SPEED = 0.5f;

        float horizon;

        void Start()
        {
            core.SetPosition(x: BETWEEN_SPACE);
        }

        void Update()
        {
#if DEBUG
            centerT.text = CenterButton() != null ? CenterButton().name : "null";
#endif
            Scroll();
            Transition();
        }

        void Transition()
        {
            if (CenterButton() != null && Inputs.Down(Constant.Keys.BUTTON))
            {
                string removedSceneIndex = CenterButton().name.ToLower().Delete(STAGE_NAME_PREFIX);
                if (int.TryParse(removedSceneIndex, out int index))
                {
                    Scenes.Load(Constant.Scenes.PREFIX + index);
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
                if (button.transform.position.x.CutailedTwins())
                {
                    return button.gameObject;
                }
            }
            return null;
        }

        void Scroll()
        {
            if (0f.Twins(horizon = Input.GetAxisRaw(Constant.Keys.HORIZONTAL)))
            {
                return;
            }

            switch (horizon.Sign())
            {
                case 1:
                    if (CenterButton() != buttons[^1].gameObject)
                    {
                        Scroller(core.position.x - BETWEEN_SPACE);
                    }
                    break;
                default:
                    if (CenterButton() != buttons[0].gameObject)
                    {
                        Scroller(core.position.x + BETWEEN_SPACE);
                    }
                    break;
            }
        }

        void Scroller(in float x)
        {
            if (isScrolling)
            {
                return;
            }

            isScrolling = true;

            core.DOMoveX(x, BUTTON_SCROLL_SPEED)
                .SetEase(Ease.InOutCubic)
                .OnComplete(() => isScrolling = false);
        }
    }
}
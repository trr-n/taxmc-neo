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

        const string PREFAB_PREFIX = "stage";
        const float OFFSET = 18.96f;

        bool isScrolling = false;
        const float BUTTON_SCROLL_SPEED = 0.5f;

        float horizon;

        void Start()
        {
            core.SetPosition(Vec.X * OFFSET);
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
            var removed = CenterButton().name.Delete(PREFAB_PREFIX);
            if (CenterButton() != null
                && int.TryParse(removed, out int idx)
                && idx <= Recorder.Instance.Done
                && Inputs.Down(Config.Keys.Button)
            )
            {
                Scenes.Load(Config.Scenes.Prefix + idx);
            }
        }

        /// <summary>
        /// xが一番0に近いボタンを取得
        /// </summary>
        GameObject CenterButton()
        {
            foreach (var button in buttons)
            {
                if (Maths.CutailedTwins(button.transform.position.x, 0f))
                {
                    return button.gameObject;
                }
            }
            return null;
        }

        void Scroll()
        {
            if ((horizon = Input.GetAxisRaw(Config.Keys.Horizontal)).Twins(0f))
            {
                return;
            }

            switch (horizon.Sign())
            {
                case 1:
                    if (CenterButton() != buttons[^1].gameObject)
                    {
                        Scroller(core.position.x - OFFSET);
                    }
                    break;
                default:
                    if (CenterButton() != buttons[0].gameObject)
                    {
                        Scroller(core.position.x + OFFSET);
                    }
                    break;
            }
        }

        void Scroller(float x)
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
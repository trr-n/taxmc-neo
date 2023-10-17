using DG.Tweening;
using trrne.Teeth;
using UnityEngine;
using UnityEngine.UI;

namespace trrne.Arm
{
    public class SelectManager : MonoBehaviour
    {
        [SerializeField]
        Text centerT;

        [SerializeField]
        RectTransform[] buttonRTs;

        [SerializeField]
        RectTransform core;

        const float Offset = 18.96f;
        readonly Vector2[] points = new Vector2[] {
            Vector100.x * Offset,
            Vector100.zero,
            Vector100.x * -Offset
        };

        bool isScrolling = false;
        readonly float scrollSpeed = 0.5f;

        float horizon;

        void Start()
        {
            core.position = points[0];
        }

        void Update()
        {
            centerT.text = GetCenterButton() != null ? GetCenterButton().name : "null";

            Scroll();
        }

        /// <summary>
        /// xが一番0に近いボタンを取得
        /// </summary>
        GameObject GetCenterButton()
        {
            foreach (var button in buttonRTs)
            {
                if (Maths.Twins(Maths.Cutail(button.transform.position.x), 0))
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
                // D
                case 1:
                    if (GetCenterButton() != buttonRTs[^1].gameObject)
                    {
                        Scroller(core.position.x - Offset);
                    }
                    break;

                // A
                case -1:
                    if (GetCenterButton() != buttonRTs[0].gameObject)
                    {
                        Scroller(core.position.x + Offset);
                    }
                    break;
            }
        }

        void Scroller(float targetx)
        {
            if (isScrolling)
            {
                return;
            }

            isScrolling = true;

            core.DOMoveX(targetx, scrollSpeed)
                .SetEase(Ease.InOutCubic)
                .OnComplete(() => isScrolling = false);
        }
    }
}
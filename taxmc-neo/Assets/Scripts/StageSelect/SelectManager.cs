using trrne.WisdomTeeth;
using UnityEngine;

namespace trrne.Arm
{
    public class SelectManager : MonoBehaviour
    {
        [SerializeField]
        GameObject[] buttons;
        RectTransform core;

        Vector3 xy0 = new(1, 0, 0);
        // int[] points => new int[] { 2048, 0, -2048 };
        Vector3[] points => new Vector3[] { Vector100.x * 2048, Vector100.x * 0, Vector100.x * -2048 };

        void Start()
        {
            core = buttons[0].transform.parent.GetComponent<RectTransform>();
            core.position = points[0];
        }

        void Update()
        {
            print($"t: {core.transform.position}, rt: {core.position}");
        }

        void Side()
        {
        }

        /// <summary>
        /// 中央(xが0)のボタンを取得
        /// </summary>
        GameObject center
        {
            get
            {
                foreach (var button in buttons)
                {
                    if (button.GetComponent<RectTransform>().position.x == 0)
                    {
                        return button;
                    }
                }
                return null;
            }
        }
    }
}
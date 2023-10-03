using System;
using trrne.Bag;
using UnityEngine;
using UnityEngine.UI;

namespace trrne.Body
{
    [ExecuteAlways]
    public class Messages : MonoBehaviour
    {
        [Serializable]
        struct Pair
        {
            public Text text;
            public RectTransform rtransform;
            public Transform transform;
            public string message;
        }

        [SerializeField]
        Pair[] pairs;

        void Start()
        {
            foreach (var pair in pairs)
            {
                pair.text.TextSettings(TextAnchor.MiddleCenter, VerticalWrapMode.Overflow, HorizontalWrapMode.Overflow);
                pair.text.SetText(pair.message);
                pair.rtransform.transform.position = pair.transform.position;
            }
        }
    }
}

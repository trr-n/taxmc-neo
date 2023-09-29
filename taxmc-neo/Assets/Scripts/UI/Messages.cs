using System;
using System.Collections;
using System.Collections.Generic;
using trrne.Bag;
using UnityEngine;
using UnityEngine.UI;

namespace trrne.Body
{
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
        Pair venom;

        void Start()
        {
            // textの設定
            Set(venom);
        }

        void LateUpdate()
        {
            // venom
            venom.text.SetText(venom.message);
            venom.rtransform.transform.position = venom.transform.position;
        }

        void Set(params Pair[] pairs)
        => Array.ForEach(pairs, pair => pair.text.TextSettings(TextAnchor.MiddleCenter, VerticalWrapMode.Overflow, HorizontalWrapMode.Overflow));
    }
}

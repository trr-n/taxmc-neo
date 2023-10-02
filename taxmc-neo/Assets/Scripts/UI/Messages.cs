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
        Pair venom, flag;

        void Start()
        {
            // textの設定
            Set(venom, flag);
        }

        void LateUpdate()
        {
            // venom
            venom.text.SetText(venom.message);
            venom.rtransform.transform.position = venom.transform.position;

            flag.text.SetText(flag.message);
            flag.rtransform.transform.position = flag.transform.position;
        }

        void Set(params Pair[] pairs) => pairs.ForEach(pair => pair.text.TextSettings(TextAnchor.MiddleCenter, VerticalWrapMode.Overflow, HorizontalWrapMode.Overflow));
    }
}

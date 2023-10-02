using System.Collections;
using System.Collections.Generic;
using trrne.Bag;
using UnityEngine;

namespace trrne.Body
{
    public class Button : Objectt
    {
        [SerializeField]
        GameObject[] targetObjs;

        [SerializeField]
        float duration = 2;

        readonly Stopwatch effectiveSW = new();
        ButtonEnableFlag enable;

        bool pressing = false;

        protected override void Start()
        {
            animatable = false;
            base.Start();

            sr.sprite = sprites[0];

            enable = transform.GetFromChild<ButtonEnableFlag>(0);
        }

        protected override void Behavior()
        {
            // 押されていない、プレイヤーが範囲内にいる、キーが押された
            if (!pressing && enable.Hit && Inputs.Down(Constant.Keys.Button))
            {
                pressing = true;
                sr.sprite = sprites[0];

                effectiveSW.Restart();
                targetObjs.ForEach(obj => obj.SetActive(!obj.activeSelf));
            }

            // 押されている、効果時間がduration以上
            if (pressing && effectiveSW.sf >= duration)
            {
                effectiveSW.Reset();
                targetObjs.ForEach(obj => obj.SetActive(!obj.activeSelf));

                pressing = false;
                sr.sprite = sprites[1];
            }
        }
    }
}

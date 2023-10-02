using trrne.Bag;
using UnityEngine;

namespace trrne.Body
{
    public class Lever : Objectt
    {
        [SerializeField]
        GameObject[] targetObjs;

        [SerializeField]
        float duration = 2;

        readonly Stopwatch effectiveSW = new();
        LeverFlag enable;

        bool pressing = false;

        protected override void Start()
        {
            base.Start();
            animatable = false;
            enable = transform.GetFromChild<LeverFlag>(0);

#if !DEBUG
            sr.color = Colour.transparent;
#endif
        }

        protected override void Behavior()
        {
            // 押されていない、プレイヤーが範囲内にいる、キーが押された
            if (!pressing && enable.Hit && Inputs.Down(Constant.Keys.Button))
            {
                pressing = true;
                sr.sprite = sprites[0];

                source.clip = sounds.Choice();

                effectiveSW.Restart();
                targetObjs.ForEach(obj => obj.SetActive(!obj.activeSelf));
            }

            // 押されている、効果時間がduration以上
            if (pressing && effectiveSW.sf >= duration)
            {
                effectiveSW.Reset();
                targetObjs.ForEach(obj => obj.SetActive(!obj.activeSelf));

                source.clip = sounds.Choice();

                pressing = false;
                sr.sprite = sprites[1];
            }
        }
    }
}

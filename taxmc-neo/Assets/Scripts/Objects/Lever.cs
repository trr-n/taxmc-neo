using trrne.WisdomTeeth;
using UnityEngine;

namespace trrne.Body
{
    public class Lever : Objectt
    {
        [SerializeField]
        GameObject[] targetObjs;

        [SerializeField]
        float duration = 2;

        [SerializeField]
        AudioClip[] sounds;

        AudioSource speaker;

        readonly Stopwatch effectiveSW = new();
        LeverFlag enable;

        bool pressing = false;

        protected override void Start()
        {
            base.Start();
            animatable = false;
            enable = transform.GetFromChild<LeverFlag>(0);

            speaker = GetComponent<AudioSource>();

#if !DEBUG
            sr.color = Colour.transparent;
#endif
        }

        protected override void Behavior()
        {
            // レバーが動作中じゃない、プレイヤーが範囲内にいる、キーが押された
            if (!pressing && enable.Hit && Inputs.Down(Constant.Keys.Button))
            {
                pressing = true;
                sr.sprite = sprites[0];

                speaker.clip = sounds.Choice();

                effectiveSW.Restart();
                targetObjs.ForEach(obj => obj.SetActive(!obj.activeSelf));
            }

            // 動作中、効果時間がduration以上
            if (pressing && effectiveSW.sf >= duration)
            {
                effectiveSW.Reset();
                targetObjs.ForEach(obj => obj.SetActive(!obj.activeSelf));

                speaker.clip = sounds.Choice();

                pressing = false;
                sr.sprite = sprites[1];
            }
        }
    }
}

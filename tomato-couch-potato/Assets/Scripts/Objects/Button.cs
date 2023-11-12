using trrne.Box;
using UnityEngine;

namespace trrne.Core
{
    public class Button : Object
    {
        [SerializeField]
        GameObject[] gimmicks;

        [SerializeField]
        float duration = 2;

        [SerializeField]
        AudioClip[] sounds;

        AudioSource speaker;

        readonly Stopwatch effectiveSW = new();
        ButtonFlag flag;

        bool isPressing = false;

        readonly (int active, int inactive) status = (0, 1);

        protected override void Start()
        {
            base.Start();
            Animate = false;

            flag = transform.GetFromChild<ButtonFlag>(0);

            speaker = Gobject.GetWithTag<AudioSource>(Constant.Tags.Manager);

#if !DEBUG
            sr.color = Colour.transparent;
#endif
        }

        protected override void Behavior()
        {
            // レバーが動作中じゃない、プレイヤーが範囲内にいる、キーが押された
            if (!isPressing && flag.Hit && Inputs.Down(Constant.Keys.Button))
            {
                isPressing = true;
                sr.sprite = sprites[status.active];

                speaker.TryPlay(sounds.Choice());
                effectiveSW.Restart();
                gimmicks.ForEach(g => g.GetComponent<IGimmick>().On());
            }

            // 動作中、効果時間がduration以上
            if (isPressing && effectiveSW.Sf >= duration)
            {
                effectiveSW.Reset();
                gimmicks.ForEach(g => g.GetComponent<IGimmick>().Off());
                speaker.TryPlay(sounds.Choice());

                isPressing = false;
                sr.sprite = sprites[status.inactive];
            }
        }
    }
}

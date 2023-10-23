using Chickenen.Pancreas;
using UnityEngine;

namespace Chickenen.Heart
{
    public class Lever : Object
    {
        [SerializeField]
        GameObject[] gimmicks;

        [SerializeField]
        float duration = 2;

        [SerializeField]
        AudioClip[] sounds;

        AudioSource speaker;

        readonly Stopwatch effectiveSW = new();
        LeverFlag enable;

        bool pressing = false;

        readonly (int active, int inactive) status = (0, 1);

        protected override void Start()
        {
            base.Start();
            Animate = false;
            enable = transform.GetFromChild<LeverFlag>(0);

            speaker = Gobject.GetWithTag<AudioSource>(Constant.Tags.Manager);

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

                sr.sprite = sprites[status.active];
                // speaker.clip = sounds.Choice();
                speaker.TryPlay(sounds.Choice());

                effectiveSW.Restart();

                foreach (var gimmick in gimmicks)
                {
                    if (gimmick.TryGetComponent(out IUsable enable))
                    {
                        enable.Active();
                    }
                }
            }

            // 動作中、効果時間がduration以上
            if (pressing && effectiveSW.Sf >= duration)
            {
                effectiveSW.Reset();

                foreach (var gimmick in gimmicks)
                {
                    if (gimmick.TryGetComponent(out IUsable disable))
                    {
                        disable.Inactive();
                    }
                }

                // speaker.clip = sounds.Choice();
                speaker.TryPlay(sounds.Choice());

                pressing = false;
                sr.sprite = sprites[status.inactive];
            }
        }
    }
}

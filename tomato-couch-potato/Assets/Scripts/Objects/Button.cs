using trrne.Box;
using UnityEngine;

namespace trrne.Core
{
    public class Button : Object
    {
        [SerializeField]
        Gimmick[] gimmicks;

        [SerializeField]
        float duration = 2;

        [SerializeField]
        AudioClip[] sounds;

        AudioSource speaker;

        readonly Stopwatch effectiveSW = new();
        ButtonFlag flag;

        bool isPressing = false;

        const int ON = 0, OFF = 1;

        protected override void Start()
        {
            base.Start();

            isAnimate = false;
            flag = transform.GetFromChild<ButtonFlag>();
            speaker = Gobject.GetWithTag<AudioSource>(Config.Tags.MANAGER);
#if !DEBUG
            sr.color = Surface.Transparent;
#endif
        }

        protected override void Behavior()
        {
            // レバーが動作中じゃない、プレイヤーが範囲内にいる、キーが押された
            if (!isPressing && flag.IsHit && Inputs.Down(Config.Keys.BUTTON))
            {
                isPressing = true;
                sr.sprite = sprites[ON];
                speaker.TryPlayOneShot(sounds.Choice());
                effectiveSW.Restart();

                if (gimmicks.Length >= 1)
                {
                    gimmicks.ForEach(g => g.GetComponent<IGimmick>().On());
                }
            }

            // 動作中、効果時間がduration以上
            if (isPressing && effectiveSW.Sf() >= duration)
            {
                effectiveSW.Reset();

                if (gimmicks.Length >= 1)
                {
                    gimmicks.ForEach(g => g.GetComponent<IGimmick>().Off());
                }

                speaker.TryPlayOneShot(sounds.Choice());
                isPressing = false;
                sr.sprite = sprites[OFF];
            }
        }
    }
}

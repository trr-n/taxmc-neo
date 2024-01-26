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

        readonly Stopwatch effectiveSw = new();
        ButtonFlag flag;

        bool isPressing = false;
        const int ON = 0, OFF = 1;

        protected override void Start()
        {
            base.Start();

            isAnimate = false;
            flag = transform.GetFromChild<ButtonFlag>();
        }

        protected override void Behavior()
        {
            if (MF.ZeroTwins(Time.timeScale))
            {
                return;
            }

            // レバーが動作中じゃない、プレイヤーが範囲内にいる、キーが押された
            if (!isPressing && flag.IsHit && Inputs.Down(Constant.Keys.BUTTON))
            {
                isPressing = true;
                sr.sprite = sprites[ON];

                speaker.PlayOneShot(sounds.Choice());

                if (gimmicks.Length >= 1)
                {
                    gimmicks.ForEach(g => g.GetComponent<IGimmick>().On());
                }

                effectiveSw.Restart();
            }

            // 動作中、効果時間がduration以上
            if (isPressing && effectiveSw.sf >= duration)
            {
                effectiveSw.Reset();

                if (gimmicks.Length >= 1)
                {
                    gimmicks.ForEach(g => g.GetComponent<IGimmick>().Off());
                }

                speaker.PlayOneShot(sounds.Choice());

                isPressing = false;
                sr.sprite = sprites[OFF];
            }
        }
    }
}

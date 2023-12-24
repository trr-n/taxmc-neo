using trrne.Box;
using trrne.Brain;
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

        readonly Stopwatch effectiveSW = new();
        ButtonFlag flag;

        bool isPressing = false;

        const int ON = 0;
        const int OFF = 1;

        Transform player;

        protected override void Start()
        {
            base.Start();

            isAnimate = false;
            flag = transform.GetFromChild<ButtonFlag>();

            player = Gobject.GetWithTag<Transform>(Config.Tags.PLAYER);
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
                float distance = Vector2.Distance(transform.position, player.position);
                Recorder.Instance.PlayOneShot(sounds.Choice(), 1 / (distance + 1));
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

                var distance = Vector2.Distance(transform.position, player.position);
                Recorder.Instance.PlayOneShot(sounds.Choice(), 1 / (distance + 1));
                isPressing = false;
                sr.sprite = sprites[OFF];
            }
        }
    }
}

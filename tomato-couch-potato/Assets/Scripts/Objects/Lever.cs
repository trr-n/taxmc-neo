using trrne.Pancreas;
using UnityEngine;

namespace trrne.Heart
{
    public class Lever : Object
    {
        [SerializeField]
        GameObject[] gimmicks;

        [SerializeField]
        AudioClip[] sounds;

        AudioSource source;
        bool isActive = false;

        LeverFlag flag;

        protected override void Start()
        {
            base.Start();

            flag = transform.GetFromChild<LeverFlag>();
            source = Gobject.GetWithTag<AudioSource>(Constant.Tags.Manager);
            sr.sprite = sprites[isActive ? 0 : 1];
        }

        protected override void Behavior()
        {
            if (!flag.Hit)
            {
                return;
            }

            // active
            if (isActive && Inputs.Down(Constant.Keys.Button))
            {
                source.TryPlay(sounds.Choice());
                sr.sprite = sprites[1];

                foreach (var gimmick in gimmicks)
                {
                    if (gimmick.TryGet(out IUsable usable))
                    {
                        usable.Active();
                    }
                }
                isActive = false;
            }

            // inactive
            else if (!isActive && Inputs.Down(Constant.Keys.Button))
            {
                source.TryPlay(sounds.Choice());
                sr.sprite = sprites[0];

                foreach (var gimmick in gimmicks)
                {
                    if (gimmick.TryGet(out IUsable usable))
                    {
                        usable.Inactive();
                    }
                }
                isActive = true;
            }
        }
    }
}

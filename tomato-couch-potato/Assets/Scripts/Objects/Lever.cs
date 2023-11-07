using trrne.Box;
using UnityEngine;

namespace trrne.Core
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
                gimmicks.ForEach(gim => gim.TryGet(out IGimmick g).BoolAction(g.Active));
                isActive = false;
            }

            // inactive
            else if (!isActive && Inputs.Down(Constant.Keys.Button))
            {
                source.TryPlay(sounds.Choice());
                sr.sprite = sprites[0];
                gimmicks.ForEach(gim => gim.TryGet(out IGimmick g).BoolAction(g.Inactive));
                isActive = true;
            }
        }
    }
}

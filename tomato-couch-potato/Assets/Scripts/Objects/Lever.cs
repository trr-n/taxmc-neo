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

        bool isActive = false;

        LeverFlag flag;

        protected override void Start()
        {
            base.Start();

            flag = transform.GetFromChild<LeverFlag>();
            sr.sprite = sprites[isActive ? 0 : 1];
        }

        protected override void Behavior()
        {
            if (!flag.IsHitting && Inputs.Down(Constant.Keys.BUTTON))
            {
                return;
            }

            if (isActive)
            {
                PlayOneShot(sounds.Choice());
                sr.sprite = sprites[1];
                gimmicks.ForEach(gimmick => gimmick.TryGetComponent(out IGimmick g).If(g.On));
                isActive = false;
            }
            else
            {
                PlayOneShot(sounds.Choice());
                sr.sprite = sprites[0];
                gimmicks.ForEach(gimmick => gimmick.TryGetComponent(out IGimmick g).If(g.Off));
                isActive = true;
            }
        }
    }
}

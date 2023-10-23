using Chickenen.Pancreas;
using UnityEngine;

namespace Chickenen.Heart
{
    public class Lever : Object
    {
        [SerializeField]
        GameObject[] gimmicks;

        bool isActive = false;

        protected override void Start()
        {
            base.Start();
            sr.sprite = sprites[0];
        }

        protected override void Behavior()
        {
            // active
            if (isActive && Inputs.Down(Constant.Keys.Button))
            {
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

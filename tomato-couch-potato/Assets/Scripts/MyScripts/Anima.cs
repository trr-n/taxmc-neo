using System;
using UnityEngine;

namespace trrne.Box
{
    public sealed class Anima
    {
        (int i, Stopwatch sw) colour, sprite;

        public void Colour(
            bool enable,
            SpriteRenderer sr,
            float interval,
            params Color[] colours
        )
        {
            if (enable && colour.sw.sf >= interval)
            {
                sprite.sw.Reset();
                colour.i = colour.i >= colours.Length - 1 ? colour.i = 0 : colour.i += 1;
                sr.color = colours[colour.i];
                colour.sw.Restart();
            }
        }

        public void Sprite(
            bool enable,
            SpriteRenderer sr,
            float interval,
            params Sprite[] pics
        )
        {
            if (enable && sprite.sw.sf >= interval)
            {
                sprite.sw.Reset();
                sprite.i = sprite.i >= pics.Length - 1 ? 0 : sprite.i += 1;
                sr.sprite = pics[sprite.i];
                sprite.sw.Restart();
            }
        }
    }

    public static class Anima2
    {
        [Obsolete]
        public static float Length(this Animator animator)
        => animator.GetNextAnimatorStateInfo(0).length;
    }
}

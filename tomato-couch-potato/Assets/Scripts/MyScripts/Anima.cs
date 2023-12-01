using System;
using UnityEngine;

namespace trrne.Box
{
    public class Anima
    {
        (int idx, Stopwatch sw) colour = (0, new(true)), sprite = (0, new(true));

        public void Colour(bool enable, SpriteRenderer sr, in float interval, params Color[] colours)
        {
            if (enable && colour.sw.Sf() >= interval)
            {
                sprite.sw.Reset();
                colour.idx = colour.idx >= colours.Length - 1 ? colour.idx = 0 : colour.idx += 1;
                sr.color = colours[colour.idx];
                colour.sw.Restart();
            }
        }

        public void Sprite(bool enable, SpriteRenderer sr, in float interval, params Sprite[] pics)
        {
            if (enable && sprite.sw.S() >= interval)
            {
                sprite.sw.Reset();
                sprite.idx = sprite.idx >= pics.Length - 1 ? 0 : sprite.idx += 1;
                sr.sprite = pics[sprite.idx];
                sprite.sw.Restart();
            }
        }
    }

    public static class Anima2
    {
        [Obsolete] public static float Length(this Animator animator) => animator.GetNextAnimatorStateInfo(0).length;
    }
}

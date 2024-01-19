using System;
using UnityEngine;

namespace trrne.Box
{
    public static class Anima
    {
        static (int i, Stopwatch sw) colour = new(0, new()), sprite = new(0, new());

        public static void Animation(this SpriteRenderer sr, bool enable, float interval, params Color[] colours)
        {
            if (enable && colour.sw.sf >= interval)
            {
                sprite.sw.Reset();
                colour.i = colour.i >= colours.Length - 1 ? colour.i = 0 : ++colour.i;
                sr.color = colours[colour.i];
                colour.sw.Restart();
            }
        }

        public static void Animation(this SpriteRenderer sr, bool enable, float interval, params Sprite[] pics)
        {
            if (enable && sprite.sw.sf >= interval)
            {
                sprite.sw.Reset();
                sprite.i = sprite.i >= pics.Length - 1 ? 0 : ++sprite.i;
                sr.sprite = pics[sprite.i];
                sprite.sw.Restart();
            }
        }
    }

    public static class Anima2
    {
        [Obsolete]
        public static float Length(this Animator animator) => animator.GetNextAnimatorStateInfo(0).length;
    }
}

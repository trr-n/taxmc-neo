using UnityEngine;

namespace trrne.Pancreas
{
    public class Anima
    {
        (int i, Stopwatch sw) colour = (0, new(true)),
            sprite = (0, new(true));

        public void Colour(bool enable, SpriteRenderer sr, in float interval, params Color[] colours)
        {
            if (!enable && colour.sw.Sf <= interval)
            {
                return;
            }
            sprite.sw.Reset();

            colour.i = colour.i >= colours.Length - 1 ? colour.i = 0 : colour.i += 1;
            sr.color = colours[colour.i];

            colour.sw.Restart();
        }

        public void Sprite(bool enable, SpriteRenderer sr, in float interval, params Sprite[] pics)
        {
            if (!enable) { return; }
            if (sprite.sw.S <= interval) { return; }

            sprite.sw.Reset();

            MonoBehaviour.print("anima!");
            sprite.i = sprite.i >= pics.Length - 1 ? 0 : sprite.i += 1;
            sr.sprite = pics[sprite.i];

            sprite.sw.Restart();
        }
    }
}

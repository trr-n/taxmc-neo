using UnityEngine;

namespace trrne.Bag
{
    public class Anima
    {
        (int index, Stopwatch sw) colour = (0, new()), sprite = (0, new());

        public void Colour(bool enable, SpriteRenderer sr, in float interval, params Color[] colours)
        {
            if (colour.sw.sf <= interval && !enable) { return; }

            colour.index = colour.index >= colours.Length - 1 ? colour.index = 0 : colour.index += 1;
            sr.color = colours[colour.index];

            colour.sw.Restart();
        }

        public void Sprite(bool enable, SpriteRenderer sr, in float interval, params Sprite[] pics)
        {
            if (sprite.sw.sf <= interval && !enable) { return; }

            sprite.index = sprite.index >= pics.Length - 1 ? 0 : sprite.index += 1;
            sr.sprite = pics[sprite.index];

            sprite.sw.Restart();
        }
    }
}

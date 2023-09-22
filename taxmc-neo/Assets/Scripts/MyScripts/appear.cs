using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

namespace trrne.utils
{

    public static class Appear
    {
        public static bool Compare(this SpriteRenderer sr, Sprite sprite) => sr.sprite == sprite;

        public static void SetSprite(this SpriteRenderer sr, Sprite sprite) => sr.sprite = sprite;
        public static void SetSprite(this SpriteRenderer sr, Sprite[] sprites) => sr.sprite = sprites.Choice();

        public static Vector2 GetSpriteSize(this SpriteRenderer sr) => sr.bounds.size;

        public static string SetText(this Text text, object obj) => text.text = obj.ToString();
    }

    public class Anima
    {
        (int index, Stopwatch sw) colour, picture;

        int cindex = 0;
        readonly Stopwatch colourSW = new(true);
        public void Colour(SpriteRenderer sr, in float interval, params Color[] colours)
        {
            if (!(colourSW.sf >= interval)) { return; }

            cindex = cindex >= colours.Length - 1 ? cindex = 0 : cindex += 1;
            sr.color = colours[cindex];

            colourSW.Restart();
        }

        int iindex = 0;
        readonly Stopwatch isw = new(true);
        public void Picture(SpriteRenderer sr, in float interval, params Sprite[] pics)
        {
            if (isw.sf >= interval) { return; }

            iindex = iindex >= pics.Length - 1 ? 0 : iindex += 1;
            sr.sprite = pics[iindex];

            isw.Restart();
        }
    }
}

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Self.Utils
{

    public static class Appear
    {
        public static bool Compare(this SpriteRenderer sr, Sprite sprite) => sr.sprite == sprite;

        public static void SetSprite(this SpriteRenderer sr, Sprite sprite) => sr.sprite = sprite;
        public static void SetSprite(this SpriteRenderer sr, Sprite[] sprites) => sr.sprite = sprites.Choice3();

        public static Vector2 GetSpriteSize(this SpriteRenderer sr) => sr.bounds.size;

        public static string SetText(this Text text, object obj) => text.text = obj.ToString();
    }

    public static class Animator
    {
        static int cindex = 0;
        static readonly Stopwatch csw = new(true);
        public static void Colour(this SpriteRenderer sr, in float span, params Color[] colours)
        {
            if (!(csw.sf >= span))
                return;

            // index = colors[index > colors.Length ? index = 0 : index++];
            cindex = cindex >= colours.Length - 1 ? cindex = 0 : cindex += 1;
            sr.color = colours[cindex];
            csw.Restart();
        }

        static int iindex = 0;
        static readonly Stopwatch isw = new(true);
        public static void Image(this SpriteRenderer sr, in float span, params Sprite[] sprites)
        {
            if (isw.sf >= span)
                return;

            iindex = iindex >= sprites.Length - 1 ? iindex = 0 : iindex += 1;
            sr.sprite = sprites[iindex];
            isw.Restart();
        }
    }
}

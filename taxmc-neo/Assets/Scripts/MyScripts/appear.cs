using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

namespace trrne.Bag
{
    public static class Appear
    {
        public static bool Compare(this SpriteRenderer sr, Sprite sprite) => sr.sprite == sprite;

        public static void SetSprite(this SpriteRenderer sr, Sprite sprite) => sr.sprite = sprite;
        public static void SetSprite(this SpriteRenderer sr, Sprite[] sprites) => sr.sprite = sprites.Choice();

        public static Vector2 GetSpriteSize(this SpriteRenderer sr) => sr.bounds.size;

        public static string SetText(this Text text, object obj) => text.text = obj.ToString();
        public static void SetTextSize(this Text text, int size) => text.fontSize = size;

        public static void TextSettings(this Text text, TextAnchor anchor = TextAnchor.UpperLeft,
        VerticalWrapMode vWrap = VerticalWrapMode.Truncate, HorizontalWrapMode hWrap = HorizontalWrapMode.Wrap)
        {
            text.alignment = anchor;
            text.verticalOverflow = vWrap;
            text.horizontalOverflow = hWrap;
        }
    }
}

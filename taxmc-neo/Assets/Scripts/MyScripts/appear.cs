using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

namespace trrne.Appendix
{
    public static class Appear
    {
        public static bool Compare(this SpriteRenderer sr, Sprite sprite) => sr.sprite == sprite;

        public static void SetSprite(this SpriteRenderer sr, Sprite sprite) => sr.sprite = sprite;
        public static void SetSprite(this SpriteRenderer sr, Sprite[] sprites) => sr.sprite = sprites.Choice();

        public static Vector2 GetSpriteSize(this SpriteRenderer sr) => sr.bounds.size;

        public static string SetText(this Text text, object obj) => text.text = obj.ToString();
    }
}

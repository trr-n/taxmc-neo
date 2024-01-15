using UnityEngine;
using UnityEngine.UI;

namespace trrne.Box
{
    public static class Surface
    {
        public static Vector2 Size(this Collider2D collider) => collider.bounds.size;
        public static Vector2 Size(this SpriteRenderer sr) => sr.bounds.size;

        public static bool CompareSprite(this SpriteRenderer sr, Sprite sprite) => sr.sprite == sprite;

        public static void SetSprite(this SpriteRenderer sr, Sprite sprite) => sr.sprite = sprite;
        public static void SetSprite(this SpriteRenderer sr, Sprite[] sprites) => sr.sprite = sprites.Choice();

        public static Vector2 SpriteSize(this SpriteRenderer sr) => sr.bounds.size;

        public static void SetColor(this Image image, Color color) => image.color = color;

        public static string SetText(this Text text, object obj) => text.text = obj.ToString();
        public static void SetTextSize(this Text text, int size) => text.fontSize = size;

        public static void TextSettings(this Text text, TextAnchor anchor, VerticalWrapMode vw, HorizontalWrapMode hw)
        {
            text.alignment = anchor;
            text.verticalOverflow = vw;
            text.horizontalOverflow = hw;
        }

        public static Color Transparent => new(0, 0, 0, 0);
        public static Color Gaming => Color.HSVToRGB(Time.unscaledTime % 1, 1, 1);

        public static void SetAlpha(this Image image, float alpha = 0) => image.color = new(image.color.r, image.color.g, image.color.b, alpha);
        public static void SetAlpha(this SpriteRenderer sr, float alpha = 0) => sr.color = new(sr.color.r, sr.color.g, sr.color.b, alpha);
        public static float Alpha(this Image image) => image.color.a;
        public static float Alpha(this SpriteRenderer sr) => sr.color.a;

        public static void SetColor(this SpriteRenderer sr, Color color) => sr.color = color;
        public static Color SetColor(this Color color, float? red = null, float? green = null, float? blue = null, float? alpha = null)
        => new(red ?? color.r, green ?? color.g, blue ?? color.b, alpha ?? color.a);

        public static bool Twins(in Color n1, Color n2) => Mathf.Approximately(n1.r, n2.r) && Mathf.Approximately(n1.g, n2.g) && Mathf.Approximately(n1.b, n2.b);
    }
}

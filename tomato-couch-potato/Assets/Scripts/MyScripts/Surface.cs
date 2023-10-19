using UnityEngine;
using UnityEngine.UI;

namespace Chickenen.Pancreas
{
    public static class Surface
    {
        public static Vector2 GetSize(this Collider2D collider)
        {
            return collider.bounds.size;
        }

        public static Vector2 GetSize(this SpriteRenderer sr)
        {
            return sr.bounds.size;
        }

        public static bool CompareSprite(this SpriteRenderer sr, Sprite sprite)
        {
            return sr.sprite == sprite;
        }

        public static void SetSprite(this SpriteRenderer sr, Sprite sprite)
        {
            sr.sprite = sprite;
        }

        public static void SetSprite(this SpriteRenderer sr, Sprite[] sprites)
        {
            sr.sprite = sprites.Choice();
        }

        public static void SetColor(this Image image, Color color)
        {
            image.color = color;
        }

        public static Vector2 GetSpriteSize(this SpriteRenderer sr)
        {
            return sr.bounds.size;
        }

        public static string SetText(this Text text, object obj)
        {
            return text.text = obj.ToString();
        }

        public static void SetTextSize(this Text text, int size)
        {
            text.fontSize = size;
        }

        public static void TextSettings(this Text text, TextAnchor anchor, VerticalWrapMode vWrap, HorizontalWrapMode hWrap)
        {
            text.alignment = anchor;
            text.verticalOverflow = vWrap;
            text.horizontalOverflow = hWrap;
        }

        public static Color Transparent => Vector4.zero;
        public static Color Gaming => Color.HSVToRGB(Time.unscaledTime % 1, 1, 1);

        public static void SetAlpha(this Image image, float alpha)
        {
            image.color = new(image.color.r, image.color.g, image.color.b, alpha);
        }

        public static void SetAlpha(this SpriteRenderer sr, float alpha)
        {
            sr.color = new(sr.color.r, sr.color.g, sr.color.b, alpha);
        }

        public static float GetAlpha(this Image image)
        {
            return image.color.a;
        }

        public static void SetColor(this SpriteRenderer sr, Color color)
        {
            sr.color = color;
        }

        public static Color SetColor(this Color color,
            float? red = null, float? green = null, float? blue = null, float? alpha = null)
        {
            if (red is null && green is null && blue is null && alpha is null)
            {
                throw new Karappoyanke();
            }

            return new Color(
                red ?? color.r,
                green ?? color.g,
                blue ?? color.b,
                alpha ?? color.a
            );
        }

        public static bool Twins(Color n1, Color n2)
        {
            return Mathf.Approximately(n1.r, n2.r) && Mathf.Approximately(n1.g, n2.g) && Mathf.Approximately(n1.b, n2.b);
        }
    }
}

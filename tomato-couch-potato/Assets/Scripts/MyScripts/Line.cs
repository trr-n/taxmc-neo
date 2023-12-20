using UnityEngine;

namespace trrne.Box
{
    public static class Line
    {
        public static void Width(this LineRenderer line, float width)
        => line.startWidth = line.endWidth = width;

        public static void Width(this LineRenderer line, float? start = null, float? end = null)
        {
            if (!Shorthand.None(start, end))
            {
                line.startWidth = start ?? line.startWidth;
                line.endWidth = end ?? line.endWidth;
            }
        }

        public static void Color(this LineRenderer line, Color color) => line.startColor = line.endColor = color;

        public static void Color(this LineRenderer line, Color? start = null, Color? end = null)
        {
            if (!Shorthand.None(start, end))
            {
                line.startColor = start ?? line.startColor;
                line.endColor = end ?? line.endColor;
            }
        }
    }
}
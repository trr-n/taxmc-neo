using UnityEngine;

namespace trrne.Pancreas
{
    public static partial class Gobject
    {
        public static bool BoxCast2D(out RaycastHit2D hit, Vector2 center, Vector2 size)
        => hit = Physics2D.BoxCast(center, size, 0, Vector2.up);

        public static bool BoxCast2D(out RaycastHit2D hit, Vector2 center, Vector2 size, int layer)
        => hit = Physics2D.BoxCast(center, size, 0, Vector2.up, 1, layer);

        public static bool BoxCast2D(out RaycastHit2D hit, Vector2 center, Vector2 size, int layer, float distance, float angle, Vector2 direction)
        => hit = Physics2D.BoxCast(center, size, angle, direction, distance, layer);

        public static bool Raycast2D(out RaycastHit2D hit, Vector2 origin, Vector2 direction)
        => hit = Physics2D.Raycast(origin, direction);

        public static bool Raycast2D(out RaycastHit2D hit, Vector2 origin, Vector2 direction, float distance)
        => hit = Physics2D.Raycast(origin, direction, distance);

        public static bool Raycast2D(out RaycastHit2D hit, Vector2 origin, Vector2 direction, int layer = 1 << 0, float distance = 1)
        => hit = Physics2D.Raycast(origin, direction, distance, layer);

        public static bool RaycastAll2D(out RaycastHit2D[] hit, Vector2 origin, Vector2 direction)
        {
            hit = Physics2D.RaycastAll(origin, direction);
            return hit.Length > 0;
        }

        public static bool RaycastAll2D(out RaycastHit2D[] hit, Vector2 origin, Vector2 direction, float distance)
        {
            hit = Physics2D.RaycastAll(origin, direction, distance);
            return hit.Length > 0;
        }
    }
}
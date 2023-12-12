using UnityEngine;

namespace trrne.Box
{
    public static partial class Gobject
    {
        public static bool Boxcast(out RaycastHit2D hit, Vector2 center, Vector2 size)
        => hit = Physics2D.BoxCast(center, size, 0, Vector2.up);

        public static bool Boxcast(out RaycastHit2D hit,
            Vector2 center, Vector2 size, int layer, float length = 1, float angle = 0, Vector2 direction = new())
        => hit = Physics2D.BoxCast(center, size, angle, direction, length, layer);

        public static bool Raycast(out RaycastHit2D hit, Ray ray, float length)
        => hit = Physics2D.Raycast(ray.origin, ray.direction, length);

        public static bool Raycast(out RaycastHit2D hit, Ray ray, int layer, float length)
        => hit = Physics2D.Raycast(ray.origin, ray.direction, length, layer);

        public static bool RaycastAll(out RaycastHit2D[] hit, Ray ray, float length)
        => (hit = Physics2D.RaycastAll(ray.origin, ray.direction, length)).Length > 0;

        public static bool Linecast(out RaycastHit2D hit, Ray ray)
        => hit = Physics2D.Linecast(ray.origin, ray.direction);

        public static bool Linecast(out RaycastHit2D hit, (Vector3 start, Vector3 end) line)
        => hit = Physics2D.Linecast(line.start, line.end);

        public static bool Linecast(out RaycastHit2D hit, Ray ray, int layer)
        => hit = Physics2D.Linecast(ray.origin, ray.direction, layer);

        public static bool Linecast(out RaycastHit2D hit, (Vector3 start, Vector3 end) ray, int layer)
        => hit = Physics2D.Linecast(ray.start, ray.end, layer);

        // --------------------------------------------------------------------

        public static RaycastHit2D Raycast(this Ray ray, float length, int layer)
        => Physics2D.Raycast(ray.origin, ray.direction, length, layer);

        public static RaycastHit2D Boxcast(this Cube cube,
            int layer, float length = 1, float angle = 0, Vector2 direction = new())
        => Physics2D.BoxCast(cube.origin, cube.size, angle, direction, length, layer);

        public static RaycastHit2D Boxcast(Vector3 center, Vector3 size,
            int layer, float length = 1, float angle = 0, Vector2 direction = new())
        => Physics2D.BoxCast(center, size, angle, direction, length, layer);
    }
}
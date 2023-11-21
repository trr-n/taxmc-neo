using System.Net;
using UnityEngine;

namespace trrne.Box
{
    public static partial class Gobject
    {
        public static bool BoxCast(out RaycastHit2D hit, Vector2 center, Vector2 size)
        => hit = Physics2D.BoxCast(center, size, 0, Vector2.up);

        public static bool BoxCast(out RaycastHit2D hit, Vector2 center, Vector2 size, int layer, float distance = 1, float angle = 0, Vector2 direction = new())
        => hit = Physics2D.BoxCast(center, size, angle, direction, distance, layer);

        public static bool Raycast(out RaycastHit2D hit, Ray ray, float distance)
        => hit = Physics2D.Raycast(ray.origin, ray.direction, distance);

        public static bool Raycast(out RaycastHit2D hit, Ray ray, int layer, float distance)
        => hit = Physics2D.Raycast(ray.origin, ray.direction, distance, layer);

        public static bool RaycastAll(out RaycastHit2D[] hit, Ray ray, float distance)
        => (hit = Physics2D.RaycastAll(ray.origin, ray.direction, distance)).Length > 0;

        public static bool TryGetRaycast<T>(Ray ray, float distance)
        {
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, distance);
            return hit && hit.TryGetComponent(out T _);
        }
    }
}
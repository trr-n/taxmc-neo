using UnityEngine;

namespace trrne.Box
{
    public static partial class Vector100
    {
        public static Vector2 Position2(this GameObject gob) => gob.transform.position;
        public static Vector2 Position2(this Collider2D info) => info.gameObject.transform.position;
        public static Vector2 Position2(this Collision2D info) => info.gameObject.transform.position;
    }
}
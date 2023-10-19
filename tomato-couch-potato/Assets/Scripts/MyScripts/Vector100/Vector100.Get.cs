using UnityEngine;

namespace Chickenen.Pancreas
{
    public static partial class Vector100
    {
        public static Vector2 Position2(this GameObject gob)
        {
            return gob.transform.position;
        }

        public static Vector2 Position2(this Collider2D info)
        {
            return info.gameObject.transform.position;
        }

        public static Vector2 Position2(this Collision2D info)
        {
            return info.gameObject.transform.position;
        }
    }
}
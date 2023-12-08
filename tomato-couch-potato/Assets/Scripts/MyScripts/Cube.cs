using UnityEngine;

namespace trrne.Box
{
    public sealed class Cube
    {
        public Vector2 center, size;

        public Cube(Vector2 origin, Vector2 size)
        {
            this.center = origin;
            this.size = size;
        }
    }
}
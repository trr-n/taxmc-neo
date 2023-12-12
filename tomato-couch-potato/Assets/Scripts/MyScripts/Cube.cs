using UnityEngine;

namespace trrne.Box
{
    public sealed class Cube
    {
        public Vector2 origin, size;

        public Cube(Vector2 origin, Vector2 size)
        {
            this.origin = origin;
            this.size = size;
        }
    }
}
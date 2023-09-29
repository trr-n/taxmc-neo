using UnityEngine;

namespace trrne.Bag
{
    public class HitBox
    {
        // Vector2 _origin, _size;
        (float x, float y) o, s;

        public HitBox((float x, float y) origin, (float x, float y) size)
        {
            o = origin;
            s = size;
        }

        public Vector2 origin => new(o.x, o.y);
        public Vector2 size => new(s.x, s.y);
    }
}
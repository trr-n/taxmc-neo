using UnityEngine;

namespace trrne.Bag
{
    public class HitBox
    {
        Vector2 _origin, _size;

        public HitBox(Vector2 origin, Vector2 size)
        {
            _origin = origin;
            _size = size;
        }

        public Vector2 origin => _origin;
        public Vector2 size => _size;
    }
}
using UnityEngine;

namespace trrne.Box
{
    public static partial class Shorthand
    {
        public static void DrawRay(this Ray ray, float length, Color color) => Debug.DrawRay(ray.origin, ray.direction * length, color);
        public static void styled(this object obj) => Debug.Log($"{nameof(obj)}: {obj}");

        public static void RBSettings(this Rigidbody2D rb) => Debug.Assert(true);
    }
}

// https://baba-s.hatenablog.com/entry/2020/01/10/090000
// https://qiita.com/t_takahari/items/6dc72f48b1ebdfed93b7
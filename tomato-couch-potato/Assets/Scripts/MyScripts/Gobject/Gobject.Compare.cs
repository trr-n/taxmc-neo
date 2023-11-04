using System;
using UnityEngine;

namespace trrne.Box
{
    public static partial class Gobject
    {
        public static bool CompareTag(this Collision info, string tag) => info.gameObject.CompareTag(tag);
        public static bool CompareTag(this Collision2D info, string tag) => info.gameObject.CompareTag(tag);
        public static bool CompareTag(this RaycastHit2D hit, string tag) => hit.collider.CompareTag(tag);
        [Obsolete] public static bool CompareTag(this Collider info, string tag) => info.CompareTag(tag);
        [Obsolete] public static bool CompareTag(this Collider2D info, string tag) => info.CompareTag(tag);

        public static bool CompareLayer(this Collider2D info, int layer) => info.GetLayer() == layer;
        public static bool CompareLayer(this Collision2D info, int layer) => info.GetLayer() == layer;
        public static bool CompareLayer(this RaycastHit2D hit, int layer) => hit.GetLayer() == layer;

        public static bool CompareBoth(this Collision2D info, int layer, string tag) => info.CompareLayer(layer) && info.gameObject.CompareTag(tag);
        public static bool CompareBoth(this Collider2D info, int layer, string tag) => info.CompareLayer(layer) && info.gameObject.CompareTag(tag);
        public static bool CompareBoth(this RaycastHit2D hit, int layer, string tag) => hit.CompareLayer(layer) && hit.collider.CompareTag(tag);

        public static bool Contain(this Collision info, string tag) => info.gameObject.tag.Contains(tag);
        public static bool Contain(this Collider info, string tag) => info.tag.Contains(tag);
        public static bool Contain(this Collision2D info, string tag) => info.gameObject.tag.Contains(tag);
        public static bool Contain(this Collider2D info, string tag) => info.gameObject.tag.Contains(tag);
    }
}
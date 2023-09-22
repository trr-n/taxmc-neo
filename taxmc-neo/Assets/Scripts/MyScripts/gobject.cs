using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace trrne.utils
{
    public enum Active { Self, Hierarchy }

    public static class Gobject
    {
        public static GameObject Generate(this GameObject[] g, Vector3 p = new(), Quaternion r = new()) => Object.Instantiate(g.Choice3(), p, r);
        public static GameObject Generate(this GameObject g, Vector3 p = new(), Quaternion r = new()) => Object.Instantiate(g, p, r);
        public static GameObject Generate(this GameObject gob) => Object.Instantiate(gob);

        public static bool Compare(this Collision info, string tag) => info.gameObject.CompareTag(tag);
        public static bool Compare(this Collider info, string tag) => info.CompareTag(tag);
        public static bool Compare(this Collision2D info, string tag) => info.gameObject.CompareTag(tag);
        public static bool Compare(this Collider2D info, string tag) => info.CompareTag(tag);
        public static bool Compare(this RaycastHit2D hit, string tag) => hit.collider.CompareTag(tag);

        public static bool Contain(this Collision info, string tag) => info.gameObject.tag.Contains(tag);
        public static bool Contain(this Collider info, string tag) => info.tag.Contains(tag);
        public static bool Contain(this Collision2D info, string tag) => info.gameObject.tag.Contains(tag);
        public static bool Contain(this Collider2D info, string tag) => info.gameObject.tag.Contains(tag);

        public static T GetWithTag<T>(string tag) => Find(tag).GetComponent<T>();
        public static T GetWithTag<T>(this GameObject gob) => gob.GetComponent<T>();
        public static bool TryWithTag<T>(out T t, string tag) => Find(tag).TryGetComponent(out t);
        public static T GetWithName<T>(string name) => GameObject.Find(name).GetComponent<T>();

        public static int GetLayer(this RaycastHit2D hit) => 1 << hit.collider.gameObject.layer;
        public static int GetLayer(this Collision2D info) => 1 << info.gameObject.layer;

        public static T Get<T>(this Collision2D info) => info.gameObject.GetComponent<T>();
        public static bool Get<T>(this Collision2D info, out T t) { t = info.Get<T>(); return t is null; }
        public static T Get<T>(this Collider2D info) => info.gameObject.GetComponent<T>();
        public static T Get<T>(this Collision info) => info.gameObject.GetComponent<T>();
        public static T Get<T>(this Collider info) => info.gameObject.GetComponent<T>();
        public static T Get<T>(this RaycastHit2D hit) => hit.collider.Get<T>();

        public static bool Try<T>(this Collision2D info, out T t) => info.gameObject.TryGetComponent(out t);
        public static bool Try<T>(this Collider2D info, out T t) => info.gameObject.TryGetComponent(out t);
        public static bool Try<T>(this Collision info, out T t) => info.gameObject.TryGetComponent(out t);
        public static bool Try<T>(this Collider info, out T t) => info.gameObject.TryGetComponent(out t);
        public static bool Try<T>(this GameObject gob, out T t) => gob.TryGetComponent(out t);
        public static bool Try<T>(this RaycastHit2D hit, out T t) => hit.collider.TryGetComponent(out t);
        public static T Try<T>(this GameObject gob) { gob.TryGetComponent(out T t); return t is null ? default : t; }

        public static GameObject Find(string tag) => GameObject.FindGameObjectWithTag(tag);
        public static GameObject[] Finds(string tag) => GameObject.FindGameObjectsWithTag(tag);

        public static void Destroy(this GameObject gob, float lifetime = 0) => Object.Destroy(gob, lifetime);
        public static void Destroy(this Collider info, float lifetime = 0) => Object.Destroy(info.gameObject, lifetime);
        public static void Destroy(this Collider2D info, float lifetime = 0) => Object.Destroy(info.gameObject, lifetime);
        public static void Destroy(this Collision info, float lifetime = 0) => Object.Destroy(info.gameObject, lifetime);
        public static void Destroy(this Collision2D info, float lifetime = 0) => Object.Destroy(info.gameObject, lifetime);

        public static bool IsActive(this Text text) => text.IsActive();
        public static bool IsActive(this GameObject gob, Active? active = null)
        => active switch { Active.Self => gob.activeSelf, Active.Hierarchy => gob.activeInHierarchy, _ => throw null, };

        public static void SetActives(this GameObject[] gobs, bool state) { foreach (var gob in gobs) { gob.SetActive(state); } }

        public static bool BoxCast2D(out RaycastHit2D hit,
            Vector2 origin, Vector2 size, int layer = 1 << 0, float distance = 1, float angle = 0, Vector2 direction = new())
        {
            hit = Physics2D.BoxCast(origin, size, angle, direction, distance, layer);
            return hit;
        }

        public static bool Raycast2D(out RaycastHit2D hit,
            Vector2 origin, Vector2 direction, int layer = 1 << 0, float distance = 1)
        {
            hit = Physics2D.Raycast(origin, direction, distance, layer);
            return hit;
        }

        public static GameObject GetChildGameObject(this Transform t, int? specify = null) => t.GetChild(specify == null ? 0 : (int)specify).gameObject;
    }
}

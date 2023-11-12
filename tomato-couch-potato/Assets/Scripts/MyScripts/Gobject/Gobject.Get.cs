#pragma warning disable UNT0014
#pragma warning disable IDE0002

using System;
using UnityEngine;

namespace trrne.Box
{
    public static partial class Gobject
    {
        public static GameObject Find(string tag) => GameObject.FindGameObjectWithTag(tag);
        public static GameObject[] Finds(string tag) => GameObject.FindGameObjectsWithTag(tag);
        public static T[] Finds<T>() where T : UnityEngine.Object => GameObject.FindObjectsByType<T>(FindObjectsSortMode.None);

        public static T GetWithTag<T>(string tag) => Find(tag).GetComponent<T>();
        public static T GetWithTag<T>(this GameObject gob) => gob.GetComponent<T>();
        public static bool TryGetWithTag<T>(out T t, string tag) => Find(tag).TryGetComponent(out t);
        [Obsolete] public static T GetWithName<T>(string name) => GameObject.Find(name).GetComponent<T>();
        public static T GetFromChild<T>(this Transform transform, int index = 0) => transform.GetChild(index).GetComponent<T>();
        public static T GetFromParent<T>(this Transform transform) => transform.parent.GetComponent<T>();
        public static T GetFromRoot<T>(this Transform transform) => transform.root.GetComponent<T>();

        public static T Get<T>(this Collision2D info) => info.gameObject.GetComponent<T>();
        public static T Get<T>(this Collider2D info) => info.gameObject.GetComponent<T>();
        public static T Get<T>(this Collision info) => info.gameObject.GetComponent<T>();
        public static T Get<T>(this Collider info) => info.gameObject.GetComponent<T>();
        public static T Get<T>(this RaycastHit2D hit) => hit.collider.Get<T>();

        public static bool TryGet<T>(this Collision2D info, out T t) => info.gameObject.TryGetComponent(out t);
        public static bool TryGet<T>(this Collider2D info, out T t) => info.gameObject.TryGetComponent(out t);
        public static bool TryGet<T>(this Collision info, out T t) => info.gameObject.TryGetComponent(out t);
        public static bool TryGet<T>(this Collider info, out T t) => info.gameObject.TryGetComponent(out t);
        public static bool TryGet<T>(this GameObject gob, out T t) => gob.TryGetComponent(out t);
        public static bool TryGet<T>(this RaycastHit2D hit, out T t) => hit.collider.TryGetComponent(out t);
        public static void TryAction<T>(this Collider2D info, Action<T> action) => Shorthand.If(info.TryGetComponent(out T t), () => action(t));

        public static GameObject GetChildObject(this Transform t) => t.GetChild(0).gameObject;
        public static GameObject GetChildObject(this Transform t, int index) => t.GetChild(index).gameObject;

        public static GameObject[] GetChildren(this Transform t)
        {
            var children = new GameObject[t.childCount];
            for (int i = 0; i < children.Length; i++)
            {
                children[i] = t.GetChild(i).gameObject;
            }
            return children;
        }

        public static T[] GetChildren<T>(this Transform t) where T : UnityEngine.Object
        {
            var children = new T[t.childCount];
            for (int i = 0; i < children.Length; i++)
            {
                children[i] = t.GetChild(i).GetComponent<T>();
            }
            return children;
        }

        public static int GetLayer(this RaycastHit2D hit) => 1 << hit.collider.gameObject.layer;
        public static int GetLayer(this Collision2D info) => 1 << info.gameObject.layer;
        public static int GetLayer(this Collider2D info) => 1 << info.gameObject.layer;
    }
}
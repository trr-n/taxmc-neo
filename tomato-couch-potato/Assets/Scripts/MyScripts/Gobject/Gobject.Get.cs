#pragma warning disable UNT0014
#pragma warning disable IDE0002

using System;
using UnityEngine;

namespace trrne.Box
{
    public static partial class Gobject
    {
        public static GameObject GetWithTag(this string tag) => GameObject.FindGameObjectWithTag(tag);
        public static GameObject[] Finds(string tag) => GameObject.FindGameObjectsWithTag(tag);
        public static T[] Finds<T>() where T : UnityEngine.Object => GameObject.FindObjectsByType<T>(FindObjectsSortMode.None);

        public static T GetComponentWithTag<T>(string tag) => GetWithTag(tag).GetComponent<T>();
        public static T GetComponentWithTag<T>(this GameObject gob) => gob.GetComponent<T>();
        public static bool TryGetComponentWithTag<T>(out T t, string tag) => GetWithTag(tag).TryGetComponent(out t);
        [Obsolete] public static T GetComponentWithName<T>(string name) => GameObject.Find(name).GetComponent<T>();
        public static T GetComponentFromChild<T>(this Transform transform, int index = 0) => transform.GetChild(index).GetComponent<T>();
        public static T GetComponentFromParent<T>(this Transform transform) => transform.parent.GetComponent<T>();
        public static T GetComponentFromRoot<T>(this Transform transform) => transform.root.GetComponent<T>();

        public static T GetComponent<T>(this Collision2D info) => info.gameObject.GetComponent<T>();
        public static T GetComponent<T>(this Collider2D info) => info.gameObject.GetComponent<T>();
        public static T GetComponent<T>(this Collision info) => info.gameObject.GetComponent<T>();
        public static T GetComponent<T>(this Collider info) => info.gameObject.GetComponent<T>();
        public static T GetComponent<T>(this RaycastHit2D hit) => GetComponent<T>(hit.collider);

        [Obsolete] public static bool TryGetComponent<T>(this Collision2D info, out T t) => info.TryGetComponent(out t);
        [Obsolete] public static bool TryGetComponent<T>(this Collider2D info, out T t) => info.TryGetComponent(out t);
        public static bool TryGetComponent<T>(this Collision info, out T t) => info.gameObject.TryGetComponent(out t);
        public static bool TryGetComponent<T>(this Collider info, out T t) => info.gameObject.TryGetComponent(out t);
        public static bool TryGetComponent<T>(this RaycastHit2D hit, out T t) => hit.collider.TryGetComponent(out t);
        public static void TryAction<T>(this Collider2D info, Action<T> action) => Shorthand.If(info.TryGetComponent(out T t), () => action(t));

        public static GameObject GetChildGameObject(this Transform t) => t.GetChild(0).gameObject;
        public static GameObject GetChildGameObject(this Transform t, int index) => t.GetChild(index).gameObject;

        public static GameObject[] GetChildrenGameObject(this Transform t)
        {
            var children = new GameObject[t.childCount];
            for (int i = 0; i < children.Length; i++)
            {
                children[i] = t.GetChild(i).gameObject;
            }
            return children;
        }

        public static T[] GetComponentsFromChildren<T>(this Transform t) where T : UnityEngine.Object
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
#pragma warning disable UNT0014
#pragma warning disable IDE0002

using System;
using UnityEngine;
using UniGob = UnityEngine.GameObject;

namespace trrne.Box
{
    public static partial class Gobject
    {
        public static UniGob GetGameObjectWithTag(this string tag) => UniGob.FindGameObjectWithTag(tag);
        public static UniGob[] GetGameObjectsWithTag(string tag) => UniGob.FindGameObjectsWithTag(tag);
        public static T[] Finds<T>(FindObjectsInactive inactive = FindObjectsInactive.Exclude,
            FindObjectsSortMode mode = FindObjectsSortMode.None) where T : UnityEngine.Object
        => UniGob.FindObjectsByType<T>(findObjectsInactive: inactive, sortMode: mode);

        public static T GetComponentWithTag<T>(this string tag) => GetGameObjectWithTag(tag).GetComponent<T>();
        public static T GetComponentWithTag<T>(this UniGob gob) => gob.GetComponent<T>();
        public static bool TryGetComponentWithTag<T>(out T t, string tag) => GetGameObjectWithTag(tag).TryGetComponent(out t);
        [Obsolete("Way to get gameobject, GetComponentWithTag is better than this.")]
        public static T GetComponentWithName<T>(this string name) => UniGob.Find(name).GetComponent<T>();
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
        public static void TryAction<T>(this Collider2D info, Action<T> action) => info.TryGetComponent(out T t).If(() => action(t));

        public static UniGob GetChildGameObject(this Transform t) => t.GetChild(0).gameObject;
        public static UniGob GetChildGameObject(this Transform t, int index) => t.GetChild(index).gameObject;

        public static UniGob[] GetChildrenGameObject(this Transform t)
        {
            UniGob[] children = new UniGob[t.childCount];
            for (int i = 0; i < children.Length; i++)
                children[i] = t.GetChild(i).gameObject;
            return children;
        }

        public static T[] GetComponentsFromChildren<T>(this Transform t) where T : UnityEngine.Object
        {
            T[] children = new T[t.childCount];
            for (int i = 0; i < children.Length; i++)
                children[i] = t.GetChild(i).GetComponent<T>();
            return children;
        }

        public static int GetLayer(this RaycastHit2D hit) => 1 << hit.collider.gameObject.layer;
        public static int GetLayer(this Collision2D info) => 1 << info.gameObject.layer;
        public static int GetLayer(this Collider2D info) => 1 << info.gameObject.layer;
    }
}
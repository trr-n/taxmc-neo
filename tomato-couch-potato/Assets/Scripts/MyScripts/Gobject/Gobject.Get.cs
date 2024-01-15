#pragma warning disable UNT0014
#pragma warning disable IDE0002

using System;
using UnityEngine;

namespace trrne.Box
{
    public static partial class Gobject
    {
        /// <summary>
        /// タグで検索し取得する -> <b>GameObject</b><br/>
        /// <c>Gobject.GetWithTag(tag)</c>
        /// </summary>
        /// <param name="tag"></param>
        /// <returns>取得したオブジェクトを返す</returns>
        public static GameObject GetWithTag(this string tag) => GameObject.FindGameObjectWithTag(tag);
        public static GameObject[] GetsWithTag(this string tag) => GameObject.FindGameObjectsWithTag(tag);

        public static T[] Finds<T>(FindObjectsInactive inactive = FindObjectsInactive.Exclude, FindObjectsSortMode mode = FindObjectsSortMode.None)
        where T : UnityEngine.Object => GameObject.FindObjectsByType<T>(inactive, mode);

        public static T GetWithTag<T>(this string tag) => GetWithTag(tag).GetComponent<T>();
        public static T GetWithTag<T>(this GameObject gob) => gob.GetComponent<T>();
        public static bool TryGetWithTag<T>(out T t, string tag) => GetWithTag(tag).TryGetComponent(out t);
        [Obsolete("Way to get gameobject, GetWithTag is better than this.")]
        public static T GetWithName<T>(this string name) => GameObject.Find(name).GetComponent<T>();
        public static T GetFromChild<T>(this Transform t, int index = 0) => t.GetChild(index).GetComponent<T>();
        public static T GetFromParent<T>(this Transform t) => t.parent.GetComponent<T>();
        public static T GetFromRoot<T>(this Transform t) => t.root.GetComponent<T>();
        public static T GetComponent<T>(this Collision2D info) => info.gameObject.GetComponent<T>();
        public static T GetComponent<T>(this Collider2D info) => info.gameObject.GetComponent<T>();
        public static T GetComponent<T>(this Collision info) => info.gameObject.GetComponent<T>();
        public static T GetComponent<T>(this Collider info) => info.gameObject.GetComponent<T>();
        public static T GetComponent<T>(this RaycastHit2D hit) => GetComponent<T>(hit.collider);

        public static bool TryGetComponent<T>(this Collision2D info, out T t) => info.gameObject.TryGetComponent(out t);
        public static bool TryGetComponent<T>(this Collider2D info, out T t) => info.gameObject.TryGetComponent(out t);
        public static bool TryGetComponent<T>(this Collision info, out T t) => info.gameObject.TryGetComponent(out t);
        public static bool TryGetComponent<T>(this Collider info, out T t) => info.gameObject.TryGetComponent(out t);
        public static bool TryGetComponent<T>(this RaycastHit2D hit, out T t) => hit.collider.TryGetComponent(out t);
        public static void TryAction<T>(this Collider2D info, Action<T> action) => info.TryGetComponent(out T t).If(() => action(t));

        public static GameObject GetChildGameObject(this Transform t) => t.GetChild(0).gameObject;
        public static GameObject GetChildGameObject(this Transform t, int i) => t.GetChild(i).gameObject;

        public static GameObject[] GetChildrenGameObject(this Transform t)
        {
            var children = new GameObject[t.childCount];
            for (int i = 0; i < children.Length; ++i)
            {
                children[i] = t.GetChild(i).gameObject;
            }
            return children;
        }

        public static T[] GetComponentsFromChildren<T>(this Transform t)
            where T : UnityEngine.Object
        {
            var children = new T[t.childCount];
            for (int i = 0; i < children.Length; ++i)
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
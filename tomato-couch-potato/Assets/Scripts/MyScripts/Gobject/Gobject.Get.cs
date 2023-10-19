#pragma warning disable UNT0014

using System;
using System.Reflection;
using UnityEngine;

namespace Chickenen.Pancreas
{
    public static partial class Gobject
    {
        public static GameObject Find(string tag)
        {
            return GameObject.FindGameObjectWithTag(tag);
        }

        public static GameObject[] Finds(string tag)
        {
            return GameObject.FindGameObjectsWithTag(tag);
        }

        public static GameObject GetWithTag(string tag)
        {
            return Find(tag);
        }

        public static T GetWithTag<T>(string tag)
        {
            return Find(tag).GetComponent<T>();
        }

        public static T GetWithTag<T>(this GameObject gob)
        {
            return gob.GetComponent<T>();
        }

        public static bool TryGetWithTag<T>(out T t, string tag)
        {
            return Find(tag).TryGetComponent(out t);
        }

        [Obsolete]
        public static T GetWithName<T>(string name)
        {
            return GameObject.Find(name).GetComponent<T>();
        }

        public static T GetFromChild<T>(this Transform transform)
        {
            return transform.GetChild(0).GetComponent<T>();
        }

        public static T GetFromChild<T>(this Transform transform, int index)
        {
            return transform.GetChild(index).GetComponent<T>();
        }

        public static T GetFromParent<T>(this Transform transform)
        {
            return transform.parent.GetComponent<T>();
        }

        public static T GetFromRoot<T>(this Transform transform)
        {
            return transform.root.GetComponent<T>();
        }


        [Obsolete]
        public static GameObject GetWithInstanceID(int id)
        {
            return (GameObject)FindObjectFromInstanceID(id);
        }

        [Obsolete]
        static UnityEngine.Object FindObjectFromInstanceID(int id)
        {
            try
            {
                var flags = BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.InvokeMethod;
                var ret = typeof(UnityEngine.Object).InvokeMember(nameof(FindObjectFromInstanceID), flags, null, null, new object[] { id });
                return (UnityEngine.Object)ret;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static T Get<T>(this Collision2D info)
        {
            return info.gameObject.GetComponent<T>();
        }

        public static T Get<T>(this Collider2D info)
        {
            return info.gameObject.GetComponent<T>();
        }

        public static T Get<T>(this Collision info)
        {
            return info.gameObject.GetComponent<T>();
        }

        public static T Get<T>(this Collider info)
        {
            return info.gameObject.GetComponent<T>();
        }

        public static T Get<T>(this RaycastHit2D hit)
        {
            return hit.collider.Get<T>();
        }


        public static GameObject GetChildObject(this Transform t)
        {
            return t.GetChild(0).gameObject;
        }

        public static GameObject GetChildObject(this Transform t, int index)
        {
            return t.GetChild(index).gameObject;
        }

        public static GameObject[] GetChildren(this Transform t)
        {
            var children = new GameObject[t.childCount];
            for (int i = 0; i < children.Length; i++)
            {
                children[i] = t.GetChild(i).gameObject;
            }
            return children;
        }

        public static bool TryGet<T>(this Collision2D info, out T t)
        {
            return info.gameObject.TryGetComponent(out t);
        }

        public static bool TryGet<T>(this Collider2D info, out T t)
        {
            return info.gameObject.TryGetComponent(out t);
        }

        public static bool TryGet<T>(this Collision info, out T t)
        {
            return info.gameObject.TryGetComponent(out t);
        }

        public static bool TryGet<T>(this Collider info, out T t)
        {
            return info.gameObject.TryGetComponent(out t);
        }

        public static bool TryGet<T>(this GameObject gob, out T t)
        {
            return gob.TryGetComponent(out t);
        }

        public static bool TryGet<T>(this RaycastHit2D hit, out T t)
        {
            return hit.collider.TryGetComponent(out t);
        }

        public static void TryAction<T>(this Collider2D info, Action<T> action)
        {
            Shorthand.BoolAction(info.TryGetComponent(out T t), () => action(t));
        }

        [Obsolete]
        public static T TryGet<T>(this GameObject gob)
        {
            gob.TryGetComponent(out T t);
            return t != null ? t : default;
        }

        public static int GetLayer(this RaycastHit2D hit)
        {
            return 1 << hit.collider.gameObject.layer;
        }

        public static int GetLayer(this Collision2D info)
        {
            return 1 << info.gameObject.layer;
        }

        public static int GetLayer(this Collider2D info)
        {
            return 1 << info.gameObject.layer;
        }
    }
}
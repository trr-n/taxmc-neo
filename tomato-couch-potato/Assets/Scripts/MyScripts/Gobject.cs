using System;
using System.Reflection;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

namespace trrne.Pancreas
{
    public enum ActiveStatus { Self, Hierarchy }

    public static class Gobject
    {
        public static GameObject Generate(this GameObject g)
        {
            return GameObject.Instantiate(g);
        }

        public static GameObject Generate(this GameObject g, Vector3 p)
        {
            return GameObject.Instantiate(g, p, quaternion.identity);
        }

        public static GameObject Generate(this GameObject g, Vector3 p, Quaternion r)
        {
            return GameObject.Instantiate(g, p, r);
        }

        public static GameObject Generate(this GameObject[] gs)
        {
            return GameObject.Instantiate(gs.Choice());
        }

        public static GameObject Generate(this GameObject[] gs, Vector3 p)
        {
            return GameObject.Instantiate(gs.Choice(), p, quaternion.identity);
        }

        public static GameObject Generate(this GameObject[] gs, Vector3 p, Quaternion r)
        {
            return GameObject.Instantiate(gs.Choice(), p, r);
        }

        public static GameObject TryGenerate(this GameObject g)
        {
            return g != null ? g.Generate() : null;
        }

        public static GameObject TryGenerate(this GameObject g, Vector3 p)
        {
            return g != null ? g.Generate(p, quaternion.identity) : null;
        }

        public static GameObject TryGenerate(this GameObject g, Vector3 p, Quaternion r)
        {
            return g != null ? g.Generate(p, r) : null;
        }

        public static GameObject TryGenerate(this GameObject[] gs)
        {
            return gs.Length > 0 ? gs.Generate() : null;
        }

        public static GameObject TryGenerate(this GameObject[] gs, Vector3 p)
        {
            return gs.Length > 0 ? gs.Generate(p, quaternion.identity) : null;
        }

        public static GameObject TryGenerate(this GameObject[] gs, Vector3 p, Quaternion r)
        {
            return gs.Length > 0 ? gs.Generate(p, r) : null;
        }

        public static bool CompareTag(this Collision info, string tag)
        {
            return info.gameObject.CompareTag(tag);
        }

        [Obsolete]
        public static bool CompareTag(this Collider info, string tag)
        {
            return info.CompareTag(tag);
        }

        public static bool CompareTag(this Collision2D info, string tag)
        {
            return info.gameObject.CompareTag(tag);
        }

        [Obsolete]
        public static bool CompareTag(this Collider2D info, string tag)
        {
            return info.CompareTag(tag);
        }

        public static bool CompareTag(this RaycastHit2D hit, string tag)
        {
            return hit.collider.CompareTag(tag);
        }

        public static bool CompareLayer(this Collider2D info, int layer)
        {
            return info.GetLayer() == layer;
        }

        public static bool CompareBoth(this Collision2D info, int layer, string tag)
        {
            return info.GetLayer(layer) && info.gameObject.CompareTag(tag);
        }

        public static bool CompareBoth(this Collider2D info, int layer, string tag)
        {
            return info.GetLayer(layer) && info.gameObject.CompareTag(tag);
        }

        public static bool Contain(this Collision info, string tag)
        {
            return info.gameObject.tag.Contains(tag);
        }

        public static bool Contain(this Collider info, string tag)
        {
            return info.tag.Contains(tag);
        }

        public static bool Contain(this Collision2D info, string tag)
        {
            return info.gameObject.tag.Contains(tag);
        }

        public static bool Contain(this Collider2D info, string tag)
        {
            return info.gameObject.tag.Contains(tag);
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

        public static T GetFromChild<T>(this Transform transform) where T : MonoBehaviour
        {
            return transform.GetChild(0).GetComponent<T>();
        }

        public static T GetFromChild<T>(this Transform transform, int index) where T : MonoBehaviour
        {
            return transform.GetChild(index).GetComponent<T>();
        }

        public static T GetFromParent<T>(this Transform transform) where T : MonoBehaviour
        {
            return transform.parent.GetComponent<T>();
        }

        public static T GetFromRoot<T>(this Transform transform) where T : MonoBehaviour
        {
            return transform.root.GetComponent<T>();
        }


        public static GameObject GetWithInstanceID(int id)
        {
            return (GameObject)FindObjectFromInstanceID(id);
        }

        static UnityEngine.Object FindObjectFromInstanceID(int id)
        {
            try
            {
                var type = typeof(UnityEngine.Object);
                var flags = BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.InvokeMethod;
                var ret = type.InvokeMember(nameof(FindObjectFromInstanceID), flags, null, null, new object[] { id });
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

        [Obsolete]
        public static T TryGet<T>(this GameObject gob)
        {
            gob.TryGetComponent(out T t);
            return t != null ? t : default;
        }

        public static void TryAction<T>(this Collider2D info, Action<T> action)
        {
            Shorthand.BoolAction(info.TryGetComponent(out T t), () => action(t));
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

        public static bool GetLayer(this Collision2D info, int layer)
        {
            return info.GetLayer() == layer;
        }

        public static bool GetLayer(this Collider2D info, int layer)
        {
            return info.GetLayer() == layer;
        }

        public static GameObject Find(string tag)
        {
            return GameObject.FindGameObjectWithTag(tag);
        }

        public static GameObject[] Finds(string tag)
        {
            return GameObject.FindGameObjectsWithTag(tag);
        }

        public static void Destroy(this GameObject gob, float lifetime = 0)
        {
            Destroy(gob, lifetime);
        }

        public static void Destroy(this Collider info, float lifetime = 0)
        {
            Destroy(info.gameObject, lifetime);
        }

        public static void Destroy(this Collider2D info, float lifetime = 0)
        {
            Destroy(info.gameObject, lifetime);
        }

        public static void Destroy(this Collision info, float lifetime = 0)
        {
            Destroy(info.gameObject, lifetime);
        }

        public static void Destroy(this Collision2D info, float lifetime = 0)
        {
            Destroy(info.gameObject, lifetime);
        }

        public static void Destroy(this RaycastHit2D info, float lifetime = 0)
        {
            Destroy(info.collider.gameObject, lifetime);
        }


        public static bool IsActive(this Text text)
        {
            return text.IsActive();
        }

        public static bool IsActive(this GameObject gob, ActiveStatus? active = null)
        {
            return active switch
            {
                ActiveStatus.Self => gob.activeSelf,
                ActiveStatus.Hierarchy => gob.activeInHierarchy,
                _ => throw null,
            };
        }

        public static void SetActives(this GameObject[] gobs, bool state)
        {
            gobs.ForEach(gob => gob.SetActive(state));
        }

        public static bool BoxCast2D(out RaycastHit2D hit, Vector2 center, Vector2 size)
        {
            return hit = Physics2D.BoxCast(center, size, 0, Vector2.up);
        }

        public static bool BoxCast2D(out RaycastHit2D hit, Vector2 center, Vector2 size, int layer)
        {
            return hit = Physics2D.BoxCast(center, size, 0, Vector2.up, 1, layer);
        }

        public static bool BoxCast2D(out RaycastHit2D hit, Vector2 center, Vector2 size, int layer, float distance, float angle, Vector2 direction)
        {
            return hit = Physics2D.BoxCast(center, size, angle, direction, distance, layer);
        }

        public static bool Raycast2D(out RaycastHit2D hit, Vector2 origin, Vector2 direction, int layer = 1 << 0, float distance = 1)
        {
            return hit = Physics2D.Raycast(origin, direction, distance, layer);
        }

        public static float Duration(this GameObject gob)
        {
            return gob.GetComponent<ParticleSystem>().main.duration;
        }
    }
}

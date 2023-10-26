using System;
using System.Reflection;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

namespace trrne.Pancreas
{
    public enum ActiveStatus { Self, Hierarchy }

    public static partial class Gobject
    {
        public static void Destroy(this GameObject gob, float lifetime = 0) => Destroy(gob, lifetime);
        public static void Destroy(this Collider info, float lifetime = 0) => Destroy(info.gameObject, lifetime);
        public static void Destroy(this Collider2D info, float lifetime = 0) => Destroy(info.gameObject, lifetime);
        public static void Destroy(this Collision info, float lifetime = 0) => Destroy(info.gameObject, lifetime);
        public static void Destroy(this Collision2D info, float lifetime = 0) => Destroy(info.gameObject, lifetime);
        public static void Destroy(this RaycastHit2D info, float lifetime = 0) => Destroy(info.collider.gameObject, lifetime);

        public static bool IsActive(this Text text) => text.IsActive();

        public static bool IsActive(this GameObject gob, ActiveStatus? active = null)
        {
            return active switch
            {
                ActiveStatus.Self => gob.activeSelf,
                ActiveStatus.Hierarchy => gob.activeInHierarchy,
                _ => throw null,
            };
        }

        public static void SetActives(this GameObject[] gobs, bool state) => gobs.ForEach(gob => gob.SetActive(state));

        public static float Duration(this GameObject gob) => gob.GetComponent<ParticleSystem>().main.duration;
    }
}

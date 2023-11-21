#pragma warning disable IDE0002

using UnityEngine;
using UnityEngine.UI;

using UniGob = UnityEngine.GameObject;

namespace trrne.Box
{
    public enum ActiveStatus { Self, Hierarchy }

    public static partial class Gobject
    {
        public static void Destroy(this UniGob gob, float lifetime = 0) => Destroy(gob, lifetime);
        public static void Destroy(this Collider info, float lifetime = 0) => Destroy(info.gameObject, lifetime);
        public static void Destroy(this Collider2D info, float lifetime = 0) => Destroy(info.gameObject, lifetime);
        public static void Destroy(this Collision info, float lifetime = 0) => Destroy(info.gameObject, lifetime);
        public static void Destroy(this Collision2D info, float lifetime = 0) => Destroy(info.gameObject, lifetime);
        public static void Destroy(this RaycastHit2D info, float lifetime = 0) => Destroy(info.collider.gameObject, lifetime);
        public static void AliveOnLoad(this UniGob gob) => UniGob.DontDestroyOnLoad(gob);

        public static bool IsActive(this Text text) => text.IsActive();

        public static bool IsActive(this UniGob gob, ActiveStatus? active = null)
        => active switch
        {
            ActiveStatus.Self => gob.activeSelf,
            ActiveStatus.Hierarchy => gob.activeInHierarchy,
            _ => throw null,
        };

        public static void SetActives(this UniGob[] gobs, bool state) => gobs.ForEach(gob => gob.SetActive(state));

        public static float Duration(this UniGob gob) => gob.GetComponent<ParticleSystem>().main.duration;
    }
}

using System.Collections;
using UnityEngine;

namespace trrne.Box
{
    public static class CameraSalmon
    {
        static readonly MonoBehaviour mb;
        static CameraSalmon() => mb = new GameObject("salmon!").AddComponent<MonoBehaviour>(); //.GetComponent<MonoBehaviour>();

        public static void DoSalmon(this Camera camera, float duration, float magnitude)
        => mb.StartCoroutine(camera.Salmon(duration, magnitude));

        static IEnumerator Salmon(this Camera c, float d, float m)
        {
            var p = c.transform.localPosition;
            var t = 0f;

            while (t <= d)
            {
                var x = p.x + Random.Range(-1f, 1f) * m;
                var y = p.y + Random.Range(-1f, 1f) * m;
                c.transform.localPosition = new(x, y, p.z);
                t += Time.deltaTime;
                yield return null;
            }
            c.transform.localPosition = p;
        }
    }
}
using UnityEngine;

namespace trrne.Utils
{
    public enum LogFormat { Standard, Warning, Error }
    public static class log
    {
        static void show(this object msg) => Debug.Log($"<color=white>{msg}</color>");
        static void warn(this object msg) => Debug.LogWarning($"<color=yellow>{msg}</color>");
        static void error(this object msg) => Debug.LogError($"<color=red>{msg}</color>");

        public static void show(this object msg, LogFormat style = LogFormat.Standard)
        {

            switch (style)
            {
                case LogFormat.Standard: msg.show(); break;
                case LogFormat.Warning: msg.warn(); break;
                case LogFormat.Error: msg.error(); break;
            }
        }

        public static string NewLine(this object msg) => msg + "\r\n";
        public static string Space(this object msg) => msg + " ";

        public static void DrawRays(params Ray[] rays)
        {
            foreach (var ray in rays)
            {
                Debug.DrawRay(ray.origin, ray.direction);
            }
        }
    }
}

using trrne.utils;
using UnityEngine;

namespace trrne.Game
{
    public class Stage : MonoBehaviour
    {
        static int cur;
        public static int current => cur;

        public static int max => Scenes.total;

        public static bool isClear => cur >= max;
        public static float progress => max / cur;

        public static void Next() => cur += 1;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Self.Game
{
    public class Stage : MonoBehaviour
    {
        static int current;
        public static int Current => current;

        public const int Max = 1;

        public static bool IsClear => current >= Max;
        public static float Progress => Max / current;

        public static void Next() => current += 1;
    }
}
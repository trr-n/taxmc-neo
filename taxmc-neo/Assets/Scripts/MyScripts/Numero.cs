﻿using System;
using UnityEngine;

namespace trrne.Bag
{
    public static class Numero
    {
        public static float Clamping(this float n, float min, float max) => Mathf.Clamp(n, min, max);
        public static int Clamping(this int n, int min, int max) => Mathf.Clamp(n, min, max);
        public static float Clamp(float n, float min, float max) => Mathf.Clamp(n, min, max);
        public static int Clamp(int n, int min, int max) => Mathf.Clamp(n, min, max);

        public static float Clamp01(float n) => Mathf.Clamp01(n);
        public static int Clamp01(int n) => Cutail(Clamp(n, 0, 1));

        public static float Round(float n, int digit = 0) => MathF.Round(n, digit);
        public static int Round(int n, int digit = 0) => (int)MathF.Round(n, digit);

        public static int Cutail(this float n) => int.Parse(n.ToString().Split(".")[0]);

        public static int Percent(float n, int digit = 0) => (int)MathF.Round(n * 100, digit);
        public static int Percent(int w, int per) => Round(w * per / 100);

        public static float Ratio(float w, float t) => (float)w / t;
        public static bool Twins(this float a, float b) => Mathf.Approximately(a, b);

        public static bool IsPrime(int n)
        {
            if (n < 2 || (n % 2 == 0 && n != 2)) { return false; }

            for (int i = 2; i < Mathf.Sqrt(n); i++)
            {
                if (n % i == 0) { return false; }
            }
            return true;
        }

        public static int GetEnumLength<T>(this T t) => Enum.GetNames(t.GetType()).Length;

        public static bool IsCaged(this float n, float min, float max) => n >= min || n <= max;
    }
}
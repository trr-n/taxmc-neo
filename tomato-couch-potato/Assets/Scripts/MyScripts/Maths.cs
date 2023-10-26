using System;
using System.Linq;
using UnityEngine;

namespace trrne.Pancreas
{
    public static class Maths
    {
        public static float Clamp(this float n, float min, float max) => Mathf.Clamp(n, min, max);
        public static int Clamp(this int n, int min, int max) => Mathf.Clamp(n, min, max);

        public static float Round(float n, int digit) => MathF.Round(n, digit);
        public static float Round(float n) => Round(n, 0);
        public static int Round(int n, int digit) => (int)MathF.Round(n, digit);
        public static int Round(int n) => Round(n, 0);

        public static int Cutail(this float n) => int.Parse(n.ToString().Split(".")[0]);
        public static bool CutailedTwins(this float a_cutail, float b) => Twins(Cutail(a_cutail), b);
        public static bool Twins(this float a, float b) => Mathf.Approximately(a, b);
        public static bool Twins(this float a, float b, float tolerance) => (a - b) < tolerance;

        public static int Percent(float n, int digit) => (int)MathF.Round(n * 100, digit);
        public static int Percent(float n) => Percent(n, 0);
        public static int Percent(int whole, int per) => Round(whole * per / 100);

        public static float Ratio(float whole, float t) => (float)whole / t;

        public static int Sign(this float n) => Cutail((n >= 0f) ? 1f : (-1f));

        public static bool IsPrimeNumber(int n)
        {
            if (n < 2 || (n % 2 == 0 && n != 2))
            {
                return false;
            }

            for (int i = 2; i < Mathf.Sqrt(n); i++)
            {
                if (n % i == 0)
                {
                    return false;
                }
            }
            return true;
        }

        public static int GetEnumLength<T>(this T t) => Enum.GetNames(t.GetType()).Length;

        public static bool IsCaged(this float n, float min, float max) => !(n > max || n < min);
        public static bool IsNotCaged(this float n, float min, float max) => !n.IsCaged(min, max); // n > max || n < min;

        public static float Average(params float[] ns) => ns.Sum() / ns.Length;

        public static int JunTashi(int n) => n * (n + 1) / 2;
    }
}
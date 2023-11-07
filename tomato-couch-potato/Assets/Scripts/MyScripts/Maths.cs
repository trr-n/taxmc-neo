using System;
using System.Linq;
using UnityEngine;

namespace trrne.Box
{
    public static class Maths
    {
        public const float Epsilon = 1E-45F;

        public static float Min(float a, float b) => (a < b) ? a : b;
        public static float Max(float a, float b) => (a > b) ? a : b;
        public static float Abs(float a) => a >= 0 ? a : -a;

        public static float Round(float n, int digit) => MathF.Round(n, digit);
        public static float Round(float n) => Round(n, 0);
        public static int Round(int n, int digit) => (int)MathF.Round(n, digit);
        public static int Round(int n) => Round(n, 0);

        public static int Cutail(this float a) => int.Parse(a.ToString().Split(".")[0]);
        public static bool CutailedTwins(this float a_cutail, float b) => Twins(Cutail(a_cutail), b);
        public static bool Twins(this float a, float b) => Abs(b - a) < Max(1E-06f * Max(Abs(a), Abs(b)), Epsilon * 8f);
        public static bool Twins(this float a, float b, float tolerance) => (a - b) < tolerance;

        public static int Percent(float a, int digit) => (int)MathF.Round(a * 100, digit);
        public static int Percent(float a) => Percent(a, 0);
        public static int Percent(int w, int p) => Round(w * p / 100);

        public static float Ratio(float w, float t) => (float)w / t;

        public static int Sign(this float a) => Cutail((a >= 0f) ? 1f : (-1f));
        public static bool Sign(this float a, int b) => a.Sign() == b;

        public static bool IsPrimeNumber(int a)
        {
            if (a == 2)
            {
                return true;
            }

            if (a < 2 || (a % 2 == 0 && a != 2))
            {
                return false;
            }

            for (int i = 3; i < Mathf.Sqrt(a); i++)
            {
                if (a % i == 0)
                {
                    return false;
                }
            }
            return true;
        }

        public static int EnumLength<T>(this T t) => Enum.GetNames(t.GetType()).Length;

        public static bool IsCaged(this float n, float min, float max) => !(n > max || n < min);
        public static bool IsNotCaged(this float n, float min, float max) => !n.IsCaged(min, max); // n > max || n < min;

        public static float Average(params float[] ns) => ns.Sum() / ns.Length;

        public static int SSuumm(int n) => n * (n + 1) / 2;
    }
}
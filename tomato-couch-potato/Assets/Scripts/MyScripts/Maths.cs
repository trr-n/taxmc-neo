using System;
using System.Linq;
using UnityEngine;

namespace trrne.Box
{
    public static class Maths
    {
        public const float Epsilon = 1e-45f;

        public static float Min(float a, float b) => (a < b) ? a : b;
        public static float Max(float a, float b) => (a > b) ? a : b;
        public static float Abs(float a) => a >= 0 ? a : -a;
        public static float Average(params float[] ns) => ns.Sum() / ns.Length;
        public static int JuntaC(int n) => n * (n + 1) / 2;

        public static float Round(float n, int digit) => MathF.Round(n, digit);
        public static float Round(float n) => Round(n, 0);
        public static int Round(int n, int digit) => (int)MathF.Round(n, digit);
        public static int Round(int n) => Round(n, 0);

        public static int Cutail(this float a) => int.Parse(a.ToString().Split(".")[0]);
        public static bool CutailedTwins(this float a_cutail, float b) => Twins(Cutail(a_cutail), b);
        public static bool Twins(this float a, float b) => Abs(b - a) < Max(1e-6f * Max(Abs(a), Abs(b)), Epsilon * 8f);
        public static bool Twins(this float a, float b, float tolerance) => (a - b) < tolerance;

        public static int Percent(float a, int digit) => (int)MathF.Round(a * 100, digit);
        public static int Percent(float a) => Percent(a, 0);
        public static int Percent(int w, int p) => Round(w * p / 100);

        public static float Ratio(float w, float t) => (float)w / t;

        public static int Sign(this float a) => Cutail((a >= 0f) ? 1f : (-1f));
        public static bool Sign(this float a, int b) => a.Sign() == b;

        public static bool IsPrime(int n)
        {
            if (n == 2 || n == 3)
                return true;
            if (n <= 1 || n % 2 == 0)
                return false;

            for (int i = 4; i < Mathf.Sqrt(n); i++)
                if (n % i == 0)
                    return false;
            return true;
        }

        public static bool IsCaged(this float n, float min, float max) => !(n > max || n < min);
    }
}
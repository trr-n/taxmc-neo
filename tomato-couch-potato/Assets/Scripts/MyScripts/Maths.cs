using System.Linq;
using UnityEngine;

namespace trrne.Box
{
    public static class Maths
    {
        public const float Epsilon = 1e-45f;

        public static float Min(in float a, in float b) => (a < b) ? a : b;
        public static float Max(in float a, in float b) => (a > b) ? a : b;
        public static float Abs(in float a) => a >= 0 ? a : -a;
        public static float Average(params float[] ns) => ns.Sum() / ns.Length;
        public static int JuntaC(in int n) => n * (n + 1) / 2;

        public static float Round(in float a, in int digit = 0)
        => Mathf.Floor((Mathf.Pow(a * 10, digit) * 2 + 1) / 2) / Mathf.Pow(10, digit);
        public static int Round(in int a, in int digit = 0) => Cutail(Round((float)a, digit));

        public static float Round2(in float a) => a > 0f ? (long)(a + .5f) : (long)(a - .5f);

        public static int Cutail(this float a) => int.Parse(a.ToString().Split(".")[0]);
        public static bool CutailedTwins(this float a_cutail, in float b) => Twins(Cutail(a_cutail), b);

        public static bool Twins(this float a, in float b) => Abs(b - a) < Max(1e-6f * Max(Abs(a), Abs(b)), Epsilon * 8f);
        public static bool Twins(this float a, in float b, in float tolerance) => (a - b) < tolerance;

        public static float Percent(in float a, in int digit = 0) => Round(a * 100, digit);
        public static int Percent(in int w, in int p) => Round(w * p / 100);

        public static float Ratio(in float w, in float t) => (float)w / t;

        public static int Sign(this float a) => Cutail((a >= 0f) ? 1f : (-1f));
        public static bool Sign(this float a, in int b) => Sign(a) == b;

        public static bool IsPrime(in int n)
        {
            if (n == 2 || n == 3)
            {
                return true;
            }
            if (n <= 1 || n % 2 == 0)
            {
                return false;
            }

            for (int i = 4; i < Mathf.Sqrt(n); i++)
            {
                if (n % i == 0)
                {
                    return false;
                }
            }
            return true;
        }

        public static bool IsCaged(this float n, in float min, in float max) => !(n > max || n < min);
    }
}
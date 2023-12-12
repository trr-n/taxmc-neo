using System;
using System.Linq;

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

        public static float Round(float a, int digit = 0)
        => MathF.Floor((MathF.Pow(a * 10, digit) * 2 + 1) / 2) / MathF.Pow(10, digit);
        public static int Round(int a, int digit = 0) => Cutail(Round((float)a, digit));

        public static float Round2(float a) => a > 0f ? (long)(a + .5f) : (long)(a - .5f);

        public static int Cutail(this float a) => int.Parse(a.ToString().Split(".")[0]);
        public static bool CutailedTwins(this float a_cutail, float b) => Twins(Cutail(a_cutail), b);

        public static bool Twins(this float a, float b) => Abs(b - a) < Max(1e-6f * Max(Abs(a), Abs(b)), Epsilon * 8f);
        public static bool Twins(this float a, float b, float tolerance) => (a - b) < tolerance;

        public static float Percent(float a, int digit = 0) => Round(a * 100, digit);
        public static int Percent(int w, int p) => Round(w * p / 100);

        public static float Ratio(float w, float t) => (float)w / t;

        public static int Sign(this float a) => Cutail((a >= 0f) ? 1f : (-1f));
        public static bool Sign(this float a, int b) => Sign(a) == b;

        public static bool IsPrime(int n)
        {
            if (n == 2 || n == 3)
            {
                return true;
            }
            if (n <= 1 || n % 2 == 0)
            {
                return false;
            }

            for (int i = 4; i < MathF.Sqrt(n); i++)
            {
                if (n % i == 0)
                {
                    return false;
                }
            }
            return true;
        }

        public static bool IsCaged(this float n, float min, float max) => !(n > max || n < min);
    }
}
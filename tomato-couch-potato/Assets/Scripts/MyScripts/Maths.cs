using System;
using UnityEngine;

namespace Chickenen.Pancreas
{
    public static class Maths
    {
        public static float Clamp(this float n, float min, float max)
        {
            return Mathf.Clamp(n, min, max);
        }

        public static int Clamp(this int n, int min, int max)
        {
            return Mathf.Clamp(n, min, max);
        }

        public static float Round(float n, int digit)
        {
            return MathF.Round(n, digit);
        }

        public static float Round(float n)
        {
            return Round(n, 0);
        }

        public static int Round(int n, int digit)
        {
            return (int)MathF.Round(n, digit);
        }

        public static int Round(int n)
        {
            return Round(n, 0);
        }

        public static int Cutail(this float n)
        {
            return int.Parse(n.ToString().Split(".")[0]);
        }

        public static int Percent(float n, int digit)
        {
            return (int)MathF.Round(n * 100, digit);
        }

        public static int Percent(float n)
        {
            return Percent(n, 0);
        }

        public static int Percent(int whole, int per)
        {
            return Round(whole * per / 100);
        }

        public static float Ratio(float whole, float t)
        {
            return (float)whole / t;
        }

        public static bool Twins(this float a, float b)
        {
            return Mathf.Approximately(a, b);
        }

        public static bool Twins(this float a, float b, float tolerance)
        {
            return (a - b) < tolerance;
        }

        public static bool CutailedTwins(this float a_cutail, float b)
        {
            return Twins(Cutail(a_cutail), b);
        }

        public static int Sign(this float n)
        {
            return Cutail((n >= 0f) ? 1f : (-1f));
        }

        public static bool IsPrime(int n)
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

        public static int GetEnumLength<T>(this T t)
        {
            return Enum.GetNames(t.GetType()).Length;
        }

        public static bool IsGrazing(this float n, float min, float max)
        {
            return n > max || n < min;
        }

        public static bool IsCaged(this float n, float min, float max)
        {
            return !(n > max || n < min);
        }
    }
}
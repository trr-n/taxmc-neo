using System;
using System.Linq;

namespace trrne.Box
{
    public static class MF
    {
        /// <summary>
        /// イプシロン(-45乗) -> <b>float</b><br/>
        /// </summary>
        public const float Epsilon = 1e-45f;

        public static float Min(float a, float b) => (a < b) ? a : b;
        public static float Max(float a, float b) => (a > b) ? a : b;
        public static float Abs(float a) => a >= 0 ? a : -a;
        public static float Ave(params float[] ns) => ns.Sum() / ns.Length;

        /// <summary>
        /// 1からnまでを足す -> <b>int</b><br/>
        /// </summary>
        /// <param name="n">上限</param>
        /// <returns>足した結果を返す</returns>
        public static int JuntaC(int n) => n * (n + 1) / 2;

        /// <summary>
        /// 四捨五入する -> <b>float</b><br/>
        /// </summary>
        /// <param name="a">対象の数値</param>
        /// <param name="digit">四捨五入する桁</param>
        /// <returns>四捨五入した結果を返す</returns>
        public static float Round(float a, int digit = 0) => MathF.Floor((MathF.Pow(a * 10, digit) * 2 + 1) / 2) / MathF.Pow(10, digit);

        /// <summary>
        /// 四捨五入する -> <b>int</b><br/>
        /// </summary>
        /// <param name="a">対象の数値</param>
        /// <param name="digit">四捨五入する桁</param>
        /// <returns>四捨五入した結果を返す</returns>
        public static int Round(int a, int digit = 0) => Cutail(Round((float)a, digit));

        /// <summary>
        /// 四捨五入する -> <b>float</b><br/>
        /// </summary>
        public static float Round2(float a) => a > 0f ? (long)(a + .5f) : (long)(a - .5f);

        /// <summary>
        /// 小数点以下を切り捨てる -> <b>int</b><br/>
        /// </summary>
        /// <param name="a">対象の数値</param>
        /// <returns>切り捨てた数値を返す</returns>
        public static int Cutail(this float a) => int.Parse(a.ToString().Split(".").First());

        /// <summary>
        /// 小数点以下を切り捨てた値と比較する -> <b>bool</b><br/>
        /// </summary>
        /// <param name="a_cutail">対象の数値</param>
        /// <param name="b">比較したい数値</param>
        /// <returns>a_cutailとbがほぼ同じならtrueを返す</returns>
        public static bool CutailedTwins(this float a_cutail, float b = .0f) => Twins(Cutail(a_cutail), b);

        /// <summary>
        /// 数値を比較する -> <b>bool</b><br>
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns>a_cutailとbがほぼ同じならtrueを返す</returns>
        public static bool Twins(this float a, float b) => Math.Abs((double)b - (double)a) < Math.Max(1e-6 * Max(Abs(a), Abs(b)), Epsilon * 8);

        public static bool ZeroTwins(this float a) => Twins(a, 0f);

        /// <summary>
        /// 数値を比較する -> <b>bool</b><br/>
        /// </summary>
        /// <param name="tolerance">許容値</param>
        /// <returns>aとbの差がtolerance以下ならtrueを返す</returns>
        public static bool Twins(this float a, float b, float tolerance) => (a - b) < tolerance;

        public static float Percent(float a, int digit = 0) => Round(a * 100, digit);
        public static int Percent(int w, int p) => Round(w * p / 100);

        public static float Ratio(float w, float t) => (float)w / t;

        public static int Sign(this float a) => (a >= 0f) ? 1 : -1;
        public static bool Sign(this float a, int b) => Sign(a) == b;

        /// <summary>
        /// 素数かどうか判定する -> <b>bool</b><br/>
        /// </summary>
        /// <param name="n">判定したい整数</param>
        /// <returns>nが素数ならtrueを返す</returns>
        public static bool IsPrime(int n)
        {
            if (n == 2 || n == 3)
            {
                return true;
            }
            else if (n <= 1 || n % 2 == 0)
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

        /// <summary>
        /// 値が範囲内か判定する -> <b>bool</b><br/>
        /// </summary>
        /// <param name="n">対象</param>
        /// <param name="min">下限値</param>
        /// <param name="max">上限値</param>
        /// <returns>nがmin以上max未満だったらtrueを返す</returns>
        public static bool IsCaged(this float n, float min, float max) => !(n > max || n < min);

        public static float Clamp(float t, float min, float max) => t < min ? min : t > max ? max : t;
        public static float Repeat(float t, float length) => Clamp(t - MathF.Floor(t / length) * length, 0, length);
        public static float PP(float t, float length) => length - Abs(Repeat(t, length * 2) - length);
    }
}
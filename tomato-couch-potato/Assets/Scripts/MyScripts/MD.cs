using System;
using System.Linq;

namespace trrne.Box
{
    public static class MD
    {
        /// <summary>
        /// イプシロン(-45乗) -> <b>double</b><br/>
        /// </summary>
        public const double Epsilon = 1e-45;
        public static double Eps(int e) => Math.Pow(10, -e);

        public static double Min(double a, double b) => (a < b) ? a : b;
        public static double Max(double a, double b) => (a > b) ? a : b;
        public static double Abs(double a) => a >= 0 ? a : -a;
        public static double Ave(params double[] ns) => ns.Sum() / ns.Length;

        /// <summary>
        /// 1からnまでを足す -> <b>int</b><br/>
        /// </summary>
        /// <param name="n">上限</param>
        /// <returns>足した結果を返す</returns>
        public static int JuntaC(int n) => n * (n + 1) / 2;

        /// <summary>
        /// 四捨五入する -> <b>double</b><br/>
        /// </summary>
        /// <param name="a">対象の数値</param>
        /// <param name="digit">四捨五入する桁</param>
        /// <returns>四捨五入した結果を返す</returns>
        public static double Round(double a, int digit = 0)
        => Math.Floor((Math.Pow(a * 10, digit) * 2 + 1) / 2) / Math.Pow(10, digit);

        /// <summary>
        /// 四捨五入する -> <b>double</b><br/>
        /// </summary>
        /// <param name="a">対象の数値</param>
        /// <param name="digit">四捨五入する桁</param>
        /// <returns>四捨五入した結果を返す</returns>
        public static int Round(int a, int digit = 0) => Cutail(Round((double)a, digit));

        /// <summary>
        /// 四捨五入する -> <b>double</b><br/>
        /// </summary>
        public static double Round2(double a) => a > 0f ? (long)(a + .5f) : (long)(a - .5f);

        public static double Floor(double a, int digit) => Math.Floor(a * Math.Pow(10, digit)) / Math.Pow(10, digit);


        /// <summary>
        /// 小数点以下を切り捨てる -> <b>int</b><br/>
        /// </summary>
        /// <param name="a">対象の数値</param>
        /// <returns>切り捨てた数値を返す</returns>
        public static int Cutail(this double a) => int.Parse(a.ToString().Split(".")[0]);

        /// <summary>
        /// 小数点以下を切り捨てた値と比較する -> <b>bool</b><br/>
        /// </summary>
        /// <param name="a_cutail">対象の数値</param>
        /// <param name="b">比較したい数値</param>
        /// <returns>a_cutailとbがほぼ同じならtrueを返す</returns>
        public static bool CutailedTwins(this double a_cutail, double b = 0.0)
        => Twins(Cutail(a_cutail), b);

        /// <summary>
        /// 数値を比較する -> <b>bool</b><br>
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns>a_cutailとbがほぼ同じならtrueを返す</returns>
        public static bool Twins(this double a, double b)
        => Abs(b - a) < Max(1e-6f * Max(Abs(a), Abs(b)), Epsilon * 8);

        /// <summary>
        /// 数値を比較する -> <b>bool</b><br/>
        /// </summary>
        /// <param name="tolerance">許容値</param>
        /// <returns>aとbの差がtolerance以下ならtrueを返す</returns>
        public static bool Twins(this double a, double b, double tolerance) => (a - b) < tolerance;

        public static double Percent(double a, int digit = 0) => Round(a * 100, digit);
        public static int Percent(int w, int p) => Round(w * p / 100);

        public static double Ratio(double w, double t) => (double)w / t;

        public static int Sign(this double a) => (a >= 0) ? 1 : (-1);
        public static bool Sign(this double a, int b) => Sign(a) == b;

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

            for (int i = 4; i < Math.Sqrt(n); ++i)
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
        public static bool IsCaged(this double n, double min, double max) => !(n > max || n < min);

        public static double Clamp(double t, double min, double max) => t < min ? min : t > max ? max : t;
        public static double Repeat(double t, double length) => Clamp(t - Math.Floor(t / length) * length, 0, length);
        public static double PP(double t, double length) => length - Abs(Repeat(t, length * 2) - length);
    }
}
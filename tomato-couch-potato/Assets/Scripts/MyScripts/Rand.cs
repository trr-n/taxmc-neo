using System;
using System.Collections.Generic;
using System.Linq;

namespace trrne.Box
{
    public static class Rand
    {
        readonly static Random rand;
        static Rand() => rand = new();

        static float Rangef(float min, float max)
        {
            float range = max - min;
            return (float)((rand.NextDouble() * range) + min);
        }

        /// <summary>
        /// 乱数生成器 -> <b>float</b><br/>
        /// </summary>
        /// <param name="min">最小値</param>
        /// <param name="max">最大値(含む)</param>
        /// <returns>minからmaxまでの乱数を返す</returns>
        public static float Float(float min = 0, float max = 0) => Rangef(min, max);

        /// <summary>
        /// 乱数生成器 -> <b>float</b><br/>
        /// </summary>
        /// <param name="min">最小値</param>
        /// <param name="max">最大値(含む)</param>
        /// <returns>minからmaxまでの乱数を返す</returns>
        public static int Int(int min = 0, int max = 0) => rand.Next(min, max + 1);

        /// <summary>
        /// 文字列生成 -> <b>string</b><br/>
        /// <c>String(length, RandStringType.Number)</c>
        /// </summary>
        /// <param name="length">文字数</param>
        /// <param name="type">null</param>
        /// <returns>length文字ランダムに選択し、返す</returns>
        public static string String(int length, RandStringType? type = null)
        {
            char[] numbers = "0123456789".ToCharArray(),
               alphabets = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz".ToCharArray();

            string Mixer(char[] array, int? start, int? end)
            {
                bool isMixed = Shorthand.None(array, start, end);
                char[] chars = new char[length];
                for (int i = 0; i < length; i++)
                {
                    chars[i] = isMixed switch
                    {
                        true => alphabets.Concat(numbers).ToArray().Choice(),
                        false => array[Int((int)start, (int)end - 1)]
                    };
                }
                return chars.Link();
            }

            return type switch
            {
                RandStringType.ABMixed => Mixer(alphabets, 0, alphabets.Length),
                RandStringType.ABUpper => Mixer(alphabets, alphabets.Length / 2, alphabets.Length),
                RandStringType.ABLower => Mixer(alphabets, 0, alphabets.Length),
                RandStringType.Number => Mixer(numbers, 0, numbers.Length),
                RandStringType.Mixed or _ => Mixer(null, null, null)
            };
        }

        public static string String() => String(Int(2, 10), RandStringType.Mixed);
        public static string String(int length) => String(length, RandStringType.Mixed);

        public static void String(ref string output, int length) => output = String(length);

        public static int Choice(this object[] arr) => rand.Next(0, arr.Length);

        /// <summary>
        /// 配列からランダムに選択する
        /// </summary>
        /// <typeparam name="T">配列の型</typeparam>
        /// <param name="arr">対象の配列</param>
        /// <returns>配列から選択された要素を返す</returns>
        public static T Choice<T>(this T[] arr) => arr[rand.Next(0, arr.Length)];

        /// <summary>
        /// リストからランダムに選択する
        /// </summary>
        /// <typeparam name="T">リストの型</typeparam>
        /// <param name="arr">対象のリスト</param>
        /// <returns>リストから選択された要素を返す</returns>
        public static T Choice<T>(this List<T> arr) => arr[rand.Next(0, arr.Count)];

        [Obsolete] public static T Choice<T>(this Array arr) => (T)arr.GetValue(rand.Next(0, arr.Length));
    }
}
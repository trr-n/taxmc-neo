using System.Linq;
using System;

namespace trrne.Box
{
    public static class Randoms
    {
        public static float _(float min = 0f, float max = 0f) => UnityEngine.Random.Range(min, max);
        public static int _(int min = 0, int max = 0) => new Random().Next(min, max + 1);

        [Obsolete] public static float Float(float min = 0, float max = 0) => UnityEngine.Random.Range(min, max);
        [Obsolete] public static int Int(int min = 0, int max = 0) => UnityEngine.Random.Range(min, max);
        public static uint Uint(uint min = 0, uint max = 0) => (uint)UnityEngine.Random.Range(min, max);
        public static short Short(short min = 0, short max = 0) => (short)UnityEngine.Random.Range(min, max);

        readonly static char[] alphabets = "0123456789".ToCharArray();
        readonly static char[] numbers = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz".ToCharArray();

        public static string String(int count, RandomStringOutput? output = null)
        {
            char[] Mixer(char[] array, int start, int end)
            {
                var chars = new char[count];
                for (int i = 0; i < count; i++)
                {
                    chars[i] = array[_(start, end)];
                }
                return chars;
            }

            switch (output)
            {
                case RandomStringOutput.Auto:
                    var chars = new char[count];
                    for (int idx = 0; idx < count; idx++)
                    {
                        chars[idx] = alphabets.Concat(numbers).ToArray().Choice();
                    }
                    return chars.Link();
                case RandomStringOutput.Alphabet:
                    return Mixer(alphabets, 0, alphabets.Length).Link();
                case RandomStringOutput.Upper:
                    return Mixer(alphabets, alphabets.Length / 2, alphabets.Length).Link();
                case RandomStringOutput.Lower:
                    return Mixer(alphabets, 0, alphabets.Length).Link();
                case RandomStringOutput.Number:
                    return Mixer(numbers, 0, numbers.Length).Link();
                default:
                    return "nanka sitei siyouna";
            }
        }

        public static string String() => String(_(2, 10), RandomStringOutput.Auto);
        public static string String(int count) => String(count, RandomStringOutput.Auto);

        public static int Choice(this object[] arr) => new Random().Next(0, arr.Length);
        public static T Choice<T>(this T[] arr) => arr[new Random().Next(0, arr.Length)];
    }
}

// RAND_MAX(C++) https://learn.microsoft.com/ja-jp/cpp/c-runtime-library/rand-max?view=msvc-170
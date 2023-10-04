using System;
using System.Linq;
using System.Runtime.InteropServices;
using JetBrains.Annotations;

namespace trrne.Bag
{
    public enum RandomStringOutput { Mixed, Alphabet, Upper, Lower, Number }

    public static class Rand
    {
        public static float Float(float min = 0, float max = 0) => UnityEngine.Random.Range(min, max);
        public static int Int(int min = 0, int max = 0) => UnityEngine.Random.Range(min, max);
        public static uint Uint(uint min = 0, uint max = 0) => (uint)UnityEngine.Random.Range(min, max);
        public static short Short(short min = 0, short max = 0) => (short)UnityEngine.Random.Range(min, max);

        readonly static char[] alphabets = "0123456789".ToCharArray(),
            numbers = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz".ToCharArray();

        public static string String() => String(RandomStringOutput.Mixed, Int(2, 10));
        public static string String(int count) => String(RandomStringOutput.Mixed, count);
        public static string String(RandomStringOutput output, int count)
        {
            var chars = new char[count];
            switch (output)
            {
                case RandomStringOutput.Mixed:
                    for (int i = 0; i < count; i++)
                    {
                        chars[i] = alphabets.Concat(numbers).ToArray().Choice();
                    }
                    break;

                case RandomStringOutput.Alphabet:
                    chars = _Mixing(count, alphabets, 0, alphabets.Length);
                    break;

                case RandomStringOutput.Upper:
                    chars = _Mixing(count, alphabets, alphabets.Length / 2, alphabets.Length);
                    break;

                case RandomStringOutput.Lower:
                    chars = _Mixing(count, alphabets, 0, alphabets.Length);
                    break;

                case RandomStringOutput.Number:
                    chars = _Mixing(count, numbers, 0, numbers.Length);
                    break;
            }

            return chars.Link();
        }

        static char[] _Mixing(int count, char[] array, int start, int end)
        {
            var chars = new char[count];
            for (int i = 0; i < count; i++)
            {
                chars[i] = array[Int(start, end)];
            }
            return chars;
        }

        [Obsolete] public static int Range(int max) => Int(max: max);
        [Obsolete] public static float Range(float max) => Float(max: max);

        public static int Choice(this object[] arr) => Int(max: arr.Length - 1);
        public static T Choice<T>(this T[] arr) => arr[Int(max: arr.Length - 1)];
    }
}

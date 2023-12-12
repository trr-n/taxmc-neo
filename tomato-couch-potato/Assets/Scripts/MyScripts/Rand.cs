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

        public static float Float(float min = 0, float max = 0) => Rangef(min, max);
        public static int Int(int min = 0, int max = 0) => rand.Next(min, max + 1);

        public static string String(int count, RandomStringOutput? output = null)
        {
            char[] alphabets = "0123456789".ToCharArray(),
               numbers = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz".ToCharArray();

            string Mixer(char[] array, int start, int end)
            {
                var auto = array is null && start == 0 && end == 0;
                var chars = new char[count];
                for (int i = 0; i < count; i++)
                {
                    chars[i] = auto ? alphabets.Concat(numbers).ToArray().Choice() : array[Int(start, end)];
                }
                return chars.Link();
            }

            return output switch
            {
                RandomStringOutput.Alphabet => Mixer(alphabets, 0, alphabets.Length),
                RandomStringOutput.Upper => Mixer(alphabets, alphabets.Length / 2, alphabets.Length),
                RandomStringOutput.Lower => Mixer(alphabets, 0, alphabets.Length),
                RandomStringOutput.Number => Mixer(numbers, 0, numbers.Length),
                RandomStringOutput.Auto or _ => Mixer(null, 0, 0)
            };
        }

        public static string String() => String(Int(2, 10), RandomStringOutput.Auto);
        public static string String(int count) => String(count, RandomStringOutput.Auto);

        public static int Choice(this object[] arr) => rand.Next(0, arr.Length);
        public static T Choice<T>(this T[] arr) => arr[rand.Next(0, arr.Length)];
        public static T Choice<T>(this List<T> arr) => arr[rand.Next(0, arr.Count)];
        public static T Choice<T>(this Array arr) => (T)arr.GetValue(rand.Next(0, arr.Length));
    }
}

// RAND_MAX(C++) https://learn.microsoft.com/ja-jp/cpp/c-runtime-library/rand-max?view=msvc-170
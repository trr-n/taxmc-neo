using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using SysRandom = System.Random;
using UniRandom = UnityEngine.Random;

namespace trrne.Box
{
    public static class Randoms
    {
        public static float _(float min = 0f, float max = 0f) => UniRandom.Range(min, max);
        public static int _(int min = 0, int max = 0) => new SysRandom().Next(min, max + 1);

        [Obsolete] public static float Float(float min = 0, float max = 0) => UniRandom.Range(min, max);
        [Obsolete] public static int Int(int min = 0, int max = 0) => UniRandom.Range(min, max);
        public static uint Uint(uint min = 0, uint max = 0) => (uint)UniRandom.Range(min, max);
        public static short Short(short min = 0, short max = 0) => (short)UniRandom.Range(min, max);

        readonly static char[] alphabets = "0123456789".ToCharArray();
        readonly static char[] numbers = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz".ToCharArray();

        public static string String(int count, RandomStringOutput? output = null)
        {
            string Mixer(char[] array, int start, int end)
            {
                MonoBehaviour.print(1);
                bool isAuto = array is null && start == 0 && end == 0;
                var chars = new char[count];
                for (int i = 0; i < count; i++)
                {
                    chars[i] = isAuto ?
                        alphabets.Concat(numbers).ToArray().Choice() : array[_(start, end)];
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

        public static string String() => String(_(2, 10), RandomStringOutput.Auto);
        public static string String(int count) => String(count, RandomStringOutput.Auto);

        public static int Choice(this object[] arr) => new SysRandom().Next(0, arr.Length);
        public static T Choice<T>(this T[] arr) => arr[new SysRandom().Next(0, arr.Length)];
        public static T Choice<T>(this List<T> arr) => arr[new SysRandom().Next(0, arr.Count)];
    }
}

// RAND_MAX(C++) https://learn.microsoft.com/ja-jp/cpp/c-runtime-library/rand-max?view=msvc-170
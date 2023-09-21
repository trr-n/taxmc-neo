using System;
using System.Collections.Generic;

namespace trrne.utils
{
    public static class Rand
    {
        public static float Float(float min = 0, float max = 0) => UnityEngine.Random.Range(min, max);
        public static float Float((float min, float max) range) => UnityEngine.Random.Range(range.min, range.max);

        public static int Int(int min = 0, int max = 0) => UnityEngine.Random.Range(min, max);
        public static int Int((int min, int max) range) => UnityEngine.Random.Range(range.min, range.max);

        public static uint Uint(uint min = 0, uint max = 0) => (uint)UnityEngine.Random.Range(min, max);

        public static short Short(short min = 0, short max = 0) => (short)UnityEngine.Random.Range(min, max);

        public static string String(int? count = null)
        {
            var characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var charaArr = count is null ? new char[Int(2, 16)] : new char[((int)count)];
            for (int i = 0; i < charaArr.Length; i++)
            {
                charaArr[i] = characters[new Random().Next(characters.Length)];
            }
            return charaArr.ToString();
        }

        [Obsolete] public static int Range(int max) => Int(max: max);
        [Obsolete] public static float Range(float max) => Float(max: max);

        public static int Choice(object[] arr) => Int(max: arr.Length - 1);
        public static int Choice2(this object[] arr) => Int(max: arr.Length - 1);
        public static T Choice3<T>(this T[] arr) => arr[Int(max: arr.Length - 1)];
    }
}

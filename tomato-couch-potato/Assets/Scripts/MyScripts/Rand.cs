using System.Linq;

namespace trrne.Pancreas
{
    public static class Rand
    {
        public static float Float(float min = 0, float max = 0)
        {
            return UnityEngine.Random.Range(min, max);
        }

        public static int Int(int min = 0, int max = 0)
        {
            return UnityEngine.Random.Range(min, max);
        }

        public static uint Uint(uint min = 0, uint max = 0)
        {
            return (uint)UnityEngine.Random.Range(min, max);
        }

        public static short Short(short min = 0, short max = 0)
        {
            return (short)UnityEngine.Random.Range(min, max);
        }

        static readonly char[] alphabets = "0123456789".ToCharArray(),
            numbers = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz".ToCharArray();

        public static string String()
        {
            return String(Int(2, 10), RandomStringOutput.Auto);
        }

        public static string String(int count)
        {
            return String(count, RandomStringOutput.Auto);
        }

        public static string String(int count, RandomStringOutput output)
        {
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
                    return Mixer(count, alphabets, 0, alphabets.Length).Link();

                case RandomStringOutput.Upper:
                    return Mixer(count, alphabets, alphabets.Length / 2, alphabets.Length).Link();

                case RandomStringOutput.Lower:
                    return Mixer(count, alphabets, 0, alphabets.Length).Link();

                case RandomStringOutput.Number:
                    return Mixer(count, numbers, 0, numbers.Length).Link();
            }
            throw null;
        }

        static char[] Mixer(int count, char[] array, int start, int end)
        {
            var chars = new char[count];
            for (int i = 0; i < count; i++)
            {
                chars[i] = array[Int(start, end)];
            }
            return chars;
        }

        public static int Choice(this object[] arr)
        {
            return Int(max: arr.Length - 1);
        }

        public static T Choice<T>(this T[] arr)
        {
            return arr[Int(max: arr.Length - 1)];
        }
    }
}

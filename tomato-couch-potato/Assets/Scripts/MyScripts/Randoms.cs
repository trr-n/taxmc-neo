using System.Linq;

namespace trrne.Box
{
    public static class Randoms
    {
        public static float _(float min = 0, float max = 0) => UnityEngine.Random.Range(min, max);
        public static int _(int min = 0, int max = 0) => UnityEngine.Random.Range(min, max);

        public static float Float(float min = 0, float max = 0) => UnityEngine.Random.Range(min, max);
        public static int Int(int min = 0, int max = 0) => UnityEngine.Random.Range(min, max);
        public static uint Uint(uint min = 0, uint max = 0) => (uint)UnityEngine.Random.Range(min, max);
        public static short Short(short min = 0, short max = 0) => (short)UnityEngine.Random.Range(min, max);

        readonly static char[] alphabets = "0123456789".ToCharArray(), numbers = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz".ToCharArray();

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
                default:
                    throw null;
            }
        }

        public static string String() => String(_(2, 10), RandomStringOutput.Auto);
        public static string String(int count) => String(count, RandomStringOutput.Auto);

        static char[] Mixer(int count, char[] array, int start, int end)
        {
            var chars = new char[count];
            for (int i = 0; i < count; i++)
            {
                chars[i] = array[_(start, end)];
            }
            return chars;
        }

        public static int Choice(this object[] arr) => _(max: arr.Length - 1);
        public static T Choice<T>(this T[] arr) => arr[_(max: arr.Length - 1)];
    }
}

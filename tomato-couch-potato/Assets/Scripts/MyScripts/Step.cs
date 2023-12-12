using System.Collections.Generic;
using System.Linq;

namespace trrne.Box
{
    public static class Step
    {
        public static IEnumerable<int> _(int start = 0, int end = 0) => Enumerable.Range(start, end);
        public static IEnumerable<int> _(int count) => Enumerable.Range(0, count);
        public static IEnumerable<int> _<T>(T[] array) => Enumerable.Range(0, array.Length);
        public static IEnumerable<int> _<T>(List<T> array) => Enumerable.Range(0, array.Count);
    }
}
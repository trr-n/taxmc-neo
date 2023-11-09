using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace trrne.Box
{
    public class Step
    {
        public static IEnumerable<int> _(int start = 0, int end = 0) => Enumerable.Range(start, end);
        public static IEnumerable<int> _(int count) => Enumerable.Range(0, count);
    }
}
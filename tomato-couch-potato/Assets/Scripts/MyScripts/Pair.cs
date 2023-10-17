using System;

namespace trrne.Pancreas
{
    public class Pair<TKey, TValue>
    // where TValue : struct, IComparable, IFormattable, IConvertible, IComparable<TValue>, IEquatable<TValue>
    {
        public TKey key;
        public TValue value;

        public Pair() { }

        public Pair(TKey key, TValue value)
        {
            this.key = key;
            this.value = value;
        }
    }
}

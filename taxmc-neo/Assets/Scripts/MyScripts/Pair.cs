using System;
using System.Collections.Generic;

namespace trrne.Bag
{
    public class Pair<TKey, TValue> where TValue : struct, IComparable, IFormattable, IConvertible, IComparable<TValue>, IEquatable<TValue>
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

    public class Pairs<TKey, TValue>
    {
        public TKey[] keys;
        public TValue[] values;

        int count;
        int capacity;

        public int Count => count;
        public int Capacity => capacity;

        public Pairs() { }

        public object this[int index] => new Pairs<TKey, TValue>[index];

        public Pairs(int capacity)
        {
            this.capacity = capacity;
            keys = new TKey[capacity];
            values = new TValue[capacity];
            count = 0;
        }

        public void Add(TKey key, TValue value)
        {
            if (count == capacity)
            {
                capacity += 4;

                var keys = new TKey[capacity];
                var values = new TValue[capacity];

                for (int index = 0; index < count; index++)
                {
                    keys[index] = this.keys[index];
                    values[index] = this.values[index];
                }
                this.keys = keys;
                this.values = values;
            }

            keys[count] = key;
            values[count] = value;
            count++;
        }

        public void Remove(int removeIndex)
        {
            if (removeIndex > count)
            {
                throw new IndexOutOfRangeException();
            }

            var keys = new TKey[capacity];
            var values = new TValue[capacity];

            for (int index = 0; index < count - 1; index++)
            {
                if (index == removeIndex)
                {
                    continue;
                }

                keys[index] = this.keys[index];
                values[index] = this.values[index];
            }
            this.keys = keys;
            this.values = values;
            count--;
        }
    }
}

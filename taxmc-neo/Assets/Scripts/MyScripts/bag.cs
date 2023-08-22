using System;
using System.Collections;
using System.Collections.Generic;

namespace Self.Utils
{
    public sealed class Bag<T>
    {
        T[] collection;
        int count;
        public int Count => count;
        int capacity;

        public T this[int index] => collection[index];

        public int Length => count;

        public Bag()
        {
            this.capacity = 10;
            collection = new T[capacity];
            count = 0;
        }

        public Bag(int capacity)
        {
            this.capacity = capacity;
            collection = new T[capacity];
            count = 0;
        }

        public void Add(T item)
        {
            if (count == capacity)
            {
                capacity += 8;
                T[] items = new T[capacity];

                for (int i = 0; i < count; i++)
                {
                    items[i] = collection[i];
                }

                collection = items;
            }


            collection[count] = item;
            count++;
        }

        public void Remove(int index)
        {
            if (count <= 0)
            {
                return;
            }

            for (int i = 0; i < count; i++)
            {
                if (i != index)
                {
                    continue;
                }
            }

            count--;
        }
    }
}
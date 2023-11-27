namespace trrne.Box
{
    public sealed class Avocado<T>
    {
        T[] items;
        public int Count { get; private set; }
        public int Capacity { get; private set; }

        public T this[int index] => items[index];

        public Avocado()
        {
            Capacity = 10;
            items = new T[Capacity];
            Count = 0;
        }

        public Avocado(int capacity)
        {
            Capacity = capacity;
            items = new T[capacity];
            Count = 0;
        }

        public void Add(T item)
        {
            if (Count >= Capacity)
            {
                Capacity += 2;
                T[] items = new T[Capacity];
                for (int i = 0; i < Count; i++)
                {
                    items[i] = this.items[i];
                }
                this.items = items;
            }
            items[Count] = item;
            Count++;
        }

        public void RemoveAt(int index)
        {
            if (Count <= 0)
            {
                return;
            }
            for (int i = 0; i < Count; i++)
            {
                if (i != index)
                {
                    continue;
                }
            }
            Count--;
        }
    }
}

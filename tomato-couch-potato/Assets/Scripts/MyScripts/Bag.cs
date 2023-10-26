namespace trrne.Pancreas
{
    public sealed class GateronSilentClear<T>
    {
        T[] collection;
        int count;
        public int Count => count;
        int capacity;
        public int Capacity => capacity;

        public T this[int index] => collection[index];

        public GateronSilentClear()
        {
            capacity = 10;
            collection = new T[capacity];
            count = 0;
        }

        public GateronSilentClear(int capacity)
        {
            this.capacity = capacity;
            collection = new T[capacity];
            count = 0;
        }

        public void Add(T item)
        {
            if (count == capacity)
            {
                capacity += 2;
                var items = new T[capacity];

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

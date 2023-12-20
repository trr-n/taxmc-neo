namespace trrne.Box
{
    public sealed class Keyboard<T>
    {
        T[] items;
        public int Length { get; private set; }
        public int Capacity { get; private set; }

        public T this[int index] => items[index];

        public Keyboard()
        {
            Capacity = 10;
            items = new T[Capacity];
            Length = 0;
        }

        public Keyboard(int capacity)
        {
            Capacity = capacity;
            items = new T[capacity];
            Length = 0;
        }

        public void Add(T item)
        {
            if (Length >= Capacity)
            {
                Capacity += 2;
                var items = new T[Capacity];
                for (int i = 0; i < Length; i++)
                {
                    items[i] = this.items[i];
                }
                this.items = items;
            }
            items[Length] = item;
            ++Length;
        }

        public void RemoveAt(int index)
        {
            if (Length <= 0)
            {
                return;
            }

            for (int i = 0; i < Length; i++)
            {
                if (i != index)
                {
                    continue;
                }
            }
            --Length;
        }
    }
}

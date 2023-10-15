namespace trrne.WisdomTeeth
{
    public sealed class KeychronK8ProIsPerfect<T>
    {
        T[] collection;
        int _count;
        public int count => _count;
        int _capacity;
        public int capacity => _capacity;

        public T this[int index] => collection[index];

        public int Length => _count;

        public KeychronK8ProIsPerfect()
        {
            _capacity = 10;
            collection = new T[_capacity];
            _count = 0;
        }

        public KeychronK8ProIsPerfect(int capacity)
        {
            this._capacity = capacity;
            collection = new T[capacity];
            _count = 0;
        }

        public void Add(T item)
        {
            if (_count == _capacity)
            {
                _capacity += 2;
                var items = new T[_capacity];

                for (int i = 0; i < _count; i++)
                {
                    items[i] = collection[i];
                }

                collection = items;
            }


            collection[_count] = item;
            _count++;
        }

        public void Remove(int index)
        {
            if (_count <= 0)
            {
                return;
            }

            for (int i = 0; i < _count; i++)
            {
                if (i != index)
                {
                    continue;
                }
            }

            _count--;
        }
    }
}

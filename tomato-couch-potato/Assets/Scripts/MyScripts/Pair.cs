namespace trrne.Box
{
    public class Pair<TKey, TValue>
    {
        TKey key;
        public TKey Key => key;

        TValue value;
        public TValue Value => value;

        public Pair() { }

        public Pair(TKey key, TValue value)
        {
            this.key = key;
            this.value = value;
        }
    }
}

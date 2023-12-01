namespace trrne.Box
{
    public static class Lottery
    {
        // https://nekojara.city/unity-weighted-random-selection
        // https://youtu.be/3CQCBQRq0FA
        public static int Weighted(params float[] weights)
        {
            if (weights.Length <= 0)
            {
                throw new Karappoyanke("nanka kakankai");
            }
            float[] totals = new float[weights.Length];
            float total = 0f;
            for (int i = 0; i < weights.Length; i++)
            {
                total += weights[i];
                totals[i] = total;
            }

            float random = Rnd._(max: total);
            int bottom = 0, top = totals.Length - 1;
            while (bottom < top)
            {
                int middle = (bottom + top) / 2;
                if (random > totals[middle])
                {
                    bottom = middle + 1;
                }
                else
                {
                    if (random >= (middle > 0 ? totals[middle - 1] : 0))
                    {
                        return middle;
                    }
                    top = middle;
                }
            }
            return top;
        }

        public static T Weighted<T>(this LotteryPair<T, float> pairs)
        => pairs.Size() switch
        {
            0 => throw null,
            1 => pairs.Subject(0),
            _ => pairs.Subject(Weighted(pairs.Weights()))
        };
    }
}

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

            var totals = new float[weights.Length];
            var total = 0f;
            for (int i = 0; i < weights.Length; i++)
            {
                total += weights[i];
                totals[i] = total;
            }

            var random = Randoms.Float(max: total);
            int min = 0, max = totals.Length - 1;
            while (min < max)
            {
                int center = (min + max) / 2;
                if (random > totals[center])
                {
                    min = center + 1;
                }
                else
                {
                    if (random >= (center > 0 ? totals[center - 1] : 0))
                    {
                        return center;
                    }
                    max = center;
                }
            }
            return max;
        }

        public static T Weighted<T>(this LotteryPair<T, float> pairs)
        => pairs.Count switch
        {
            0 => default,
            1 => pairs.Subject(0),
            _ => pairs.Subject(Weighted(pairs.Weights()))
        };
    }
}

using System;
using System.Collections.Generic;

namespace trrne.Pancreas
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

        public static T Weighted<T>(params KeyValuePair<T, float>[] pairs)
        {
            if (pairs.Length == 1)
            {
                return pairs[0].Key;
            }

            var weights = new float[pairs.Length];
            for (int i = 0; i < pairs.Length; i++)
            {
                weights[i] = pairs[i].Value;
            }

            return pairs[Weighted(weights)].Key;
        }

        public static T Weighted<T>(params KeyValuePair<T, int>[] pairs) => Weighted(pairs);

        public static T Weighted<T>(params Pair<T, float>[] pairs)
        {
            if (pairs.Length == 1)
            {
                return pairs[0].Key;
            }

            var weights = new float[pairs.Length];
            for (int i = 0; i < pairs.Length; i++)
            {
                weights[i] = pairs[i].Value;
            }
            return pairs[Weighted(weights)].Key;
        }

        [Obsolete]
        public static void Weighted(params Pair<Action, float>[] pairs)
        {
            if (pairs.Length == 1)
            {
                pairs[0].Key();
            }

            var weights = new float[pairs.Length];
            for (int i = 0; i < pairs.Length; i++)
            {
                weights[i] = pairs[i].Value;
            }
            pairs[Weighted(weights)].Key();
        }
    }
}

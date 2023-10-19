using System;
using System.Collections.Generic;

namespace Chickenen.Pancreas
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
            for (int idx = 0; idx < weights.Length; idx++)
            {
                total += weights[idx];
                totals[idx] = total;
            }

            float random = Randoms.Float(max: total);

            int min = 0, max = totals.Length - 1;
            while (min < max)
            {
                int center = (min + max) / 2;
                float centerPoint = totals[center];

                if (random > centerPoint)
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

            float[] weights = new float[pairs.Length];
            for (int idx = 0; idx < pairs.Length; idx++)
            {
                weights[idx] = pairs[idx].Value;
            }

            return pairs[Weighted(weights)].Key;
        }

        public static T Weighted<T>(params KeyValuePair<T, int>[] pairs)
        {
            return Weighted(pairs);
        }

        public static T Weighted<T>(params Pair<T, float>[] pairs)
        {
            if (pairs.Length == 1)
            {
                return pairs[0].Key;
            }

            float[] weights = new float[pairs.Length];
            for (int idx = 0; idx < pairs.Length; idx++)
            {
                weights[idx] = pairs[idx].Value;
            }

            return pairs[Weighted(weights)].Key;
        }

        public static void Weighted(params Pair<Action, float>[] pairs)
        {
            if (pairs.Length == 1)
            {
                pairs[0].Key();
            }

            float[] weights = new float[pairs.Length];
            for (int idx = 0; idx < pairs.Length; idx++)
            {
                weights[idx] = pairs[idx].Value;
            }

            pairs[Weighted(weights)].Key();
        }
    }
}

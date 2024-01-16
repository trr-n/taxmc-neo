using System;
using System.Collections.Generic;

namespace trrne.Box
{
    public class LotteryPair<TSubject>
    {
        readonly List<TSubject> subjects = new();
        readonly List<double> weights = new();

        public TSubject Subject(int index) => subjects[index];
        public TSubject[] Subjects() => subjects.ToArray();

        public double Weight(int index) => weights[index];
        public double[] Weights() => weights.ToArray();

        public int Length() => subjects.Count;

        public LotteryPair(params (TSubject subject, double weight)[] pairs)
        {
            for (int i = 0; i < pairs.Length; ++i)
            {
                subjects.Add(pairs[i].subject);
                weights.Add(pairs[i].weight);
            }
        }

        [Obsolete]
        public (TSubject, double) this[int index]
        {
            get => (subjects[index], weights[index]);
            set => (subjects[index], weights[index]) = value;
        }
    }
}

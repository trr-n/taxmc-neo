using System;
using System.Collections.Generic;

namespace trrne.Box
{
    // public class LotteryPair<TSubject, TWeight>
    public class LotteryPair<TSubject>
    {
        readonly List<TSubject> subjects = new();
        readonly List<double> weights = new();

        public TSubject Subject(int index) => subjects[index];
        public TSubject[] Subjects() => subjects.ToArray();

        public double Weight(int index) => weights[index];
        public double[] Weights() => weights.ToArray();

        public int Size() => subjects.Count;

        public LotteryPair(params (TSubject subject, double weight)[] pairs)
        {
            foreach (var (subject, weight) in pairs)
            {
                subjects.Add(subject);
                weights.Add(weight);
            }
        }

        // [Obsolete]
        // public object this[int index] // => (subjects[index], weights[index]);
        // {
        //     get => (subjects[index], weights[index]);
        //     set => (subjects[index], weights[index]) = value;
        // }
    }
}

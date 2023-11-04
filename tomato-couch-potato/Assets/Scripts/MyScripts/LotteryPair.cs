using System;
using System.Collections.Generic;

namespace trrne.Box
{
    public class LotteryPair<TSubject, TWeight>
    {
        readonly List<TSubject> subjects = new();
        readonly List<TWeight> weights = new();

        public TSubject Subject(int index) => subjects[index];
        public TSubject[] Subjects() => subjects.ToArray();

        public TWeight Weight(int index) => weights[index];
        public TWeight[] Weights() => weights.ToArray();

        public int Count => subjects.Count;

        public LotteryPair(params (TSubject subject, TWeight weight)[] pairs)
        {
            foreach (var (subject, weight) in pairs)
            {
                subjects.Add(subject);
                weights.Add(weight);
            }
        }

        [Obsolete] public object this[int index] => (subjects[index], weights[index]);
    }
}

using System;

namespace trrne.Box
{
    public static class Eases
    {
        readonly static Stopwatch sw = new(true);
        static float x => sw.secondf;

        public static float _1 => x;
        public static float _2 => x * x;
        public static float _3 => x * x * x;
        public static float sin => MathF.Sin(x);
        public static float cos => MathF.Cos(x);
        public static float tan => MathF.Tan(x);
    }
}
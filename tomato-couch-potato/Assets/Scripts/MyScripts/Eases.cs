using System;

namespace trrne.Box
{
    public static class Eases
    {
        readonly static Stopwatch sw = new(true);
        static float x => sw.secondf;

        public static float Linear => x;
        public static float Quadratic => x * x;
        public static float Cubic => x * x * x;
        public static float Sin => MathF.Sin(x);
        public static float Cos => MathF.Cos(x);
        public static float Tan => MathF.Tan(x);

        public static float OmosiroFunction(int type)
        {
            return type switch
            {
                0 => x,
                1 => x * x,
                2 => x * x * x,
                3 => -(x * x / 2) + (1 / 2),
                4 => -x * x + 2 * x,
                5 => -2 * x * x * x + 3 * x * x, // -2x^3+3x^2
                6 => MathF.Sin(x * 8),
                _ => 0
            };
        }
    }
}
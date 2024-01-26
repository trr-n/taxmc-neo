using SSW = System.Diagnostics.Stopwatch;

namespace trrne.Box
{
    public sealed class Timer
    {
        readonly SSW timer;
        public Timer() => timer = new SSW();
        public void Start() => timer.Start();
        public void Restart() => timer.Restart();
        public double Delta => (double)timer.ElapsedMilliseconds / 1000f;
    }
}
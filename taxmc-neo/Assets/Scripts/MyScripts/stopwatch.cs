using SystemStopwatch = System.Diagnostics.Stopwatch;

namespace trrne.Bag
{
    public enum StopwatchFormat { H, h, Hour, hour, M, m, Minute, minute, S, s, Second, second, MS, ms }

    public sealed class Stopwatch
    {
        SystemStopwatch stopwatch;

        public Stopwatch() => stopwatch = new();
        public Stopwatch(bool start)
        {
            stopwatch = new();
            if (start) { stopwatch.Start(); }
        }

        public void Start() => stopwatch.Start();
        public static void Start(params Stopwatch[] sw)
        {
            foreach (var i in sw)
            {
                i.Start();
            }
        }
        public void Stop() => stopwatch.Stop();
        public void Restart() => stopwatch.Restart();
        public void Reset() => stopwatch.Reset();
        public bool isRunning => stopwatch.IsRunning;
        public static void Rubbish(params Stopwatch[] sw) { foreach (var i in sw) i.Rubbish(); }
        public void Rubbish() { stopwatch.Stop(); stopwatch = null; }

        public int h => stopwatch.Elapsed.Hours;
        public float hf => Numeric.Round((float)stopwatch.Elapsed.TotalHours, 6);
        public int Hour() => stopwatch.Elapsed.Hours;
        public float HourF(int digit = 6) => Numeric.Round((float)stopwatch.Elapsed.TotalHours, digit);

        public int m => stopwatch.Elapsed.Minutes;
        public float mf => Numeric.Round((float)stopwatch.Elapsed.TotalMinutes, 6);
        public int Minute() => stopwatch.Elapsed.Minutes;
        public float MinuteF(int digit = 6) => Numeric.Round((float)stopwatch.Elapsed.TotalMinutes, digit);

        public int s => stopwatch.Elapsed.Seconds;
        public float sf => Numeric.Round((float)stopwatch.Elapsed.TotalSeconds, 6);
        public int Second() => stopwatch.Elapsed.Seconds;
        public float SecondF(int digit = 6) => Numeric.Round((float)stopwatch.Elapsed.TotalSeconds, digit);

        public int ms => stopwatch.Elapsed.Milliseconds;
        public float msf => Numeric.Round((float)stopwatch.Elapsed.TotalMilliseconds, 6);
        public int MSecond() => stopwatch.Elapsed.Milliseconds;
        public float MSecondF(int digit = 6) => Numeric.Round((float)stopwatch.Elapsed.TotalMilliseconds, digit);

        public string Spent() => stopwatch.Elapsed.ToString();
        public int Spent(StopwatchFormat style)
        {
            return style switch
            {
                StopwatchFormat.H or StopwatchFormat.h or StopwatchFormat.Hour or StopwatchFormat.hour => Hour(),
                StopwatchFormat.M or StopwatchFormat.m or StopwatchFormat.Minute or StopwatchFormat.minute => Minute(),
                StopwatchFormat.S or StopwatchFormat.s or StopwatchFormat.Second or StopwatchFormat.second => Second(),
                StopwatchFormat.MS or StopwatchFormat.ms => MSecond(),
                _ => -1,
            };
        }
        public float SpentF(StopwatchFormat style)
        {
            return style switch
            {
                StopwatchFormat.H or StopwatchFormat.h or StopwatchFormat.Hour or StopwatchFormat.hour => HourF(),
                StopwatchFormat.M or StopwatchFormat.m or StopwatchFormat.Minute or StopwatchFormat.minute => MinuteF(),
                StopwatchFormat.S or StopwatchFormat.s or StopwatchFormat.Second or StopwatchFormat.second => SecondF(),
                StopwatchFormat.MS or StopwatchFormat.ms => MSecondF(),
                _ => -1f,
            };
        }
    }
}

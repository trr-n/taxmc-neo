using SystemStopwatch = System.Diagnostics.Stopwatch;

namespace trrne.Utils
{
    public enum StopwatchFormat { H, h, Hour, hour, M, m, Minute, minute, S, s, Second, second, MS, ms }

    public sealed class Stopwatch
    {
        SystemStopwatch sw;

        public Stopwatch() => sw = new();
        public Stopwatch(bool autoStart)
        {
            sw = new();
            sw.Start();
        }

        public void Start() => sw.Start();
        public static void Start(params Stopwatch[] sw)
        {
            foreach (var i in sw)
            {
                i.Start();
            }
        }
        public void Stop() => sw.Stop();
        public void Restart() => sw.Restart();
        public void Reset() => sw.Reset();
        public bool IsRunning() => sw.IsRunning;
        public bool isRunning => sw.IsRunning;
        public static void Rubbish(params Stopwatch[] sw) { foreach (var i in sw) i.Rubbish(); }
        public void Rubbish() { sw.Stop(); sw = null; }

        public int h => sw.Elapsed.Hours;
        public float hf => Numeric.Round((float)sw.Elapsed.TotalHours, 6);
        public int Hour() => sw.Elapsed.Hours;
        public float HourF(int digit = 6) => Numeric.Round((float)sw.Elapsed.TotalHours, digit);

        public int m => sw.Elapsed.Minutes;
        public float mf => Numeric.Round((float)sw.Elapsed.TotalMinutes, 6);
        public int Minute() => sw.Elapsed.Minutes;
        public float MinuteF(int digit = 6) => Numeric.Round((float)sw.Elapsed.TotalMinutes, digit);

        public int s => sw.Elapsed.Seconds;
        public float sf => Numeric.Round((float)sw.Elapsed.TotalSeconds, 6);
        public int Second() => sw.Elapsed.Seconds;
        public float SecondF(int digit = 6) => Numeric.Round((float)sw.Elapsed.TotalSeconds, digit);

        public int ms => sw.Elapsed.Milliseconds;
        public float msf => Numeric.Round((float)sw.Elapsed.TotalMilliseconds, 6);
        public int MSecond() => sw.Elapsed.Milliseconds;
        public float MSecondF(int digit = 6) => Numeric.Round((float)sw.Elapsed.TotalMilliseconds, digit);

        public string Spent() => sw.Elapsed.ToString();
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

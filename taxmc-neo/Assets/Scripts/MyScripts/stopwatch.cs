using System;
using SystemStopwatch = System.Diagnostics.Stopwatch;

namespace trrne.Bag
{
    public enum StopwatchFormat
    {
        H, h, Hour, hour,
        M, m, Minute, minute,
        S, s, Second, second,
        MS, ms, MilliSecond, millisecond
    }

    public enum StopwatchSpentOutput
    {
        HMS, HourMinuteSecond,
        MS, MinuteSecond,
    }

    public sealed class Stopwatch
    {
        SystemStopwatch sw;

        public Stopwatch() => sw = new();
        public Stopwatch(bool start)
        {
            sw = new();
            if (start) { sw.Start(); }
        }

        public void Start() => sw.Start();
        public static void Start(params Stopwatch[] sws) => Array.ForEach(sws, sw => sw.Start());
        public void Stop() => sw.Stop();
        public void Restart() => sw.Restart();
        public void Reset() => sw.Reset();
        public bool isRunning => sw.IsRunning;
        public static void Rubbish(params Stopwatch[] sws) => Array.ForEach(sws, sw => sw.Rubbish());
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

        public TimeSpan spent => sw.Elapsed;
        public string Spent(StopwatchSpentOutput output) => output switch
        {
            StopwatchSpentOutput.HMS or StopwatchSpentOutput.HourMinuteSecond => spent.ToString("hh\\:mm\\:ss"),
            StopwatchSpentOutput.MS or StopwatchSpentOutput.MinuteSecond => spent.ToString("mm\\:ss"),
            _ => "NullNull Lotion"
        };

        public int Spent(StopwatchFormat style)
        {
            return style switch
            {
                StopwatchFormat.H or StopwatchFormat.h or StopwatchFormat.Hour or StopwatchFormat.hour => Hour(),
                StopwatchFormat.M or StopwatchFormat.m or StopwatchFormat.Minute or StopwatchFormat.minute => Minute(),
                StopwatchFormat.S or StopwatchFormat.s or StopwatchFormat.Second or StopwatchFormat.second => Second(),
                StopwatchFormat.MS or StopwatchFormat.ms or StopwatchFormat.MilliSecond or StopwatchFormat.millisecond => MSecond(),
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
                StopwatchFormat.MS or StopwatchFormat.ms or StopwatchFormat.MilliSecond or StopwatchFormat.millisecond => MSecondF(),
                _ => -1f,
            };
        }
    }
}

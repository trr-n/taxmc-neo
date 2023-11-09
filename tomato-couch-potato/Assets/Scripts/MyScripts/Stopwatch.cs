using System;
using SystemStopwatch = System.Diagnostics.Stopwatch;

namespace trrne.Box
{
    public sealed class Stopwatch
    {
        SystemStopwatch syswatch;

        public Stopwatch() => syswatch = new();
        public Stopwatch(bool start)
        {
            syswatch = new();
            Simple.If(start, () => syswatch.Start());
        }

        public void Start() => syswatch.Start();
        public static void Start(params Stopwatch[] sws) => sws.ForEach(sw => sw.Start());
        public void Stop() => syswatch.Stop();
        public void Restart() => syswatch.Restart();
        public void Reset() => syswatch.Reset();
        public bool IsRunning() => syswatch.IsRunning;
        public void Rubbish()
        {
            syswatch.Stop();
            syswatch = null;
        }
        public static void Rubbish(params Stopwatch[] sws) => sws.ForEach(sw => sw.Rubbish());

        public int H => syswatch.Elapsed.Hours;
        public float Hf => Maths.Round((float)syswatch.Elapsed.TotalHours, 6);
        public int Hour() => syswatch.Elapsed.Hours;
        public float HourF(int digit = 6) => Maths.Round((float)syswatch.Elapsed.TotalHours, digit);

        public int M => syswatch.Elapsed.Minutes;
        public float Mf => Maths.Round((float)syswatch.Elapsed.TotalMinutes, 6);
        public int Minute() => syswatch.Elapsed.Minutes;
        public float MinuteF(int digit = 6) => Maths.Round((float)syswatch.Elapsed.TotalMinutes, digit);

        public int S => syswatch.Elapsed.Seconds;
        public float Sf => Maths.Round((float)syswatch.Elapsed.TotalSeconds, 6);
        public int Second() => syswatch.Elapsed.Seconds;
        public float SecondF(int digit = 6) => Maths.Round((float)syswatch.Elapsed.TotalSeconds, digit);

        public int MS => syswatch.Elapsed.Milliseconds;
        public float MSf => Maths.Round((float)syswatch.Elapsed.TotalMilliseconds, 6);
        public int Milliseconds() => syswatch.Elapsed.Milliseconds;
        public float MillisecondF(int digit = 6) => Maths.Round((float)syswatch.Elapsed.TotalMilliseconds, digit);

        public TimeSpan Spent() => syswatch.Elapsed;

        public string Spent(StopwatchOutput output)
        => output switch
        {
            StopwatchOutput.HMS or StopwatchOutput.HourMinuteSecond or StopwatchOutput.hms => Spent().ToString("hh\\:mm\\:ss"),
            StopwatchOutput.MS or StopwatchOutput.MinuteSecond or StopwatchOutput.ms => Spent().ToString("mm\\:ss"),
            _ => "nullnull lotion"
        };

        public int Spent(StopwatchFormat format)
        => format switch
        {
            StopwatchFormat.H or StopwatchFormat.h or StopwatchFormat.Hour or StopwatchFormat.hour => Hour(),
            StopwatchFormat.M or StopwatchFormat.m or StopwatchFormat.Minute or StopwatchFormat.minute => Minute(),
            StopwatchFormat.S or StopwatchFormat.s or StopwatchFormat.Second or StopwatchFormat.second => Second(),
            StopwatchFormat.MS or StopwatchFormat.ms or StopwatchFormat.MilliSecond or StopwatchFormat.millisecond => Milliseconds(),
            _ => -1,
        };

        public float SpentF(StopwatchFormat format)
        => format switch
        {
            StopwatchFormat.H or StopwatchFormat.h or StopwatchFormat.Hour or StopwatchFormat.hour => HourF(),
            StopwatchFormat.M or StopwatchFormat.m or StopwatchFormat.Minute or StopwatchFormat.minute => MinuteF(),
            StopwatchFormat.S or StopwatchFormat.s or StopwatchFormat.Second or StopwatchFormat.second => SecondF(),
            StopwatchFormat.MS or StopwatchFormat.ms or StopwatchFormat.MilliSecond or StopwatchFormat.millisecond => MillisecondF(),
            _ => -1f,
        };
    }
}

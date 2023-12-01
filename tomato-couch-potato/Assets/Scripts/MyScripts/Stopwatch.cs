using System;
using SystemStopwatch = System.Diagnostics.Stopwatch;

namespace trrne.Box
{
    public sealed class Stopwatch
    {
        readonly SystemStopwatch syswatch;

        public Stopwatch() => syswatch = new();
        public Stopwatch(bool start)
        {
            syswatch = new();
            start.If(syswatch.Start);
        }

        public void Start() => syswatch.Start();
        public void Stop() => syswatch.Stop();
        public void Restart() => syswatch.Restart();
        public void Reset() => syswatch.Reset();
        public bool IsRunning() => syswatch.IsRunning;

        public static void Start(params Stopwatch[] sws) => sws.ForEach(sw => sw.Start());
        public static void Stop(params Stopwatch[] sws) => sws.ForEach(sw => sw.Stop());
        public static void Restart(params Stopwatch[] sws) => sws.ForEach(sw => sw.Restart());
        public static void Reset(params Stopwatch[] sws) => sws.ForEach(sw => sw.Reset());

        public int H() => syswatch.Elapsed.Hours;
        public float Hf(int digit = 6) => MathF.Round((float)syswatch.Elapsed.TotalHours, digit);

        public int M() => syswatch.Elapsed.Minutes;
        public float Mf(int digit = 6) => MathF.Round((float)syswatch.Elapsed.TotalMinutes, digit);

        public int S() => syswatch.Elapsed.Seconds;
        public float Sf(int digit = 6) => MathF.Round((float)syswatch.Elapsed.TotalSeconds, digit);

        public int MS() => syswatch.Elapsed.Milliseconds;
        public float MSf(int digit = 6) => MathF.Round((float)syswatch.Elapsed.TotalMilliseconds, digit);

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
            StopwatchFormat.H or StopwatchFormat.h or StopwatchFormat.Hour or StopwatchFormat.hour => H(),
            StopwatchFormat.M or StopwatchFormat.m or StopwatchFormat.Minute or StopwatchFormat.minute => M(),
            StopwatchFormat.S or StopwatchFormat.s or StopwatchFormat.Second or StopwatchFormat.second => S(),
            StopwatchFormat.MS or StopwatchFormat.ms or StopwatchFormat.MilliSecond or StopwatchFormat.millisecond => MS(),
            _ => -1,
        };

        public float Spentf(StopwatchFormat format)
        => format switch
        {
            StopwatchFormat.H or StopwatchFormat.h or StopwatchFormat.Hour or StopwatchFormat.hour => Hf(),
            StopwatchFormat.M or StopwatchFormat.m or StopwatchFormat.Minute or StopwatchFormat.minute => Mf(),
            StopwatchFormat.S or StopwatchFormat.s or StopwatchFormat.Second or StopwatchFormat.second => Sf(),
            StopwatchFormat.MS or StopwatchFormat.ms or StopwatchFormat.MilliSecond or StopwatchFormat.millisecond => MSf(),
            _ => -1f,
        };
    }
}

using System;
using SSW = System.Diagnostics.Stopwatch;

namespace trrne.Box
{
    public sealed class Stopwatch
    {
        readonly SSW ssw;

        public Stopwatch(bool start)
        {
            ssw = new();
            start.If(ssw.Start);
        }
        public Stopwatch() : this(false) { }

        public void Start() => ssw.Start();
        public void Stop() => ssw.Stop();
        public void Restart() => ssw.Restart();
        public void Reset() => ssw.Reset();
        public bool IsRunning() => ssw.IsRunning;

        public static void Start(params Stopwatch[] sws) => sws.ForEach(sw => sw.Start());
        public static void Stop(params Stopwatch[] sws) => sws.ForEach(sw => sw.Stop());
        public static void Restart(params Stopwatch[] sws) => sws.ForEach(sw => sw.Restart());
        public static void Reset(params Stopwatch[] sws) => sws.ForEach(sw => sw.Reset());

        public int H() => ssw.Elapsed.Hours;
        public float Hf(int digit = 6) => MathF.Round((float)ssw.Elapsed.TotalHours, digit);
        public int h => H();
        public float hf => Hf();

        public int M() => ssw.Elapsed.Minutes;
        public float Mf(int digit = 6) => MathF.Round((float)ssw.Elapsed.TotalMinutes, digit);
        public int m => M();
        public float mf => Mf();

        public int S() => ssw.Elapsed.Seconds;
        public float Sf(int digit = 6) => MathF.Round((float)ssw.Elapsed.TotalSeconds, digit);
        public int s => S();
        public float sf => Sf();

        public int MS() => ssw.Elapsed.Milliseconds;
        public float MSf(int digit = 6) => MathF.Round((float)ssw.Elapsed.TotalMilliseconds, digit);
        public int ms => MS();
        public float msf => MSf();

        public TimeSpan Spent() => ssw.Elapsed;

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

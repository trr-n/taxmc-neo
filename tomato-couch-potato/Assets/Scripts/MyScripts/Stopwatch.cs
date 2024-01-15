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
        public bool isRunning => ssw.IsRunning;
        public bool IsRunning() => isRunning;

        public static void Start(params Stopwatch[] sws) => sws.ForEach(sw => sw.Start());
        public static void Stop(params Stopwatch[] sws) => sws.ForEach(sw => sw.Stop());
        public static void Restart(params Stopwatch[] sws) => sws.ForEach(sw => sw.Restart());
        public static void Reset(params Stopwatch[] sws) => sws.ForEach(sw => sw.Reset());

        public int H() => ssw.Elapsed.Hours;
        public int Hour() => H();
        public float Hf(int digit = 6) => MathF.Round((float)ssw.Elapsed.TotalHours, digit);
        public float Hourf(int digit = 6) => Hf(digit);
        public int h => H();
        public int hour => h;
        public float hf => Hf();
        public float hourf => hf;

        public int M() => ssw.Elapsed.Minutes;
        public int Minute() => M();
        public float Mf(int digit = 6) => MathF.Round((float)ssw.Elapsed.TotalMinutes, digit);
        public float Minutef(int digit = 6) => Mf(digit);
        public int m => M();
        public int minute => m;
        public float mf => Mf();
        public float minutef => mf;

        public int S() => ssw.Elapsed.Seconds;
        public int Second() => S();
        public float Sf(int digit = 6) => MathF.Round((float)ssw.Elapsed.TotalSeconds, digit);
        public float Secondf(int digit = 6) => Sf(digit);
        public int s => S();
        public int second => s;
        public float sf => Sf();
        public float secondf => sf;

        public int MS() => ssw.Elapsed.Milliseconds;
        public int MilliSecond() => MS();
        public float MSf(int digit = 6) => MathF.Round((float)ssw.Elapsed.TotalMilliseconds, digit);
        public float MilliSecondf(int digit = 6) => MSf(digit);
        public int ms => MS();
        public int millisecond => ms;
        public float msf => MSf();
        public float millisecondf => msf;

        public TimeSpan Spent() => ssw.Elapsed;

        public string Spent(StopwatchOutputFormat output)
        => output switch
        {
            StopwatchOutputFormat.HMS or StopwatchOutputFormat.HourMinuteSecond or StopwatchOutputFormat.hms => Spent().ToString("hh\\:mm\\:ss"),
            StopwatchOutputFormat.MS or StopwatchOutputFormat.MinuteSecond or StopwatchOutputFormat.ms => Spent().ToString("mm\\:ss"),
            _ => "nullnull lotion"
        };

        public int Spent(StopwatchFormat format)
        => format switch
        {
            StopwatchFormat.H or StopwatchFormat.h or StopwatchFormat.Hour or StopwatchFormat.hour => h,
            StopwatchFormat.M or StopwatchFormat.m or StopwatchFormat.Minute or StopwatchFormat.minute => m,
            StopwatchFormat.S or StopwatchFormat.s or StopwatchFormat.Second or StopwatchFormat.second => s,
            StopwatchFormat.MS or StopwatchFormat.ms or StopwatchFormat.MilliSecond or StopwatchFormat.millisecond => ms,
            _ => -1,
        };

        public float Spentf(StopwatchFormat format)
        => format switch
        {
            StopwatchFormat.H or StopwatchFormat.h or StopwatchFormat.Hour or StopwatchFormat.hour => hf,
            StopwatchFormat.M or StopwatchFormat.m or StopwatchFormat.Minute or StopwatchFormat.minute => mf,
            StopwatchFormat.S or StopwatchFormat.s or StopwatchFormat.Second or StopwatchFormat.second => sf,
            StopwatchFormat.MS or StopwatchFormat.ms or StopwatchFormat.MilliSecond or StopwatchFormat.millisecond => msf,
            _ => -1f,
        };
    }
}

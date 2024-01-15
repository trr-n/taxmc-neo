using UnityEngine;

namespace trrne.Box
{
    public enum FrameRate
    {
        Low = 30,
        Medium = 60,
        High = 144,
        Ultra = 200,
        VSync = -1
    }

    public enum CursorAppearance
    {
        Invisible,
        Visible
    }

    public enum CursorRangeOfMotion
    {
        InScene = CursorLockMode.Confined,
        Fixed = CursorLockMode.Locked,
        Limitless = CursorLockMode.None
    }

    public enum KindaScreen
    {
        Resolution,
        Size,
        Center
    }

    public enum TimesFormat
    {
        D, Domestics,
        I, International
    }

    public enum RandStringType
    {
        Mixed,
        ABMixed,
        ABUpper,
        ABLower,
        Number
    }

    public enum ScenesCountingType
    {
        Built,
        Unbuilt
    }

    public enum StopwatchFormat
    {
        H, h, Hour, hour,
        M, m, Minute, minute,
        S, s, Second, second,
        MS, ms, MilliSecond, millisecond
    }

    public enum StopwatchOutputFormat
    {
        HMS, HourMinuteSecond, hms,
        MS, MinuteSecond, ms
    }
}

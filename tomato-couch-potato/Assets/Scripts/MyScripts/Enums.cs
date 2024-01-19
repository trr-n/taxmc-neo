using UnityEngine;

namespace trrne.Box
{
    public enum FrameRate
    {
        Low = 30,
        Medium = 60,
        High = 144,
        Ultra = 240,
        VSync = -1
    }

    public enum CursorAppearance
    {
        Invisible,
        Visible
    }

    public enum CursorRangeOfMotion
    {
        Limitless = CursorLockMode.None,
        Fixed = CursorLockMode.Locked,
        InScene = CursorLockMode.Confined
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

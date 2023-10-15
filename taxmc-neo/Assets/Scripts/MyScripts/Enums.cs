using UnityEngine;

namespace trrne.Bag
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

    public enum TempsFormat
    {
        Standard,
        Rebirth
    }

    public enum RandomStringOutput
    {
        Auto,
        Alphabet,
        Upper,
        Lower,
        Number
    }

    public enum ScenesCountingFormat
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

    public enum StopwatchOutput
    {
        HMS, HourMinuteSecond, hms,
        MS, MinuteSecond, ms
    }


}

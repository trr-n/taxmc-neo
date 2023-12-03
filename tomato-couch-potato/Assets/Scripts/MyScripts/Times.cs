using System;

namespace trrne.Box
{
    public static class Times
    {
        public static string Raw()
        => (Date(TimesFormat.Domestics) + Time(TimesFormat.Domestics)).DeleteLump("/", ":");

        public static string Date(TimesFormat format = TimesFormat.Domestics)
        => format switch
        {
            TimesFormat.International => $"{DateTime.Now.Day}/{DateTime.Now.Month}/{DateTime.Now.Year}",
            TimesFormat.Domestics or _ => $"{DateTime.Now.Year}/{DateTime.Now.Month}/{DateTime.Now.Day}",
        };

        public static string Time(TimesFormat format = TimesFormat.Domestics)
        => format switch
        {
            TimesFormat.International => $"{DateTime.Now.Second}:{DateTime.Now.Minute}:{DateTime.Now.Hour}",
            TimesFormat.Domestics or _ => $"{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second}",
        };
    }
}

using System;

namespace trrne.Box
{
    public static class Times
    {
        static readonly DateTime now = DateTime.Now;

        public static string Raw() => (Date(TimesFormat.D) + Time(TimesFormat.D)).DeleteLump("/", ":");

        public static string Date(TimesFormat format = TimesFormat.Domestics)
        => format switch
        {
            TimesFormat.International or TimesFormat.I => $"{now.Day}/{now.Month}/{now.Year}",
            TimesFormat.Domestics or TimesFormat.D or _ => $"{now.Year}/{now.Month}/{now.Day}",
        };

        public static string Time(TimesFormat format = TimesFormat.Domestics)
        => format switch
        {
            TimesFormat.International => $"{now.Second}:{now.Minute}:{now.Hour}",
            TimesFormat.Domestics or _ => $"{now.Hour}:{now.Minute}:{now.Second}",
        };
    }
}

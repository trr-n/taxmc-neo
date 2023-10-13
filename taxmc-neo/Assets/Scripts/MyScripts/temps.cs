using System;

namespace trrne.Bag
{
    public enum TempsFormat { Standard, Rebirth }

    public static class Temps
    {
        public static string raw => (Date() + Time()).ReplaceLump("/  :", "");

        public static string Date(TempsFormat format = TempsFormat.Standard)
        => format switch
        {
            TempsFormat.Standard => $"{DateTime.Now.Year}/{DateTime.Now.Month}/{DateTime.Now.Day}",
            TempsFormat.Rebirth => $"{DateTime.Now.Day}/{DateTime.Now.Month}/{DateTime.Now.Year}",
            _ => throw null,
        };

        public static string Time(TempsFormat style = TempsFormat.Standard)
        => style switch
        {
            TempsFormat.Standard => $"{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second}",
            TempsFormat.Rebirth => $"{DateTime.Now.Second}:{DateTime.Now.Minute}:{DateTime.Now.Hour}",
            _ => throw null,
        };
    }
}
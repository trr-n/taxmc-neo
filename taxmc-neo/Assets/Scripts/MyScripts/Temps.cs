using System;

namespace trrne.Bag
{
    public static class Temps
    {
        public static string raw => (Date() + Time()).DeleteLump("/", ":"); // ReplaceLump("/  :", "");

        public static string Date(TempsFormat format = TempsFormat.Standard)
        {
            return format switch
            {
                TempsFormat.Standard => $"{DateTime.Now.Year}/{DateTime.Now.Month}/{DateTime.Now.Day}",
                TempsFormat.Rebirth => $"{DateTime.Now.Day}/{DateTime.Now.Month}/{DateTime.Now.Year}",
                _ => throw null,
            };
        }

        public static string Time(TempsFormat style = TempsFormat.Standard)
        {
            return style switch
            {
                TempsFormat.Standard => $"{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second}",
                TempsFormat.Rebirth => $"{DateTime.Now.Second}:{DateTime.Now.Minute}:{DateTime.Now.Hour}",
                _ => throw null,
            };
        }
    }
}

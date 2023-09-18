using System;
using System.Text.RegularExpressions;

namespace trrne.Utils
{
    public enum TempsFormat { Standard, Rebirth }

    public static class Temps
    {
        public static string Raw => (Date() + Time()).ReplaceLump("/  :", "");

        public static string Date(TempsFormat style = TempsFormat.Standard)
        {
            return style switch
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
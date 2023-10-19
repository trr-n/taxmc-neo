using System;

namespace Chickenen.Pancreas
{
    public static class Times
    {
        public static string Raw => (Date(TempsFormat.Domestics) + Time(TempsFormat.Domestics)).DeleteLump("/", ":"); // ReplaceLump("/  :", "");

        public static string Date(TempsFormat format)
        {
            return format switch
            {
                TempsFormat.Domestics => $"{DateTime.Now.Year}/{DateTime.Now.Month}/{DateTime.Now.Day}",
                TempsFormat.International => $"{DateTime.Now.Day}/{DateTime.Now.Month}/{DateTime.Now.Year}",
                _ => throw null,
            };
        }

        public static string Time(TempsFormat format)
        {
            return format switch
            {
                TempsFormat.Domestics => $"{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second}",
                TempsFormat.International => $"{DateTime.Now.Second}:{DateTime.Now.Minute}:{DateTime.Now.Hour}",
                _ => throw null,
            };
        }
    }
}

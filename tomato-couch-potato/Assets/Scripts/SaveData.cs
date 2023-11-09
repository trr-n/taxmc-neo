using trrne.Box;

namespace trrne.Core
{
    public struct SaveData
    {
        // public float minutes;
        // public float seconds;
    }

    public static class SaveSettings
    {
        public static string Path => Paths.DataPath("data.sav");
        public static string Password => "P0M0D0R0";
    }
}
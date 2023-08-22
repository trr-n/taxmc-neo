using static UnityEngine.SystemInfo;

namespace Self.Utils
{
    public static class Sys
    {
        public static string OS => operatingSystem;
        public static int RAM => systemMemorySize / 1000;
        public static string CPU => processorType;
        public static string GPU => graphicsDeviceName;
        public static int VRAM => graphicsMemorySize / 1000;
    }
}
using System.IO;
using System.Runtime.CompilerServices;

namespace trrne.Box
{
    public static class Files
    {
        public static string CallerPath([CallerFilePath] string path = "") => path;
        public static int CallerLineNumber([CallerLineNumber] int line = 0) => line;

        public static string __Path__(string name) => Path.GetDirectoryName(name);
        public static string __Path__() => __Path__(CallerPath());
    }
}

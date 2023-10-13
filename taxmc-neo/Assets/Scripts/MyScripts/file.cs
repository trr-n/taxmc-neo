using System.IO;
using System.Runtime.CompilerServices;

namespace trrne.Bag
{
    public static class File
    {
        public static string CallerPath([CallerFilePath] string path = "")
        {
            return path;
        }

        public static int CallerLineNumber([CallerLineNumber] int line = 0)
        {
            return line;
        }

        public static string __Path__(string name)
        {
            return Path.GetDirectoryName(name);
        }
        public static string __Path__()
        {
            return __Path__(CallerPath());
        }
    }
}

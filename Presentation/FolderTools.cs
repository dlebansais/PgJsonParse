using System.IO;

namespace Presentation
{
    public static class FolderTools
    {
        public static void DeleteDirectory(string directoryName, bool recursive)
        {
#if CSHTML5
#else
            Directory.Delete(directoryName, recursive);
#endif
        }
    }
}

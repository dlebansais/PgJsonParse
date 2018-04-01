using System.IO;

namespace Presentation
{
    public class FolderTools
    {
        public static void DeleteDirectory(string directoryName, bool recursive)
        {
            Directory.Delete(directoryName, recursive);
        }
    }
}

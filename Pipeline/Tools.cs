namespace Preprocessor;

using System.IO;

public static class Tools
{
    public static string SafeGetSubdirectory(string mainDirectory, string subDirectory, out bool isCreated)
    {
        string Subdirectory = Path.Combine(mainDirectory, subDirectory);

        if (!Directory.Exists(Subdirectory))
        {
            isCreated = true;
            Directory.CreateDirectory(Subdirectory);
        }
        else
            isCreated = false;

        return Subdirectory;
    }
}

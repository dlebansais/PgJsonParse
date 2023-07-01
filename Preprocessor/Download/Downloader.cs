namespace Downloader;

using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using Preprocessor;

internal class Downloader
{
    public bool Download(List<JsonFile> jsonFileList, string versionDirectory, out string versionPath)
    {
        if (!CheckVersion(versionDirectory, out int Version, out versionPath, out bool IsNew))
            return false;

        if (IsNew)
        {
            foreach (JsonFile File in jsonFileList)
                if (!Download(Version, versionPath, File))
                    return false;
        }

        return true;
    }

    private bool CheckVersion(string versionDirectory, out int version, out string versionPath, out bool isNew)
    {
        const string RequestUri = "http://client.projectgorgon.com/fileversion.txt";
        Stopwatch Watch = new Stopwatch();
        string VersionContent = string.Empty;
        WebClientTool.DownloadText(RequestUri, Watch, (bool isFound, string? content) => VersionContent = content!, ignoreCache: true, out bool IsFound);

        if (!int.TryParse(VersionContent, out version))
        {
            Debug.WriteLine($"Unable to parse {VersionContent} as a version number");

            versionPath = string.Empty;
            isNew = false;
            return false;
        }

        versionPath = Tools.SafeGetSubdirectory(versionDirectory, version.ToString(), out bool isCreated);

        if (isCreated)
        {
            isNew = true;
            Debug.WriteLine($"{version} is a new version");
        }
        else
            isNew = false;

        return true;
    }

    private bool Download(int version, string versionPath, JsonFile file)
    {
        string FullPath = $"{versionPath}\\{file.FileName}.json";

        if (!File.Exists(FullPath))
        {
            string RequestUri = $"http://client.projectgorgon.com/v{version}/data/{file.FileName}.json";
            Stopwatch Watch = new Stopwatch();
            string FileContent = string.Empty;
            WebClientTool.DownloadText(RequestUri, Watch, (bool isFound, string? content) => FileContent = content!, ignoreCache: false, out bool IsFound);

            if (IsFound)
            {
                using FileStream WriteStream = new FileStream(FullPath, FileMode.Create, FileAccess.Write);
                using StreamWriter Writer = new StreamWriter(WriteStream, Encoding.UTF8);
                Writer.Write(FileContent);
            }
        }

        return true;
    }
}

namespace Downloader;

using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using Preprocessor;

internal class Downloader
{
    public bool Download(int version, string versionPath, List<JsonFile> jsonFileList)
    {
        foreach (JsonFile File in jsonFileList)
            if (!Download(version, versionPath, File))
                return false;

        return true;
    }

    public bool Download(int version, string versionPath, JsonFile file)
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

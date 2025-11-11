namespace Preprocessor;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using Downloader;

internal partial class Program
{
    static int Main(string[] args)
    {
        string ApplicationDataDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        string ParserDirectory = Tools.SafeGetSubdirectory(ApplicationDataDirectory, "PgJsonParse", out _);
        string VersionDirectory = Tools.SafeGetSubdirectory(ParserDirectory, "Versions", out _);
        string AnyVersionDirectory = Tools.SafeGetSubdirectory(VersionDirectory, "000", out _);
        string DatabaseFile = $"{AnyVersionDirectory}/_version.db";

        Downloader Downloader = new();
        if (!Downloader.Download(JsonFiles, VersionDirectory, out int version, out string VersionPath))
            return -1;

        if (File.Exists(DatabaseFile))
            File.Delete(DatabaseFile);

        using IFreeSql? fsql = new FreeSql.FreeSqlBuilder()
            .UseConnectionString(FreeSql.DataType.Sqlite, $"Data Source={DatabaseFile}")
            .UseAutoSyncStructure(true)
            .Build();
        if (fsql is null)
        {
            Debug.WriteLine($"Failed to create the database at {DatabaseFile}.");
            return -3;
        }

        Preprocessor Preprocessor = new();
        if (!Preprocessor.Preprocess(VersionPath, JsonFiles, fsql, out string CuratedVersionDirectory))
            return -2;

        foreach (string OldFile in Directory.GetFiles(AnyVersionDirectory))
            if (!string.Equals(Path.GetFullPath(OldFile), Path.GetFullPath(DatabaseFile), StringComparison.OrdinalIgnoreCase))
                File.Delete(OldFile);
        foreach (string NewFile in Directory.GetFiles(CuratedVersionDirectory))
            File.Copy(NewFile, Path.Combine(AnyVersionDirectory, Path.GetFileName(NewFile)));
        File.WriteAllText(Path.Combine(AnyVersionDirectory, "_version.txt"), version.ToString());

        return 0;
    }
}

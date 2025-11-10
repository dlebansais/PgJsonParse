namespace Pipeline.Test;

using System;
using System.IO;

public static class TestTools
{
    public static string GetVersionPath(string testName)
    {
        string Directory = Environment.CurrentDirectory;
        
        while (Directory != string.Empty)
        {
            string FolderName = Path.GetFileName(Directory)!;
            if (FolderName != "net481" &&
                FolderName != "x64" &&
                FolderName != "Debug" &&
                FolderName != "Release" &&
                FolderName != "bin" &&
                FolderName != "obj")
                break;

            Directory = Path.GetDirectoryName(Directory)!;
        }

        string Result = Path.Combine(Directory, "TestCases", testName);

        string Curated = Path.Combine(Result, "Curated");
        if (System.IO.Directory.Exists(Curated))
            System.IO.Directory.Delete(Curated, recursive: true);

        return Result;
    }

    public static IFreeSql CreateTestDatabase()
    {
        IFreeSql? fsql = new FreeSql.FreeSqlBuilder()
                .UseConnectionString(FreeSql.DataType.Sqlite, $"Data Source=test.db")
                .UseAutoSyncStructure(true)
                .Build();

        return fsql ?? throw new InvalidOperationException();
    }
}

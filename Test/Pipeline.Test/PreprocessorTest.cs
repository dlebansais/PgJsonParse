namespace Pipeline.Test;

using System;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using Preprocessor;

[TestFixture]
public partial class PreprocessorTest
{
    [Test]
    public void Test()
    {
        string ApplicationDataDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        string ParserDirectory = Tools.SafeGetSubdirectory(ApplicationDataDirectory, "PgJsonParse", out _);
        string VersionDirectory = Tools.SafeGetSubdirectory(ParserDirectory, "Versions", out _);

        int LastVersion = -1;
        foreach (string Directory in Directory.GetDirectories(VersionDirectory))
            if (int.TryParse(Path.GetFileName(Directory), out int Version) && LastVersion < Version)
                LastVersion = Version;

        Assert.That(LastVersion, Is.GreaterThan(0));

        string LastVersionPath = $"{VersionDirectory}\\{LastVersion}";
        string VersionPath = Tools.SafeGetSubdirectory(Environment.CurrentDirectory, "Temp", out _);
        string CuratedVersionPath = $"{VersionPath}\\Curated";

        if (Directory.Exists(CuratedVersionPath))
            Directory.Delete(CuratedVersionPath, recursive: true);

        foreach (string FileName in Directory.GetFiles(LastVersionPath, "*.json"))
            File.Copy(FileName, $"{VersionPath}\\{Path.GetFileName(FileName)}", overwrite: true);

        using IFreeSql? Fsql = new FreeSql.FreeSqlBuilder()
            .UseConnectionString(FreeSql.DataType.Sqlite, $"Data Source=test.db")
            .UseAutoSyncStructure(true)
            .Build();
        Assert.That(Fsql, Is.Not.Null);

        Preprocessor Preprocessor = new();
        bool Success = Preprocessor.Preprocess(VersionPath, JsonFileList, Fsql, out _);

        Assert.That(Success, Is.True);
    }

    [Test]
    public void TestDestinationFileExists()
    {
        string ApplicationDataDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        string ParserDirectory = Tools.SafeGetSubdirectory(ApplicationDataDirectory, "PgJsonParse", out _);
        string VersionDirectory = Tools.SafeGetSubdirectory(ParserDirectory, "Versions", out _);

        int LastVersion = -1;
        foreach (string Directory in Directory.GetDirectories(VersionDirectory))
            if (int.TryParse(Path.GetFileName(Directory), out int Version) && LastVersion < Version)
                LastVersion = Version;

        Assert.That(LastVersion, Is.GreaterThan(0));

        string LastVersionPath = $"{VersionDirectory}\\{LastVersion}";
        string VersionPath = Tools.SafeGetSubdirectory(Environment.CurrentDirectory, "Temp", out _);
        string CuratedVersionPath = $"{VersionPath}\\Curated";

        if (!Directory.Exists(CuratedVersionPath))
            Directory.CreateDirectory(CuratedVersionPath);

        foreach (string FileName in Directory.GetFiles(LastVersionPath, "*.json"))
        {
            File.Copy(FileName, $"{VersionPath}\\{Path.GetFileName(FileName)}", overwrite: true);
            File.Copy(FileName, $"{CuratedVersionPath}\\{Path.GetFileName(FileName)}", overwrite: true);
        }

        using IFreeSql? Fsql = new FreeSql.FreeSqlBuilder()
            .UseConnectionString(FreeSql.DataType.Sqlite, $"Data Source=test.db")
            .UseAutoSyncStructure(true)
            .Build();
        Assert.That(Fsql, Is.Not.Null);

        Preprocessor Preprocessor = new();
        bool Success = Preprocessor.Preprocess(VersionPath, JsonFileList, Fsql, out _);

        Assert.That(Success, Is.True);
    }

    [Test]
    public void TestFailure()
    {
        string VersionPath = TestTools.GetVersionPath("Preprocessing Failure");
      
        List<JsonFile> InvalidJsonFileList = new()
        {
            new JsonFile("abilities",
                         true,
                         Preprocessor.PreprocessDictionary<AbilityDictionary>,
                         Fixer.NoFix,
                         Preprocessor.SaveSerializedDictionary<AbilityDictionary, Ability>,
                         (IFreeSql fsql, object content) => fsql.Insert((IEnumerable<Ability>)(((AbilityDictionary)content).Values)).ExecuteAffrows(),
                         (IFreeSql fsql) => fsql.Select<Ability>().WithLock().ToDictionary(item => item.Key)),
        };

        using IFreeSql? Fsql = new FreeSql.FreeSqlBuilder()
            .UseConnectionString(FreeSql.DataType.Sqlite, $"Data Source=test.db")
            .UseAutoSyncStructure(true)
            .Build();
        Assert.That(Fsql, Is.Not.Null);

        Preprocessor FailingPreprocessor = new();
        bool Success = FailingPreprocessor.Preprocess(VersionPath, InvalidJsonFileList, Fsql, out _);
        Assert.That(Success, Is.False);
    }

    [Test]
    public void TestFailureNotPretty()
    {
        string VersionPath = TestTools.GetVersionPath("Preprocessing Failure Not Pretty");

        List<JsonFile> InvalidJsonFileList = new()
        {
            new JsonFile("npcs",
                         false,
                         Preprocessor.PreprocessDictionary<NpcDictionary>,
                         Fixer.NoFix,
                         Preprocessor.SaveSerializedDictionary<NpcDictionary, Npc>,
                         (IFreeSql fsql, object content) => fsql.Insert((IEnumerable<Npc>)(((NpcDictionary)content).Values)).ExecuteAffrows(),
                         (IFreeSql fsql) => fsql.Select<Npc>().WithLock().ToDictionary(item => item.Key)),
        };

        using IFreeSql? Fsql = new FreeSql.FreeSqlBuilder()
            .UseConnectionString(FreeSql.DataType.Sqlite, $"Data Source=test.db")
            .UseAutoSyncStructure(true)
            .Build();
        Assert.That(Fsql, Is.Not.Null);

        Preprocessor FailingPreprocessor = new();
        bool Success = FailingPreprocessor.Preprocess(VersionPath, InvalidJsonFileList, Fsql, out _);
        Assert.That(Success, Is.False);
    }

    [Test]
    public void TestBadlyMixedArray()
    {
        string VersionPath = TestTools.GetVersionPath("Preprocessing Badly Mixed Array");

        List<JsonFile> InvalidJsonFileList = new()
        {
            new JsonFile("quests",
                         true,
                         Preprocessor.PreprocessDictionary<QuestDictionary>,
                         Fixer.NoFix,
                         Preprocessor.SaveSerializedDictionary<QuestDictionary, Quest>,
                         (IFreeSql fsql, object content) => fsql.Insert((IEnumerable<Quest>)(((QuestDictionary)content).Values)).ExecuteAffrows(),
                         (IFreeSql fsql) => fsql.Select<Quest>().WithLock().ToDictionary(item => item.Key)),
        };

        using IFreeSql? Fsql = new FreeSql.FreeSqlBuilder()
            .UseConnectionString(FreeSql.DataType.Sqlite, $"Data Source=test.db")
            .UseAutoSyncStructure(true)
            .Build();
        Assert.That(Fsql, Is.Not.Null);

        Preprocessor FailingPreprocessor = new();
        Assert.Throws<PreprocessorException>(() => FailingPreprocessor.Preprocess(VersionPath, InvalidJsonFileList, Fsql, out _));
    }
}

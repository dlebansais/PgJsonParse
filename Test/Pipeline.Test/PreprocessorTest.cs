namespace Pipeline.Test;

using System;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using Preprocessor;

[TestFixture]
public class PreprocessorTest
{
    private static List<JsonFile> JsonFileList = new()
    {
        new JsonFile("abilities", true, Preprocessor.PreprocessDictionary<AbilityDictionary>, Fixer.NoFix, Preprocessor.SaveSerializedContent<AbilityDictionary>),
        new JsonFile("abilitydynamicdots", true, Preprocessor.PreprocessArray<AbilityDynamicDotArray>, Fixer.NoFix, Preprocessor.SaveSerializedContent<AbilityDynamicDotArray>),
        new JsonFile("abilitydynamicspecialvalues", true, Preprocessor.PreprocessArray<AbilityDynamicSpecialValueArray>, Fixer.NoFix, Preprocessor.SaveSerializedContent<AbilityDynamicSpecialValueArray>),
        new JsonFile("abilitykeywords", true, Preprocessor.PreprocessArray<AbilityKeywordArray>, Fixer.NoFix, Preprocessor.SaveSerializedContent<AbilityKeywordArray>),
        new JsonFile("advancementtables", true, Preprocessor.PreprocessDictionary<AdvancementTableDictionary>, Fixer.NoFix, Preprocessor.SaveSerializedContent<AdvancementTableDictionary>),
        new JsonFile("ai", true, Preprocessor.PreprocessDictionary<AIDictionary>, Fixer.NoFix, Preprocessor.SaveSerializedContent<AIDictionary>),
        new JsonFile("areas", true, Preprocessor.PreprocessDictionary<AreaDictionary>, Fixer.NoFix, Preprocessor.SaveSerializedContent<AreaDictionary>),
        new JsonFile("attributes", true, Preprocessor.PreprocessDictionary<AttributeDictionary>, Fixer.FixAttributes, Preprocessor.SaveSerializedContent<AttributeDictionary>),
        new JsonFile("directedgoals", true, Preprocessor.PreprocessDictionary<DirectedGoalDictionary>, Fixer.NoFix, Preprocessor.SaveSerializedContent<DirectedGoalDictionary>),
        new JsonFile("effects", true, Preprocessor.PreprocessDictionary<EffectDictionary>, Fixer.NoFix, Preprocessor.SaveSerializedContent<EffectDictionary>),
        new JsonFile("items", true, Preprocessor.PreprocessDictionary<ItemDictionary>, Fixer.NoFix, Preprocessor.SaveSerializedContent<ItemDictionary>),
        new JsonFile("itemuses", true, Preprocessor.PreprocessDictionary<ItemUseDictionary>, Fixer.NoFix, Preprocessor.SaveSerializedContent<ItemUseDictionary>),
        new JsonFile("lorebookinfo", true, Preprocessor.PreprocessSingle<LoreBookInfo>, Fixer.NoFix, Preprocessor.SaveSerializedContent<LoreBookInfo>),
        new JsonFile("lorebooks", true, Preprocessor.PreprocessDictionary<LoreBookDictionary>, Fixer.NoFix, Preprocessor.SaveSerializedContent<LoreBookDictionary>),
        new JsonFile("npcs", true, Preprocessor.PreprocessDictionary<NpcDictionary>, Fixer.NoFix, Preprocessor.SaveSerializedContent<NpcDictionary>),
        new JsonFile("playertitles", true, Preprocessor.PreprocessDictionary<PlayerTitleDictionary>, Fixer.NoFix, Preprocessor.SaveSerializedContent<PlayerTitleDictionary>),
        new JsonFile("quests", true, Preprocessor.PreprocessDictionary<QuestDictionary>, Fixer.NoFix, Preprocessor.SaveSerializedContent<QuestDictionary>),
        new JsonFile("recipes", true, Preprocessor.PreprocessDictionary<RecipeDictionary>, Fixer.FixRecipes, Preprocessor.SaveSerializedContent<RecipeDictionary>),
        new JsonFile("skills", true, Preprocessor.PreprocessDictionary<SkillDictionary>, Fixer.FixSkills, Preprocessor.SaveSerializedContent<SkillDictionary>),
        new JsonFile("sources_abilities", true, Preprocessor.PreprocessDictionary<SourceAbilityDictionary>, Fixer.FixSourceAbilities, Preprocessor.SaveSerializedContent<SourceAbilityDictionary>),
        new JsonFile("sources_recipes", true, Preprocessor.PreprocessDictionary<SourceRecipeDictionary>, Fixer.FixSourceRecipes, Preprocessor.SaveSerializedContent<SourceRecipeDictionary>),
        new JsonFile("storagevaults", true, Preprocessor.PreprocessDictionary<StorageVaultDictionary>, Fixer.FixStorageVaults, Preprocessor.SaveSerializedContent<StorageVaultDictionary>),
        new JsonFile("tsysclientinfo", true, Preprocessor.PreprocessDictionary<PowerDictionary>, Fixer.FixPowers, Preprocessor.SaveSerializedContent<PowerDictionary>),
        new JsonFile("tsysprofiles", true, Preprocessor.PreprocessDictionary<ProfileDictionary>, Fixer.NoFix, Preprocessor.SaveSerializedContent<ProfileDictionary>),
        new JsonFile("xptables", true, Preprocessor.PreprocessDictionary<XpTableDictionary>, Fixer.FixXpTables, Preprocessor.SaveSerializedContent<XpTableDictionary>),
    };

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

        Preprocessor Preprocessor = new();
        bool Success = Preprocessor.Preprocess(VersionPath, JsonFileList);

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

        Preprocessor Preprocessor = new();
        bool Success = Preprocessor.Preprocess(VersionPath, JsonFileList);

        Assert.That(Success, Is.True);
    }

    [Test]
    public void TestFailure()
    {
        string VersionPath = TestTools.GetVersionPath("Preprocessing Failure");
      
        List<JsonFile> InvalidJsonFileList = new()
        {
            new JsonFile("abilities", true, Preprocessor.PreprocessDictionary<AbilityDictionary>, Fixer.NoFix, Preprocessor.SaveSerializedContent<AbilityDictionary>),
        };

        Preprocessor FailingPreprocessor = new();
        bool Success = FailingPreprocessor.Preprocess(VersionPath, InvalidJsonFileList);
        Assert.That(Success, Is.False);
    }

    [Test]
    public void TestFailureNotPretty()
    {
        string VersionPath = TestTools.GetVersionPath("Preprocessing Failure Not Pretty");

        List<JsonFile> InvalidJsonFileList = new()
        {
            new JsonFile("npcs", false, Preprocessor.PreprocessDictionary<NpcDictionary>, Fixer.NoFix, Preprocessor.SaveSerializedContent<NpcDictionary>),
        };

        Preprocessor FailingPreprocessor = new();
        bool Success = FailingPreprocessor.Preprocess(VersionPath, InvalidJsonFileList);
        Assert.That(Success, Is.False);
    }

    [Test]
    public void TestBadlyMixedArray()
    {
        string VersionPath = TestTools.GetVersionPath("Preprocessing Badly Mixed Array");

        List<JsonFile> InvalidJsonFileList = new()
        {
            new JsonFile("quests", true, Preprocessor.PreprocessDictionary<QuestDictionary>, Fixer.NoFix, Preprocessor.SaveSerializedContent<QuestDictionary>),
        };

        Preprocessor FailingPreprocessor = new();
        Assert.Throws<PreprocessorException>(() => FailingPreprocessor.Preprocess(VersionPath, InvalidJsonFileList));
    }
}

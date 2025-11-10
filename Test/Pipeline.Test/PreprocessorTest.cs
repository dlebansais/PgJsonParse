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
        new JsonFile("abilities", true, Preprocessor.PreprocessDictionary<AbilityDictionary>, Fixer.NoFix, Preprocessor.SaveSerializedDictionary<AbilityDictionary, Ability>, (IFreeSql fsql, object content) => fsql.Insert((IEnumerable<Ability>)(((AbilityDictionary)content).Values)).ExecuteAffrows()),
        new JsonFile("abilitydynamicdots", true, Preprocessor.PreprocessArray<AbilityDynamicDotArray>, Fixer.NoFix, Preprocessor.SaveSerializedList<AbilityDynamicDotArray, AbilityDynamicDot>, (IFreeSql fsql, object content) => fsql.Insert((IEnumerable<AbilityDynamicDot>)content).ExecuteAffrows()),
        new JsonFile("abilitydynamicspecialvalues", true, Preprocessor.PreprocessArray<AbilityDynamicSpecialValueArray>, Fixer.NoFix, Preprocessor.SaveSerializedList<AbilityDynamicSpecialValueArray, AbilityDynamicSpecialValue>, (IFreeSql fsql, object content) => fsql.Insert((IEnumerable<AbilityDynamicSpecialValue>)content).ExecuteAffrows()),
        new JsonFile("abilitykeywords", true, Preprocessor.PreprocessArray<AbilityKeywordArray>, Fixer.NoFix, Preprocessor.SaveSerializedList<AbilityKeywordArray, AbilityKeyword>, (IFreeSql fsql, object content) => fsql.Insert((IEnumerable<AbilityKeyword>)content).ExecuteAffrows()),
        new JsonFile("advancementtables", true, Preprocessor.PreprocessDictionary<AdvancementTableDictionary>, Fixer.NoFix, Preprocessor.SaveSerializedDictionary<AdvancementTableDictionary, AdvancementTable>, (IFreeSql fsql, object content) => fsql.Insert((IEnumerable<AdvancementTable>)(((AdvancementTableDictionary)content).Values)).ExecuteAffrows()),
        new JsonFile("ai", true, Preprocessor.PreprocessDictionary<AIDictionary>, Fixer.NoFix, Preprocessor.SaveSerializedDictionary<AIDictionary, AI>, (IFreeSql fsql, object content) => fsql.Insert((IEnumerable<AI>)(((AIDictionary)content).Values)).ExecuteAffrows()),
        new JsonFile("areas", true, Preprocessor.PreprocessDictionary<AreaDictionary>, Fixer.NoFix, Preprocessor.SaveSerializedDictionary<AreaDictionary, Area>, (IFreeSql fsql, object content) => fsql.Insert((IEnumerable<Area>)(((AreaDictionary)content).Values)).ExecuteAffrows()),
        new JsonFile("attributes", true, Preprocessor.PreprocessDictionary<AttributeDictionary>, Fixer.FixAttributes, Preprocessor.SaveSerializedDictionary<AttributeDictionary, global::Preprocessor.Attribute>, (IFreeSql fsql, object content) => fsql.Insert((IEnumerable<global::Preprocessor.Attribute>)(((AttributeDictionary)content).Values)).ExecuteAffrows()),
        new JsonFile("directedgoals", true, Preprocessor.PreprocessDictionary<DirectedGoalDictionary>, Fixer.NoFix, Preprocessor.SaveSerializedDictionary<DirectedGoalDictionary, DirectedGoal>, (IFreeSql fsql, object content) => fsql.Insert((IEnumerable<DirectedGoal>)(((DirectedGoalDictionary)content).Values)).ExecuteAffrows()),
        new JsonFile("effects", true, Preprocessor.PreprocessDictionary<EffectDictionary>, Fixer.NoFix, Preprocessor.SaveSerializedDictionary<EffectDictionary, Effect>, (IFreeSql fsql, object content) => fsql.Insert((IEnumerable<Effect>)(((EffectDictionary)content).Values)).ExecuteAffrows()),
        new JsonFile("items", true, Preprocessor.PreprocessDictionary<ItemDictionary>, Fixer.NoFix, Preprocessor.SaveSerializedDictionary<ItemDictionary, Item>, (IFreeSql fsql, object content) => fsql.Insert((IEnumerable<Item>)(((ItemDictionary)content).Values)).ExecuteAffrows()),
        new JsonFile("itemuses", true, Preprocessor.PreprocessDictionary<ItemUseDictionary>, Fixer.NoFix, Preprocessor.SaveSerializedDictionary<ItemUseDictionary, ItemUse>, (IFreeSql fsql, object content) => fsql.Insert((IEnumerable<ItemUse>)(((ItemUseDictionary)content).Values)).ExecuteAffrows()),
        new JsonFile("lorebookinfo", true, Preprocessor.PreprocessSingle<LoreBookInfo>, Fixer.NoFix, Preprocessor.SaveSerializedSingle<LoreBookInfo>, (IFreeSql fsql, object content) => fsql.Insert((LoreBookInfo)content).ExecuteAffrows()),
        new JsonFile("lorebooks", true, Preprocessor.PreprocessDictionary<LoreBookDictionary>, Fixer.NoFix, Preprocessor.SaveSerializedDictionary<LoreBookDictionary, LoreBook>, (IFreeSql fsql, object content) => fsql.Insert((IEnumerable<LoreBook>)(((LoreBookDictionary)content).Values)).ExecuteAffrows()),
        new JsonFile("npcs", true, Preprocessor.PreprocessDictionary<NpcDictionary>, Fixer.NoFix, Preprocessor.SaveSerializedDictionary<NpcDictionary, Npc>, (IFreeSql fsql, object content) => fsql.Insert((IEnumerable<Npc>)(((NpcDictionary)content).Values)).ExecuteAffrows()),
        new JsonFile("playertitles", true, Preprocessor.PreprocessDictionary<PlayerTitleDictionary>, Fixer.NoFix, Preprocessor.SaveSerializedDictionary<PlayerTitleDictionary, PlayerTitle>, (IFreeSql fsql, object content) => fsql.Insert((IEnumerable<PlayerTitle>)(((PlayerTitleDictionary)content).Values)).ExecuteAffrows()),
        new JsonFile("quests", true, Preprocessor.PreprocessDictionary<QuestDictionary>, Fixer.NoFix, Preprocessor.SaveSerializedDictionary<QuestDictionary, Quest>, (IFreeSql fsql, object content) => fsql.Insert((IEnumerable<Quest>)(((QuestDictionary)content).Values)).ExecuteAffrows()),
        new JsonFile("recipes", true, Preprocessor.PreprocessDictionary<RecipeDictionary>, Fixer.FixRecipes, Preprocessor.SaveSerializedDictionary<RecipeDictionary, Recipe>, (IFreeSql fsql, object content) => fsql.Insert((IEnumerable<Recipe>)(((RecipeDictionary)content).Values)).ExecuteAffrows()),
        new JsonFile("skills", true, Preprocessor.PreprocessDictionary<SkillDictionary>, Fixer.FixSkills, Preprocessor.SaveSerializedDictionary<SkillDictionary, Skill>, (IFreeSql fsql, object content) => fsql.Insert((IEnumerable<Skill>)(((SkillDictionary)content).Values)).ExecuteAffrows()),
        new JsonFile("sources_abilities", true, Preprocessor.PreprocessDictionary<SourceAbilityDictionary>, Fixer.FixSourceAbilities, Preprocessor.SaveSerializedDictionary<SourceAbilityDictionary, SourceAbility>, (IFreeSql fsql, object content) => fsql.Insert((IEnumerable<SourceAbility>)(((SourceAbilityDictionary)content).Values)).ExecuteAffrows()),
        new JsonFile("sources_items", true, Preprocessor.PreprocessDictionary<SourceItemDictionary>, Fixer.NoFix, Preprocessor.SaveSerializedDictionary<SourceItemDictionary, SourceItem>, (IFreeSql fsql, object content) => fsql.Insert((IEnumerable<SourceItem>)(((SourceItemDictionary)content).Values)).ExecuteAffrows()),
        new JsonFile("sources_recipes", true, Preprocessor.PreprocessDictionary<SourceRecipeDictionary>, Fixer.FixSourceRecipes, Preprocessor.SaveSerializedDictionary<SourceRecipeDictionary, SourceRecipe>, (IFreeSql fsql, object content) => fsql.Insert((IEnumerable<SourceRecipe>)(((SourceRecipeDictionary)content).Values)).ExecuteAffrows()),
        new JsonFile("storagevaults", true, Preprocessor.PreprocessDictionary<StorageVaultDictionary>, Fixer.FixStorageVaults, Preprocessor.SaveSerializedDictionary<StorageVaultDictionary, StorageVault>, (IFreeSql fsql, object content) => fsql.Insert((IEnumerable<StorageVault>)(((StorageVaultDictionary)content).Values)).ExecuteAffrows()),
        new JsonFile("tsysclientinfo", true, Preprocessor.PreprocessDictionary<PowerDictionary>, Fixer.FixPowers, Preprocessor.SaveSerializedDictionary<PowerDictionary, Power>, (IFreeSql fsql, object content) => fsql.Insert((IEnumerable<Power>)(((PowerDictionary)content).Values)).ExecuteAffrows()),
        new JsonFile("tsysprofiles", true, Preprocessor.PreprocessDictionary<ProfileDictionary>, Fixer.NoFix, Preprocessor.SaveSerializedDictionary<ProfileDictionary, Profile>, (IFreeSql fsql, object content) => fsql.Insert((IEnumerable<Profile>)(((ProfileDictionary)content).Values)).ExecuteAffrows()),
        new JsonFile("xptables", true, Preprocessor.PreprocessDictionary<XpTableDictionary>, Fixer.FixXpTables, Preprocessor.SaveSerializedDictionary<XpTableDictionary, XpTable>, (IFreeSql fsql, object content) => fsql.Insert((IEnumerable<XpTable>)(((XpTableDictionary)content).Values)).ExecuteAffrows()),
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
            new JsonFile("abilities", true, Preprocessor.PreprocessDictionary<AbilityDictionary>, Fixer.NoFix, Preprocessor.SaveSerializedDictionary<AbilityDictionary, Ability>, (IFreeSql fsql, object content) => fsql.Insert((IEnumerable<Ability>)(((AbilityDictionary)content).Values)).ExecuteAffrows()),
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
            new JsonFile("npcs", false, Preprocessor.PreprocessDictionary<NpcDictionary>, Fixer.NoFix, Preprocessor.SaveSerializedDictionary<NpcDictionary, Npc>, (IFreeSql fsql, object content) => fsql.Insert((IEnumerable<Npc>)(((NpcDictionary)content).Values)).ExecuteAffrows()),
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
            new JsonFile("quests", true, Preprocessor.PreprocessDictionary<QuestDictionary>, Fixer.NoFix, Preprocessor.SaveSerializedDictionary<QuestDictionary, Quest>, (IFreeSql fsql, object content) => fsql.Insert((IEnumerable<Quest>)(((QuestDictionary)content).Values)).ExecuteAffrows()),
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

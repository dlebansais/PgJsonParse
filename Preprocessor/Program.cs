namespace Preprocessor;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using Downloader;

internal class Program
{
    private static List<JsonFile> JsonFiles = new()
    {
        new JsonFile("abilities", true, Preprocessor.PreprocessDictionary<AbilityDictionary>, Fixer.NoFix, Preprocessor.SaveSerializedDictionary<AbilityDictionary, Ability>, (IFreeSql fsql, object content) => fsql.Insert((IEnumerable<Ability>)(((AbilityDictionary)content).Values)).ExecuteAffrows()),
        new JsonFile("abilitydynamicdots", true, Preprocessor.PreprocessArray<AbilityDynamicDotArray>, Fixer.NoFix, Preprocessor.SaveSerializedList<AbilityDynamicDotArray, AbilityDynamicDot>, (IFreeSql fsql, object content) => fsql.Insert((IEnumerable<AbilityDynamicDot>)content).ExecuteAffrows()),
        new JsonFile("abilitydynamicspecialvalues", true, Preprocessor.PreprocessArray<AbilityDynamicSpecialValueArray>, Fixer.NoFix, Preprocessor.SaveSerializedList<AbilityDynamicSpecialValueArray, AbilityDynamicSpecialValue>, (IFreeSql fsql, object content) => fsql.Insert((IEnumerable<AbilityDynamicSpecialValue>)content).ExecuteAffrows()),
        new JsonFile("abilitykeywords", true, Preprocessor.PreprocessArray<AbilityKeywordArray>, Fixer.NoFix, Preprocessor.SaveSerializedList<AbilityKeywordArray, AbilityKeyword>, (IFreeSql fsql, object content) => fsql.Insert((IEnumerable<AbilityKeyword>)content).ExecuteAffrows()),
        new JsonFile("advancementtables", true, Preprocessor.PreprocessDictionary<AdvancementTableDictionary>, Fixer.NoFix, Preprocessor.SaveSerializedDictionary<AdvancementTableDictionary, AdvancementTable>, (IFreeSql fsql, object content) => fsql.Insert((IEnumerable<AdvancementTable>)(((AdvancementTableDictionary)content).Values)).ExecuteAffrows()),
        new JsonFile("ai", true, Preprocessor.PreprocessDictionary<AIDictionary>, Fixer.NoFix, Preprocessor.SaveSerializedDictionary<AIDictionary, AI>, (IFreeSql fsql, object content) => fsql.Insert((IEnumerable<AI>)(((AIDictionary)content).Values)).ExecuteAffrows()),
        new JsonFile("areas", true, Preprocessor.PreprocessDictionary<AreaDictionary>, Fixer.NoFix, Preprocessor.SaveSerializedDictionary<AreaDictionary, Area>, (IFreeSql fsql, object content) => fsql.Insert((IEnumerable<Area>)(((AreaDictionary)content).Values)).ExecuteAffrows()),
        new JsonFile("attributes", true, Preprocessor.PreprocessDictionary<AttributeDictionary>, Fixer.FixAttributes, Preprocessor.SaveSerializedDictionary<AttributeDictionary, Attribute>, (IFreeSql fsql, object content) => fsql.Insert((IEnumerable<Attribute>)(((AttributeDictionary)content).Values)).ExecuteAffrows()),
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

    private class StringArrayHandler : FreeSql.Internal.Model.TypeHandler<string[]>
    {
        public override string[] Deserialize(object value)
        {
            return JsonSerializer.Deserialize<string[]>((string)value)!;
        }

        public override object Serialize(string[] value)
        {
            return JsonSerializer.Serialize(value);
        }
    }

    private class IntArrayHandler : FreeSql.Internal.Model.TypeHandler<int[]>
    {
        public override int[] Deserialize(object value)
        {
            return JsonSerializer.Deserialize<int[]>((string)value)!;
        }

        public override object Serialize(int[] value)
        {
            return JsonSerializer.Serialize(value);
        }
    }

    private class DecimalArrayHandler : FreeSql.Internal.Model.TypeHandler<decimal[]>
    {
        public override decimal[] Deserialize(object value)
        {
            return JsonSerializer.Deserialize<decimal[]>((string)value)!;
        }

        public override object Serialize(decimal[] value)
        {
            return JsonSerializer.Serialize(value);
        }
    }

    private class StringIntDictionaryHandler : FreeSql.Internal.Model.TypeHandler<Dictionary<string, int>>
    {
        public override Dictionary<string, int> Deserialize(object value)
        {
            return JsonSerializer.Deserialize<Dictionary<string, int>>((string)value)!;
        }

        public override object Serialize(Dictionary<string, int> value)
        {
            return JsonSerializer.Serialize(value);
        }
    }

    static int Main(string[] args)
    {
        string ApplicationDataDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        string ParserDirectory = Tools.SafeGetSubdirectory(ApplicationDataDirectory, "PgJsonParse", out _);
        string VersionDirectory = Tools.SafeGetSubdirectory(ParserDirectory, "Versions", out _);
        string AnyVersionDirectory = Tools.SafeGetSubdirectory(VersionDirectory, "000", out _);
        string DatabaseFile = $"{AnyVersionDirectory}/test.db";

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

        FreeSql.Internal.Utils.TypeHandlers.TryAdd(typeof(string[]), new StringArrayHandler());
        FreeSql.Internal.Utils.TypeHandlers.TryAdd(typeof(int[]), new IntArrayHandler());
        FreeSql.Internal.Utils.TypeHandlers.TryAdd(typeof(decimal[]), new DecimalArrayHandler());
        FreeSql.Internal.Utils.TypeHandlers.TryAdd(typeof(Dictionary<string, int>), new StringIntDictionaryHandler());

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

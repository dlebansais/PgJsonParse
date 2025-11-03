namespace Preprocessor;

using System;
using System.Collections.Generic;
using System.IO;
using Downloader;

internal class Program
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
        new JsonFile("sources_items", true, Preprocessor.PreprocessDictionary<SourceItemDictionary>, Fixer.NoFix, Preprocessor.SaveSerializedContent<SourceItemDictionary>),
        new JsonFile("sources_recipes", true, Preprocessor.PreprocessDictionary<SourceRecipeDictionary>, Fixer.FixSourceRecipes, Preprocessor.SaveSerializedContent<SourceRecipeDictionary>),
        new JsonFile("storagevaults", true, Preprocessor.PreprocessDictionary<StorageVaultDictionary>, Fixer.FixStorageVaults, Preprocessor.SaveSerializedContent<StorageVaultDictionary>),
        new JsonFile("tsysclientinfo", true, Preprocessor.PreprocessDictionary<PowerDictionary>, Fixer.FixPowers, Preprocessor.SaveSerializedContent<PowerDictionary>),
        new JsonFile("tsysprofiles", true, Preprocessor.PreprocessDictionary<ProfileDictionary>, Fixer.NoFix, Preprocessor.SaveSerializedContent<ProfileDictionary>),
        new JsonFile("xptables", true, Preprocessor.PreprocessDictionary<XpTableDictionary>, Fixer.FixXpTables, Preprocessor.SaveSerializedContent<XpTableDictionary>),
    };

    static int Main(string[] args)
    {
        string ApplicationDataDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        string ParserDirectory = Tools.SafeGetSubdirectory(ApplicationDataDirectory, "PgJsonParse", out _);
        string VersionDirectory = Tools.SafeGetSubdirectory(ParserDirectory, "Versions", out _);

        Downloader Downloader = new();
        if (!Downloader.Download(JsonFileList, VersionDirectory, out int version, out string VersionPath))
            return -1;

        Preprocessor Preprocessor = new();
        if (!Preprocessor.Preprocess(VersionPath, JsonFileList, out string CuratedVersionDirectory))
            return -2;

        string AnyVersionDirectory = Tools.SafeGetSubdirectory(VersionDirectory, "000", out _);
        foreach (string OldFile in Directory.GetFiles(AnyVersionDirectory))
            File.Delete(OldFile);
        foreach (string NewFile in Directory.GetFiles(CuratedVersionDirectory))
            File.Copy(NewFile, Path.Combine(AnyVersionDirectory, Path.GetFileName(NewFile)));
        File.WriteAllText(Path.Combine(AnyVersionDirectory, "_version.txt"), version.ToString());

        return 0;
    }
}

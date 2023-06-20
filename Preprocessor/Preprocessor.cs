namespace Preprocessor;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;

internal class Preprocessor
{
    private static List<JsonConverter> JsonConverters = new()
    {
        new IntDictionaryJsonConverter<Ability, RawAbility, AbilityDictionary>("ability"),
        new AdvancementTableDictionaryJsonConverter(),
        new StringDictionaryJsonConverter<AI, AI, AIDictionary>(),
        new StringDictionaryJsonConverter<AIAbility, RawAIAbility, AIAbilityDictionary>(),
        new StringDictionaryJsonConverter<Area, Area, AreaDictionary>(),
        new StringDictionaryJsonConverter<Attribute, Attribute, AttributeDictionary>(),
        new DirectedGoalDictionaryJsonConverter(),
        new StringDictionaryJsonConverter<Effect, RawEffect, EffectDictionary>(),
        new IntDictionaryJsonConverter<Item, Item, ItemDictionary>("item"),
        new SkillRequirementDictionaryJsonConverter(),
        new IntDictionaryJsonConverter<ItemUse, ItemUse, ItemUseDictionary>("item"),
        new StringDictionaryJsonConverter<LoreBookCategory, RawLoreBookCategory, LoreBookCategoryDictionary>(),
        new IntDictionaryJsonConverter<LoreBook, LoreBook, LoreBookDictionary>("Book"),
        new StringDictionaryJsonConverter<Npc, RawNpc, NpcDictionary>(),
        new IntDictionaryJsonConverter<PlayerTitle, PlayerTitle, PlayerTitleDictionary>("Title"),
        new IntDictionaryJsonConverter<Quest, Quest, QuestDictionary>("quest"),
        new IntDictionaryJsonConverter<Recipe, RawRecipe, RecipeDictionary>("recipe"),
        new StringDictionaryJsonConverter<Skill, Skill, SkillDictionary>(),
        new SkillRewardCollectionJsonConverter(),
        new SkillAdvancementHintCollectionJsonConverter(),
        new SkillLevelCapCollectionJsonConverter(),
        new SkillReportCollectionJsonConverter(),
        new IntDictionaryJsonConverter<SourceAbility, RawSourceAbility, SourceAbilityDictionary>("ability"),
        new IntDictionaryJsonConverter<SourceRecipe, RawSourceRecipe, SourceRecipeDictionary>("recipe"),
        new StringDictionaryJsonConverter<StorageVault, StorageVault, StorageVaultDictionary>(),
        new IntDictionaryJsonConverter<Power, Power, PowerDictionary>("power"),
        new IntDictionaryJsonConverter<PowerTier, PowerTier, PowerTierDictionary>("id"),
        new IntDictionaryJsonConverter<XpTable, XpTable, XpTableDictionary>("Table"),
    };

    static void Main(string[] args)
    {
        PreprocessDictionary<AbilityDictionary>("abilities");
        PreprocessDictionary<AdvancementTableDictionary>("advancementtables");
        PreprocessDictionary<AIDictionary>("ai");
        PreprocessDictionary<AreaDictionary>("areas");
        PreprocessDictionary<AttributeDictionary>("attributes");
        PreprocessDictionary<DirectedGoalDictionary>("directedgoals");
        PreprocessDictionary<EffectDictionary>("effects");
        PreprocessDictionary<ItemDictionary>("items");
        PreprocessDictionary<ItemUseDictionary>("itemuses");
        PreprocessSingle<LoreBookInfo>("lorebookinfo");
        PreprocessDictionary<LoreBookDictionary>("lorebooks");
        PreprocessDictionary<NpcDictionary>("npcs", isPretty: false);
        PreprocessDictionary<PlayerTitleDictionary>("playertitles");
        PreprocessDictionary<QuestDictionary>("quests");
        PreprocessDictionary<RecipeDictionary>("recipes");
        PreprocessDictionary<SkillDictionary>("skills");
        PreprocessDictionary<SourceAbilityDictionary>("sources_abilities");
        PreprocessDictionary<SourceRecipeDictionary>("sources_recipes");
        PreprocessDictionary<StorageVaultDictionary>("storagevaults");
        PreprocessDictionary<PowerDictionary>("tsysclientinfo");
        PreprocessDictionary<XpTableDictionary>("xptables");

        Debug.WriteLine("Done");
    }

    static void PreprocessSingle<T>(string fileName, bool isPretty = true)
    {
        if (Preprocess<T>(fileName, isPretty, out _))
            Debug.WriteLine(" OK");
    }

    static void PreprocessDictionary<T>(string fileName, bool isPretty = true)
        where T : ICollection
    {
        if (Preprocess(fileName, isPretty, out T ObjectCollection))
            Debug.WriteLine($" OK ({ObjectCollection.Count})");
    }

    static bool Preprocess<T>(string fileName, bool isPretty, out T result)
    {
        Debug.Write($"Processing {fileName}.json...");

        string SourceDirectory = @"C:\Users\DLB\AppData\Roaming\PgJsonParse\Versions\387";
        string SourceFilePath = $"{SourceDirectory}\\{fileName}.json";

        string ReadContent = GetReadContent(SourceFilePath, isPretty);
        result = GetDeserializedObjects<T>(ReadContent);
        EnsureAlphabeticalOrder(typeof(T));
        string WriteContent = GetWriteContent(result, isPretty);

        if (ReadContent != WriteContent)
        {
            Debug.WriteLine("\r\nERROR: Difference found");

            if (isPretty)
            {
                Comparer Comparer = new();
                Comparer.Compare(ReadContent, WriteContent);
            }

            return false;
        }
        else
        {
            string DestinationDirectory = @"C:\Users\DLB\AppData\Roaming\PgJsonParse\Versions\387\Curated";

            if (!Directory.Exists(DestinationDirectory))
                Directory.CreateDirectory(DestinationDirectory);

            string DestinationFilePath = $"{DestinationDirectory}\\{fileName}.json";
            SaveSerializedContent(DestinationFilePath, result);

            return true;
        }
    }

    static string GetReadContent(string filePath, bool isPretty)
    {
        using FileStream Stream = new(filePath, FileMode.Open, FileAccess.Read);
        using StreamReader Reader = new(Stream, Encoding.UTF8);
        string ReadContent = Reader.ReadToEnd();

        if (isPretty)
        {
            string Indent = string.Empty;
            while (ReadContent.Contains("\t"))
            {
                ReadContent = ReadContent.Replace($"\n{Indent}\t", $"\n{Indent}  ");
                Indent += "  ";
            }
        }

        ReadContent = ReadContent.Replace("[ ]", "[]");
        ReadContent = ReadContent.Replace("{ }", "{}");
        ReadContent = ReadContent.Replace("\"AdvancementTable\": null,", "\"AdvancementTable\": \"null\",");

        return ReadContent;
    }

    private static T GetDeserializedObjects<T>(string readContent)
    {
        JsonSerializerOptions ReadOptions = new();
        JsonConverters.ForEach(ReadOptions.Converters.Add);
        T Result = JsonSerializer.Deserialize<T>(readContent, ReadOptions) ?? throw new InvalidCastException();

        return Result;
    }

    private static void EnsureAlphabeticalOrder(Type type)
    {
        PropertyInfo[] Properties;

        if (type.BaseType.IsGenericType)
        {
            Type[] GenericArguments = type.BaseType.GetGenericArguments();
            Properties = GenericArguments[GenericArguments.Length - 1].GetProperties();
        }
        else
            Properties = type.GetProperties();

        List<string> PropertyNames = new();

        foreach (PropertyInfo Property in Properties)
        {
            PropertyNames.Add(Property.Name);

            if (Property.PropertyType.FullName.StartsWith("Preprocessor"))
            {
                Type PropertyType = Property.PropertyType;
                if (PropertyType.IsArray)
                    PropertyType = PropertyType.GetElementType();

                EnsureAlphabeticalOrder(PropertyType);
            }
        }

        List<string> SortedPropertyNames = new(PropertyNames);
        SortedPropertyNames.Sort(StringComparer.Ordinal);

        for (int i = 0; i < SortedPropertyNames.Count; i++)
            Debug.Assert(SortedPropertyNames[i] == PropertyNames[i], $"Property {SortedPropertyNames[i]} in {type.Name} is not declared in order");
    }

    private static string GetWriteContent<T>(T objects, bool isPretty)
    {
        JsonSerializerOptions WriteOptions = new();
        JsonConverters.ForEach(WriteOptions.Converters.Add);
        WriteOptions.WriteIndented = isPretty;
        WriteOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        WriteOptions.NumberHandling = JsonNumberHandling.Strict;
        WriteOptions.Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
        string WriteContent = JsonSerializer.Serialize(objects, WriteOptions);

        if (isPretty)
            WriteContent = WriteContent.Replace("\r\n", "\n");

        return WriteContent;
    }

    private static void SaveSerializedContent<T>(string filePath, T objects)
    {
        JsonSerializerOptions WriteOptions = new();
        WriteOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
        WriteOptions.WriteIndented = true;
        WriteOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        WriteOptions.NumberHandling = JsonNumberHandling.Strict;
        string CuratedContent = JsonSerializer.Serialize(objects, WriteOptions);

        using FileStream Stream = new(filePath, FileMode.Create, FileAccess.Write);
        using StreamWriter Writer = new(Stream, Encoding.UTF8);
        Writer.Write(CuratedContent);
    }

    public static object? FromSingleOrMultiple<T>(T[]? items, bool isSingle)
    {
        if (items is null)
            return null;
        else if (isSingle)
            return items[0];
        else
            return items;
    }

    public static T[]? ToSingleOrMultiple<T>(object? element, out bool isSingle)
    {
        T[]? Result;

        if (element is null)
        {
            isSingle = false;
            return null;
        }
        else if (ToSingleItem<T>(element, out Result))
        {
            isSingle = true;
            return Result;
        }
        else if (ToMultipleItems<T>(element, out Result))
        {
            isSingle = false;
            return Result;
        }
        else
            throw new InvalidCastException();
    }

    public static bool ToSingleItem<T>(object element, out T[] result)
    {
        if (element is JsonElement AsSingle && AsSingle.ValueKind == JsonValueKind.Object)
        {
            result = new T[1];
            result[0] = AsSingle.Deserialize<T>() ?? throw new InvalidCastException();
            return true;
        }

        result = null!;
        return false;
    }

    public static bool ToMultipleItems<T>(object element, out T[] result)
    {
        if (element is JsonElement AsMultiple && AsMultiple.ValueKind == JsonValueKind.Array)
        {
            int ArrayLength = AsMultiple.GetArrayLength();
            result = new T[ArrayLength];

            for (int i = 0; i < ArrayLength; i++)
                result[i] = AsMultiple[i].Deserialize<T>() ?? throw new InvalidCastException();

            return true;
        }

        result = null!;
        return false;
    }

    public static object? FromNumberOrString(int? item, bool isNumber)
    {
        if (item is null)
            return null;
        else if (isNumber)
            return item;
        else
            return item.ToString();
    }

    public static int? ToNumberOrString(object? element, out bool isNumber)
    {
        if (element is null)
        {
            isNumber = false;
            return null;
        }
        else if (element is JsonElement AsNumber && AsNumber.ValueKind == JsonValueKind.Number)
        {
            isNumber = true;
            return AsNumber.GetInt32();
        }
        else if (element is JsonElement AsString && AsString.ValueKind == JsonValueKind.String)
        {
            isNumber = false;
            return int.Parse(AsString.GetString());
        }
        else
            throw new InvalidCastException();
    }
}

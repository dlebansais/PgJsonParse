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
    private List<JsonConverter> JsonConverters = new()
    {
        new IntDictionaryJsonConverter<Ability, RawAbility, AbilityDictionary>("ability"),
        new AdvancementTableDictionaryJsonConverter(),
        new StringDictionaryJsonConverter<AI, AI, AIDictionary>(),
        new StringDictionaryJsonConverter<AIAbility, RawAIAbility, AIAbilityDictionary>(),
        new StringDictionaryJsonConverter<Area, Area, AreaDictionary>(),
        new StringDictionaryJsonConverter<Attribute, Attribute, AttributeDictionary>(),
        new DirectedGoalDictionaryJsonConverter(),
        new IntDictionaryJsonConverter<Effect, RawEffect, EffectDictionary>("effect"),
        new IntDictionaryJsonConverter<Item, RawItem, ItemDictionary>("item"),
        new SkillRequirementDictionaryJsonConverter(),
        new IntDictionaryJsonConverter<ItemUse, ItemUse, ItemUseDictionary>("item"),
        new StringDictionaryJsonConverter<LoreBookCategory, RawLoreBookCategory, LoreBookCategoryDictionary>(),
        new IntDictionaryJsonConverter<LoreBook, LoreBook, LoreBookDictionary>("Book"),
        new StringDictionaryJsonConverter<Npc, RawNpc, NpcDictionary>(),
        new IntDictionaryJsonConverter<PlayerTitle, RawPlayerTitle, PlayerTitleDictionary>("Title"),
        new IntDictionaryJsonConverter<Quest, RawQuest, QuestDictionary>("quest"),
        new IntDictionaryJsonConverter<Recipe, RawRecipe, RecipeDictionary>("recipe"),
        new StringDictionaryJsonConverter<Skill, RawSkill, SkillDictionary>(),
        new SkillRewardCollectionJsonConverter(),
        new SkillAdvancementHintCollectionJsonConverter(),
        new SkillLevelCapCollectionJsonConverter(),
        new SkillReportCollectionJsonConverter(),
        new IntDictionaryJsonConverter<SourceAbility, RawSourceAbility, SourceAbilityDictionary>("ability"),
        new IntDictionaryJsonConverter<SourceRecipe, RawSourceRecipe, SourceRecipeDictionary>("recipe"),
        new StringDictionaryJsonConverter<StorageVault, RawStorageVault, StorageVaultDictionary>(),
        new IntDictionaryJsonConverter<Power, Power, PowerDictionary>("power"),
        new IntDictionaryJsonConverter<PowerTier, PowerTier, PowerTierDictionary>("id"),
        new IntDictionaryJsonConverter<XpTable, XpTable, XpTableDictionary>("Table"),
    };

    public bool Preprocess()
    {
        List<JsonFile> JsonFileList = new()
        {
            new JsonFile("abilities", true, PreprocessDictionary<AbilityDictionary>, Fixer.NoFix, SaveSerializedContent<AbilityDictionary>),
            new JsonFile("advancementtables", true, PreprocessDictionary<AdvancementTableDictionary>, Fixer.NoFix, SaveSerializedContent<AdvancementTableDictionary>),
            new JsonFile("ai", true, PreprocessDictionary<AIDictionary>, Fixer.NoFix, SaveSerializedContent<AIDictionary>),
            new JsonFile("areas", true, PreprocessDictionary<AreaDictionary>, Fixer.NoFix, SaveSerializedContent<AreaDictionary>),
            new JsonFile("attributes", true, PreprocessDictionary<AttributeDictionary>, Fixer.FixAttributes, SaveSerializedContent<AttributeDictionary>),
            new JsonFile("directedgoals", true, PreprocessDictionary<DirectedGoalDictionary>, Fixer.NoFix, SaveSerializedContent<DirectedGoalDictionary>),
            new JsonFile("effects", true, PreprocessDictionary<EffectDictionary>, Fixer.NoFix, SaveSerializedContent<EffectDictionary>),
            new JsonFile("items", true, PreprocessDictionary<ItemDictionary>, Fixer.NoFix, SaveSerializedContent<ItemDictionary>),
            new JsonFile("itemuses", true, PreprocessDictionary<ItemUseDictionary>, Fixer.NoFix, SaveSerializedContent<ItemUseDictionary>),
            new JsonFile("lorebookinfo", true, PreprocessSingle<LoreBookInfo>, Fixer.NoFix, SaveSerializedContent<LoreBookInfo>),
            new JsonFile("lorebooks", true, PreprocessDictionary<LoreBookDictionary>, Fixer.NoFix, SaveSerializedContent<LoreBookDictionary>),
            new JsonFile("npcs", false, PreprocessDictionary<NpcDictionary>, Fixer.NoFix, SaveSerializedContent<NpcDictionary>),
            new JsonFile("playertitles", true, PreprocessDictionary<PlayerTitleDictionary>, Fixer.NoFix, SaveSerializedContent<PlayerTitleDictionary>),
            new JsonFile("quests", true, PreprocessDictionary<QuestDictionary>, Fixer.NoFix, SaveSerializedContent<QuestDictionary>),
            new JsonFile("recipes", true, PreprocessDictionary<RecipeDictionary>, Fixer.FixRecipes, SaveSerializedContent<RecipeDictionary>),
            new JsonFile("skills", true, PreprocessDictionary<SkillDictionary>, Fixer.FixSkills, SaveSerializedContent<SkillDictionary>),
            new JsonFile("sources_abilities", true, PreprocessDictionary<SourceAbilityDictionary>, Fixer.FixSourceAbilities, SaveSerializedContent<SourceAbilityDictionary>),
            new JsonFile("sources_recipes", true, PreprocessDictionary<SourceRecipeDictionary>, Fixer.FixSourceRecipes, SaveSerializedContent<SourceRecipeDictionary>),
            new JsonFile("storagevaults", true, PreprocessDictionary<StorageVaultDictionary>, Fixer.FixStorageVaults, SaveSerializedContent<StorageVaultDictionary>),
            new JsonFile("tsysclientinfo", true, PreprocessDictionary<PowerDictionary>, Fixer.FixPowers, SaveSerializedContent<PowerDictionary>),
            new JsonFile("xptables", true, PreprocessDictionary<XpTableDictionary>, Fixer.FixXpTables, SaveSerializedContent<XpTableDictionary>),
        };

        string DestinationDirectory = @"C:\Users\DLB\AppData\Roaming\PgJsonParse\Versions\387\Curated";

        if (!Directory.Exists(DestinationDirectory))
            Directory.CreateDirectory(DestinationDirectory);

        foreach (JsonFile File in JsonFileList)
        {
            (bool Success, object Result) = File.PreprocessingMethod(File.FileName, File.IsPretty);
            if (!Success)
                return false;

            File.FixingMethod(Result);

            string DestinationFilePath = $"{DestinationDirectory}\\{File.FileName}.json";
            File.SerializingMethod(DestinationFilePath, Result);
        }

        Debug.WriteLine("Done");
        return true;
    }

    private (bool, object) PreprocessSingle<T>(string fileName, bool isPretty)
        where T: class
    {
        if (Preprocess(fileName, isPretty, out T SingleObject))
        {
            Debug.WriteLine(" OK");
            return (true, SingleObject);
        }

        return (false, null!);
    }

    private (bool, object) PreprocessDictionary<T>(string fileName, bool isPretty)
        where T : ICollection
    {
        if (Preprocess(fileName, isPretty, out T ObjectCollection))
        { 
            Debug.WriteLine($" OK ({ObjectCollection.Count})");
            return (true, ObjectCollection);
        }

        return (false, null!);
    }

    private bool Preprocess<T>(string fileName, bool isPretty, out T result)
    {
        Debug.Write($"Preprocessing {fileName}.json...");

        string SourceDirectory = @"C:\Users\DLB\AppData\Roaming\PgJsonParse\Versions\387";
        string SourceFilePath = $"{SourceDirectory}\\{fileName}.json";

        string ReadContent = GetReadContent(SourceFilePath, isPretty);
        result = GetDeserializedObjects<T>(ReadContent);
        EnsureAlphabeticalOrder(typeof(T), new List<Type>());
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
            return true;
    }

    private string GetReadContent(string filePath, bool isPretty)
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

    private T GetDeserializedObjects<T>(string readContent)
    {
        JsonSerializerOptions ReadOptions = new();
        JsonConverters.ForEach(ReadOptions.Converters.Add);
        T Result = JsonSerializer.Deserialize<T>(readContent, ReadOptions) ?? throw new InvalidCastException();

        return Result;
    }

    private void EnsureAlphabeticalOrder(Type type, List<Type> visitedTypes)
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

                if (!visitedTypes.Contains(PropertyType))
                {
                    visitedTypes.Add(PropertyType);
                    EnsureAlphabeticalOrder(PropertyType, visitedTypes);
                }
            }
        }

        List<string> SortedPropertyNames = new(PropertyNames);
        SortedPropertyNames.Sort(StringComparer.Ordinal);

        for (int i = 0; i < SortedPropertyNames.Count; i++)
            Debug.Assert(SortedPropertyNames[i] == PropertyNames[i], $"Property {SortedPropertyNames[i]} in {type.Name} is not declared in order");
    }

    private string GetWriteContent<T>(T objects, bool isPretty)
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

    private void SaveSerializedContent<T>(string filePath, object content)
    {
        SaveSerializedContent<T>(filePath, (T)content);
    }

    private void SaveSerializedContent<T>(string filePath, T content)
    {
        JsonSerializerOptions WriteOptions = new();
        WriteOptions.WriteIndented = true;
        WriteOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        WriteOptions.NumberHandling = JsonNumberHandling.Strict;
        WriteOptions.Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
        string CuratedContent = JsonSerializer.Serialize(content, WriteOptions);

        string Indent = string.Empty;
        while (CuratedContent.Contains($"\r\n{Indent}  "))
        {
            CuratedContent = CuratedContent.Replace($"\r\n{Indent}  ", $"\r\n{Indent}\t");
            Indent += "\t";
        }

        CuratedContent = CuratedContent.Replace("\r\n", "\n");

        using FileStream Stream = new(filePath, FileMode.Create, FileAccess.Write);
        using StreamWriter Writer = new(Stream, Encoding.UTF8);
        Writer.Write(CuratedContent);
    }

    public static object? FromSingleOrMultiple<T, TRaw>(T[]? items, Func<T, TRaw> converter, JsonArrayFormat format)
    {
        if (items is null)
            return null;
        else
            switch (format)
            {
                default:
                case JsonArrayFormat.Null:
                case JsonArrayFormat.Normal:
                    return FromMultiple(items, converter);
                case JsonArrayFormat.SingleElement:
                    return converter(items[0]);
                case JsonArrayFormat.NestedArray:
                    return FromNestedArray(items, converter);
                case JsonArrayFormat.MixedArray:
                    return FromMixedArray(items, converter);
            }
    }

    public static object? FromMultiple<T, TRaw>(T[] items, Func<T, TRaw> converter)
    {
        if (items.Length == 0)
            return null;

        TRaw[] Result = new TRaw[items.Length];

        for (int i = 0; i < Result.Length; i++)
            Result[i] = converter(items[i]);

        return Result;
    }

    public static object? FromNestedArray<T, TRaw>(T[] items, Func<T, TRaw> converter)
    {
        TRaw[][] Result = new TRaw[1][];
        TRaw[] NestedResult = new TRaw[items.Length];

        for (int i = 0; i < NestedResult.Length; i++)
            NestedResult[i] = converter(items[i]);

        Result[0] = NestedResult;

        return Result;
    }

    public static object? FromMixedArray<T, TRaw>(T[] items, Func<T, TRaw> converter)
    {
        TRaw[] SecondArray = new TRaw[items.Length - 1];
        for (int i = 0; i + 1 < items.Length; i++)
            SecondArray[i] = converter(items[i + 1]);

        return new object[2] { converter(items[0])!, SecondArray };
    }

    public static T[]? ToSingleOrMultiple<T, TRaw>(object? element, Func<TRaw, T> converter, out JsonArrayFormat format)
    {
        if (element is null)
        {
            format = JsonArrayFormat.Null;
            return null;
        }
        else
        {
            TRaw[] RawResult;

            if (ToSingleItem(element, out RawResult))
                format = JsonArrayFormat.SingleElement;
            else if (ToMixedArray(element, out RawResult))
                format = JsonArrayFormat.MixedArray;
            else if (ToNestedArray(element, out RawResult))
                format = JsonArrayFormat.NestedArray;
            else if (ToMultipleItems(element, out RawResult))
                format = JsonArrayFormat.Normal;
            else
                throw new InvalidCastException();

            T[] Result = new T[RawResult.Length];
            for (int i = 0; i < RawResult.Length; i++)
                Result[i] = converter(RawResult[i]);

            return Result;
        }
    }

    public static bool ToSingleItem<TRaw>(object element, out TRaw[] result)
    {
        if (element is JsonElement AsSingle)
        {
            if (AsSingle.ValueKind == JsonValueKind.Object)
            {
                result = new TRaw[1];
                result[0] = AsSingle.Deserialize<TRaw>() ?? throw new InvalidCastException();
                return true;
            }
            else if (AsSingle.ValueKind == JsonValueKind.String && typeof(TRaw) == typeof(string) && AsSingle.GetString() is TRaw AsString)
            {
                result = new TRaw[1];
                result[0] = AsString;
                return true;
            }
        }

        result = null!;
        return false;
    }

    public static bool ToMultipleItems<TRaw>(object element, out TRaw[] result)
    {
        if (element is JsonElement AsMultiple && AsMultiple.ValueKind == JsonValueKind.Array)
        {
            int ArrayLength = AsMultiple.GetArrayLength();
            result = new TRaw[ArrayLength];

            for (int i = 0; i < ArrayLength; i++)
                result[i] = AsMultiple[i].Deserialize<TRaw>() ?? throw new InvalidCastException();

            return true;
        }

        result = null!;
        return false;
    }

    public static bool ToNestedArray<TRaw>(object element, out TRaw[] result)
    {
        if (element is JsonElement AsMultiple && AsMultiple.ValueKind == JsonValueKind.Array)
        {
            int ArrayLength = AsMultiple.GetArrayLength();
            if (ArrayLength == 1)
            {
                var Enumerator = AsMultiple.EnumerateArray();
                Enumerator.MoveNext();
                JsonElement FirstElement = Enumerator.Current;

                if (FirstElement.ValueKind == JsonValueKind.Array)
                {
                    ArrayLength = FirstElement.GetArrayLength();
                    result = new TRaw[ArrayLength];

                    for (int i = 0; i < ArrayLength; i++)
                        result[i] = FirstElement[i].Deserialize<TRaw>() ?? throw new InvalidCastException();

                    return true;
                }
            }
        }

        result = null!;
        return false;
    }

    public static bool ToMixedArray<TRaw>(object element, out TRaw[] result)
    {
        if (element is JsonElement AsMultiple && AsMultiple.ValueKind == JsonValueKind.Array)
        {
            int ArrayLength = AsMultiple.GetArrayLength();
            if (ArrayLength == 2)
            {
                var Enumerator = AsMultiple.EnumerateArray();
                Enumerator.MoveNext();
                JsonElement FirstElement = Enumerator.Current;
                Enumerator.MoveNext();
                JsonElement SecondElement = Enumerator.Current;

                if (FirstElement.ValueKind == JsonValueKind.Object && SecondElement.ValueKind == JsonValueKind.Array)
                {
                    ArrayLength = SecondElement.GetArrayLength();
                    result = new TRaw[ArrayLength + 1];

                    result[0] = FirstElement.Deserialize<TRaw>() ?? throw new InvalidCastException();
                    for (int i = 0; i < ArrayLength; i++)
                        result[i + 1] = SecondElement[i].Deserialize<TRaw>() ?? throw new InvalidCastException();

                    return true;
                }
            }
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

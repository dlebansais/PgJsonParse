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

public class Preprocessor
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
        new IntDictionaryJsonConverter<Effect, RawEffect, EffectDictionary>("effect"),
        new IntDictionaryJsonConverter<Item, RawItem, ItemDictionary>("item"),
        new SkillRequirementDictionaryJsonConverter(),
        new IntDictionaryJsonConverter<ItemUse, ItemUse, ItemUseDictionary>("item"),
        new StringDictionaryJsonConverter<LoreBookCategory, RawLoreBookCategory, LoreBookCategoryDictionary>(),
        new IntDictionaryJsonConverter<LoreBook, LoreBook, LoreBookDictionary>("Book"),
        new StringDictionaryJsonConverter<Npc, RawNpc, NpcDictionary>(),
        new IntDictionaryJsonConverter<PlayerTitle, RawPlayerTitle, PlayerTitleDictionary>("Title"),
        new PowerTierCollectionJsonConverter(),
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
        new IntDictionaryJsonConverter<Power, RawPower, PowerDictionary>("power"),
        new IntDictionaryJsonConverter<XpTable, RawXpTable, XpTableDictionary>("Table"),
    };

    public bool Preprocess(string versionPath, List<JsonFile> jsonFileList)
    {
        string DestinationDirectory = @$"{versionPath}\Curated";
        bool PreprocessingDone = false;

        foreach (JsonFile JsonFile in jsonFileList)
        {
            string DestinationFilePath = $"{DestinationDirectory}\\{JsonFile.FileName}.json";

            if (!File.Exists(DestinationFilePath))
            {
                (bool Success, object Result) = JsonFile.PreprocessingMethod(versionPath, JsonFile.FileName, JsonFile.IsPretty);
                if (!Success)
                    return false;

                JsonFile.FixingMethod(Result);

                if (!Directory.Exists(DestinationDirectory))
                    Directory.CreateDirectory(DestinationDirectory);

                JsonFile.SerializingMethod(DestinationFilePath, Result);
                PreprocessingDone = true;
            }
        }

        if (PreprocessingDone)
            Debug.WriteLine("Done");

        return true;
    }

    public static (bool, object) PreprocessSingle<T>(string versionPath, string fileName, bool isPretty)
        where T: class
    {
        if (Preprocess(versionPath, fileName, isPretty, out T SingleObject))
        {
            Debug.WriteLine(" OK");
            return (true, SingleObject);
        }

        return (false, null!);
    }

    public static (bool, object) PreprocessDictionary<T>(string versionPath, string fileName, bool isPretty)
        where T : ICollection
    {
        if (Preprocess(versionPath, fileName, isPretty, out T ObjectCollection))
        { 
            Debug.WriteLine($" OK ({ObjectCollection.Count})");
            return (true, ObjectCollection);
        }

        return (false, null!);
    }

    private static bool Preprocess<T>(string versionPath, string fileName, bool isPretty, out T result)
    {
        Debug.Write($"Preprocessing {fileName}.json...");

        string SourceFilePath = $"{versionPath}\\{fileName}.json";

        string ReadContent = GetReadContent(SourceFilePath, isPretty);
        result = GetDeserializedObjects<T>(ReadContent);
        EnsureAlphabeticalOrder(typeof(T), new List<Type>());
        string WriteContent = GetWriteContent(result, isPretty);

        if (ReadContent != WriteContent)
        {
            Debug.WriteLine("");
            Debug.WriteLine("ERROR: Difference found");

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

    private static string GetReadContent(string filePath, bool isPretty)
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
        T Result = JsonSerializer.Deserialize<T>(readContent, ReadOptions) ?? throw new NullReferenceException();

        return Result;
    }

    private static void EnsureAlphabeticalOrder(Type type, List<Type> visitedTypes)
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

    public static void SaveSerializedContent<T>(string filePath, object content)
    {
        SaveSerializedContent<T>(filePath, (T)content);
    }

    private static void SaveSerializedContent<T>(string filePath, T content)
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
            return format == JsonArrayFormat.EmptyArray ? new TRaw[0] : null;
        else
            switch (format)
            {
                default:
                case JsonArrayFormat.Null:
                case JsonArrayFormat.Normal:
                case JsonArrayFormat.EmptyArray:
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
        format = JsonArrayFormat.Null;

        if (element is null)
            return null;

        TRaw[] RawResult;

        if (ToSingleItem(element, out RawResult))
            format = JsonArrayFormat.SingleElement;
        else if (ToMixedArray(element, out RawResult))
            format = JsonArrayFormat.MixedArray;
        else if (ToNestedArray(element, out RawResult))
            format = JsonArrayFormat.NestedArray;
        else if (ToMultipleItems(element, out RawResult))
            if (RawResult.Length == 0)
            {
                format = JsonArrayFormat.EmptyArray;
                return null;
            }
            else
                format = JsonArrayFormat.Normal;
        else
            throw new PreprocessorException();

        T[] Result = new T[RawResult.Length];
        for (int i = 0; i < RawResult.Length; i++)
            Result[i] = converter(RawResult[i]);

        return Result;
    }

    public static bool ToSingleItem<TRaw>(object element, out TRaw[] result)
    {
        if (element is JsonElement AsSingle)
        {
            if (AsSingle.ValueKind == JsonValueKind.Object)
            {
                result = new TRaw[1];
                result[0] = AsSingle.Deserialize<TRaw>() ?? throw new NullReferenceException();
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
                result[i] = AsMultiple[i].Deserialize<TRaw>() ?? throw new NullReferenceException();

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
                        result[i] = FirstElement[i].Deserialize<TRaw>() ?? throw new NullReferenceException();

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

                    result[0] = FirstElement.Deserialize<TRaw>() ?? throw new NullReferenceException();
                    for (int i = 0; i < ArrayLength; i++)
                        result[i + 1] = SecondElement[i].Deserialize<TRaw>() ?? throw new NullReferenceException();

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
        isNumber = false;
        int? Result = null;

        if (element is not null)
        {
            if (element is JsonElement AsNumber && AsNumber.ValueKind == JsonValueKind.Number)
            {
                isNumber = true;
                Result = AsNumber.GetInt32();
            }
            else if (element is JsonElement AsString && AsString.ValueKind == JsonValueKind.String)
            {
                isNumber = false;
                Result = int.Parse(AsString.GetString());
            }
            else
                throw new PreprocessorException();
        }

        return Result;
    }

    public static (int?, string?) ParseAsNumberOrString(object? element)
    {
        (int?, string?) Result = (null, null);

        if (element is not null)
        {
            if (element is JsonElement AsNumber && AsNumber.ValueKind == JsonValueKind.Number)
                Result = (AsNumber.GetInt32(), null);
            else if (element is JsonElement AsString && AsString.ValueKind == JsonValueKind.String)
                Result = (null, AsString.GetString());
            else
                throw new PreprocessorException();
        }

        return Result;
    }
}

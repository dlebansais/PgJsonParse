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
using FreeSql.Internal;
using FreeSql.Internal.Model;

public partial class Preprocessor
{
    private static List<JsonConverter> JsonConverters = new()
    {
        new IntDictionaryJsonConverter<Ability, RawAbility, AbilityDictionary>("ability"),
        new ArrayJsonConverter<AbilityKeyword, AbilityKeyword, AbilityKeywordArray>(),
        new ArrayJsonConverter<AbilityDynamicDot, RawAbilityDynamicDot, AbilityDynamicDotArray>(),
        new ArrayJsonConverter<AbilityDynamicSpecialValue, RawAbilityDynamicSpecialValue, AbilityDynamicSpecialValueArray>(),
        new AdvancementTableDictionaryJsonConverter(),
        new StringDictionaryJsonConverter<AI, RawAI, AIDictionary>(),
        new StringDictionaryJsonConverter<AIAbility, RawAIAbility, AIAbilityDictionary>(),
        new StringDictionaryJsonConverter<Area, Area, AreaDictionary>(),
        new StringDictionaryJsonConverter<Attribute, RawAttribute, AttributeDictionary>(),
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
        new IntDictionaryJsonConverter<SourceItem, RawSourceItem, SourceItemDictionary>("item"),
        new IntDictionaryJsonConverter<SourceRecipe, RawSourceRecipe, SourceRecipeDictionary>("recipe"),
        new StringDictionaryJsonConverter<StorageVault, RawStorageVault, StorageVaultDictionary>(),
        new IntDictionaryJsonConverter<Power, RawPower, PowerDictionary>("power"),
        new ProfileDictionaryJsonConverter(),
        new IntDictionaryJsonConverter<XpTable, RawXpTable, XpTableDictionary>("Table"),
    };

    public bool Preprocess(string versionPath, List<JsonFile> jsonFileList, IFreeSql fsql, out string destinationDirectory)
    {
        InitializeHandlers();

        destinationDirectory = @$"{versionPath}\Curated";
        bool PreprocessingDone = false;

        foreach (JsonFile JsonFile in jsonFileList)
        {
            string DestinationFilePath = $"{destinationDirectory}\\{JsonFile.FileName}.json";

            if (!File.Exists(DestinationFilePath))
            {
                (bool Success, object Result) = JsonFile.PreprocessingMethod(versionPath, JsonFile.FileName, JsonFile.IsPretty);
                if (!Success)
                    return false;

                JsonFile.FixingMethod(Result);

                if (!Directory.Exists(destinationDirectory))
                    Directory.CreateDirectory(destinationDirectory);

                List<object> InsertedContents = new();
                if (Result is IDictionary InsertedDictionary)
                    InsertedContents.Add(InsertedDictionary.Values);
                else
                    InsertedContents.Add(Result);

                foreach (Action<IFreeSql, List<object>> Action in JsonFile.DatabaseAdditionalInserters)
                    Action(fsql, InsertedContents);

                JsonFile.DatabaseInsertMethod(fsql, Result);
                Debug.Write(" INSERTED");

                object TempResult = JsonFile.DatabaseSelectMethod(fsql, Result);

                List<object> SelectedContents = new();
                if (TempResult is IDictionary SelectedDictionary)
                    SelectedContents.Add(SelectedDictionary.Values);
                else
                    SelectedContents.Add(TempResult);

                foreach (Action<IFreeSql, List<object>, bool> Action in JsonFile.DatabaseAdditionalSelectors)
                    Action(fsql, SelectedContents, false);

                JsonFile.DatabaseFixingMethod(TempResult);
                Debug.Write(" SELECTED");

                JsonFile.SerializingMethod(DestinationFilePath, Result);
                PreprocessingDone = true;

                string TempPath = $"{destinationDirectory}\\~{JsonFile.FileName}.json";
                if (File.Exists(TempPath))
                    File.Delete(TempPath);

                JsonFile.SerializingMethod(TempPath, TempResult);

                string Content1 = File.ReadAllText(DestinationFilePath);
                string Content2 = File.ReadAllText(TempPath);
                bool IsIdentical = Content1 == Content2;

                if (IsIdentical)
                {
                    Debug.WriteLine(" DONE");
                    File.Delete(TempPath);
                }
                else
                {
                    Debug.WriteLine(" WARNING: File content is different");
                    Debug.WriteLine(DestinationFilePath);
                    Debug.WriteLine(TempPath);
                }
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
            Debug.Write(" OK");
            return (true, SingleObject);
        }

        return (false, null!);
    }

    public static (bool, object) PreprocessDictionary<T>(string versionPath, string fileName, bool isPretty)
        where T : ICollection
    {
        if (Preprocess(versionPath, fileName, isPretty, out T ObjectCollection))
        { 
            Debug.Write($" OK ({ObjectCollection.Count})");
            return (true, ObjectCollection);
        }

        return (false, null!);
    }

    public static (bool, object) PreprocessArray<T>(string versionPath, string fileName, bool isPretty)
        where T : ICollection
    {
        if (Preprocess(versionPath, fileName, isPretty, out T ObjectCollection))
        {
            Debug.Write($" OK ({ObjectCollection.Count})");
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

        if (isPretty)
        {
            ReadContent = ReadContent.Replace("\r\n", "\n");

            while (ReadContent.EndsWith("\n"))
                ReadContent = ReadContent.Substring(0, ReadContent.Length - 1);
        }

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

        if (type.BaseType!.IsGenericType)
        {
            Type[] GenericArguments = type.BaseType.GetGenericArguments();
            Properties = GenericArguments[GenericArguments.Length - 1].GetProperties();
        }
        else
            Properties = type.GetProperties();

        List<string> PropertyNames = new();

        foreach (PropertyInfo Property in Properties)
        {
            if (Property.Name == nameof(IHasKey<int>.Key) ||
                Property.Name == nameof(IHasParentKey<int>.ParentKey) ||
                Property.Name == nameof(IHasParentKey<int>.ParentProperty))
                continue;

            PropertyNames.Add(Property.Name);

            if (Property.PropertyType.FullName!.StartsWith("Preprocessor"))
            {
                Type PropertyType = Property.PropertyType;
                if (PropertyType.IsArray)
                    PropertyType = PropertyType.GetElementType()!;

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

    public static void SaveSerializedDictionary<TDictionary, TValue>(string filePath, object content)
        where TDictionary : IDictionary
    {
        SaveSerializedContent(filePath, content);
    }

    public static void SaveSerializedList<TList, TItem>(string filePath, object content)
        where TList : IList
    {
        SaveSerializedContent(filePath, content);
    }

    public static void SaveSerializedSingle<TItem>(string filePath, object content)
        where TItem : class
    {
        SaveSerializedContent(filePath, content);
    }

    public static void SaveSerializedContent(string filePath, object content)
    {
        JsonSerializerOptions WriteOptions = new();
        WriteOptions.WriteIndented = true;
        WriteOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        WriteOptions.NumberHandling = JsonNumberHandling.Strict;
        WriteOptions.Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
        string JsonContent = JsonSerializer.Serialize(content, WriteOptions);

        string Indent = string.Empty;
        while (JsonContent.Contains($"\r\n{Indent}  "))
        {
            JsonContent = JsonContent.Replace($"\r\n{Indent}  ", $"\r\n{Indent}\t");
            Indent += "\t";
        }

        JsonContent = JsonContent.Replace("\r\n", "\n");

        using FileStream Stream = new(filePath, FileMode.Create, FileAccess.Write);
        using StreamWriter Writer = new(Stream, Encoding.UTF8);
        Writer.Write(JsonContent);
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
        where TRaw : class
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
        JsonElement AsSingle = (JsonElement)element;

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
        else
        {
            result = null!;
            return false;
        }
    }

    public static bool ToMultipleItems<TRaw>(object element, out TRaw[] result)
        where TRaw: class
    {
        JsonElement AsMultiple = (JsonElement)element;

        if (AsMultiple.ValueKind == JsonValueKind.Array)
        {
            int ArrayLength = AsMultiple.GetArrayLength();
            List<TRaw> ResultList = new();

            for (int i = 0; i < ArrayLength; i++)
            {
                TRaw? Item;

                try
                {
                    Item = AsMultiple[i].Deserialize<TRaw>();
                }
                catch
                {
                    Item = null;
                }

                if (Item is null)
                    break;

                ResultList.Add(Item);
            }

            if (ResultList.Count == ArrayLength)
            {
                result = ResultList.ToArray();
                return true;
            }
        }

        result = null!;
        return false;
    }

    public static bool ToNestedArray<TRaw>(object element, out TRaw[] result)
    {
        JsonElement AsMultiple = (JsonElement)element;

        if (AsMultiple.ValueKind == JsonValueKind.Array)
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
        JsonElement AsMultiple = (JsonElement)element;

        if (AsMultiple.ValueKind == JsonValueKind.Array)
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
                Result = int.Parse(AsString.GetString()!);
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

    private void InitializeHandlers()
    {
        Utils.TypeHandlers.TryAdd(typeof(string[]), new StringArrayHandler());
        Utils.TypeHandlers.TryAdd(typeof(int[]), new IntArrayHandler());
        Utils.TypeHandlers.TryAdd(typeof(decimal[]), new DecimalArrayHandler());
        Utils.TypeHandlers.TryAdd(typeof(Dictionary<string, int>), new StringIntDictionaryHandler());
    }

    private class StringArrayHandler : TypeHandler<string[]>
    {
        public override string[] Deserialize(object value)
            => JsonSerializer.Deserialize<string[]>((string)value)!;

        public override object Serialize(string[] value)
            => JsonSerializer.Serialize(value);
    }

    private class IntArrayHandler : TypeHandler<int[]>
    {
        public override int[] Deserialize(object value)
            => JsonSerializer.Deserialize<int[]>((string)value)!;

        public override object Serialize(int[] value)
            => JsonSerializer.Serialize(value);
    }

    private class DecimalArrayHandler : TypeHandler<decimal[]>
    {
        public override decimal[] Deserialize(object value)
            => JsonSerializer.Deserialize<decimal[]>((string)value)!;

        public override object Serialize(decimal[] value)
            => JsonSerializer.Serialize(value);
    }

    private class StringIntDictionaryHandler : TypeHandler<Dictionary<string, int>>
    {
        public override Dictionary<string, int> Deserialize(object value)
            => JsonSerializer.Deserialize<Dictionary<string, int>>((string)value)!;

        public override object Serialize(Dictionary<string, int> value)
            => JsonSerializer.Serialize(value);
    }
}

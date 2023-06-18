namespace Preprocessor;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;

internal class Preprocessor
{
    static void Main(string[] args)
    {
        PreprocessDictionary<RawAbilityDictionary>("abilities");
        PreprocessDictionary<RawAdvancementTableDictionary>("advancementtables");
        PreprocessDictionary<RawAIDictionary>("ai");
        PreprocessDictionary<RawAreaDictionary>("areas");
        PreprocessDictionary<RawAttributeDictionary>("attributes");
        PreprocessDictionary<RawDirectedGoalDictionary>("directedgoals");
        PreprocessDictionary<RawEffectDictionary>("effects");
        PreprocessDictionary<RawItemDictionary>("items");
        PreprocessDictionary<RawItemUseDictionary>("itemuses");
        PreprocessSingle<RawLoreBookInfo>("lorebookinfo");
        PreprocessDictionary<RawLoreBookDictionary>("lorebooks");
        PreprocessDictionary<RawNpcDictionary>("npcs", isPretty: false);
        PreprocessDictionary<RawPlayerTitleDictionary>("playertitles");
        PreprocessDictionary<RawQuestDictionary>("quests");

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

        using FileStream Stream = new(@$"C:\Users\DLB\AppData\Roaming\PgJsonParse\Versions\387\{fileName}.json", FileMode.Open, FileAccess.Read);
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

        List<JsonConverter> Converters = new()
        {
            new RawIntDictionaryJsonConverter<RawAbility, RawAbility1, RawAbilityDictionary>("ability"),
            new RawAdvancementTableDictionaryJsonConverter(),
            new RawAIDictionaryJsonConverter(),
            new RawAIAbilityDictionaryJsonConverter(),
            new RawAreaDictionaryJsonConverter(),
            new RawAttributeDictionaryJsonConverter(),
            new RawDirectedGoalDictionaryJsonConverter(),
            new RawEffectDictionaryJsonConverter(),
            new RawIntDictionaryJsonConverter<RawItem, RawItem, RawItemDictionary>("item"),
            new RawSkillRequirementDictionaryJsonConverter(),
            new RawIntDictionaryJsonConverter<RawItemUse, RawItemUse, RawItemUseDictionary>("item"),
            new RawLoreBookCategoryDictionaryJsonConverter(),
            new RawIntDictionaryJsonConverter<RawLoreBook, RawLoreBook, RawLoreBookDictionary>("Book"),
            new RawNpcDictionaryJsonConverter(),
            new RawIntDictionaryJsonConverter<RawPlayerTitle, RawPlayerTitle, RawPlayerTitleDictionary>("Title"),
            new RawIntDictionaryJsonConverter<RawQuest, RawQuest, RawQuestDictionary>("quest"),
        };

        JsonSerializerOptions ReadOptions = new();
        Converters.ForEach(ReadOptions.Converters.Add);
        result = JsonSerializer.Deserialize<T>(ReadContent, ReadOptions) ?? throw new InvalidCastException();

        JsonSerializerOptions WriteOptions = new();
        Converters.ForEach(WriteOptions.Converters.Add);
        WriteOptions.WriteIndented = isPretty;
        WriteOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        WriteOptions.NumberHandling = JsonNumberHandling.Strict;
        WriteOptions.Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
        string WriteContent = JsonSerializer.Serialize(result, WriteOptions);

        if (isPretty)
            WriteContent = WriteContent.Replace("\r\n", "\n");

        if (ReadContent != WriteContent)
        {
            Comparer Comparer = new();
            Comparer.Compare(ReadContent, WriteContent);
            return false;
        }
        else
            return true;
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
}

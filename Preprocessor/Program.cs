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

internal class Program
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
            new RawAbilityDictionaryJsonConverter(),
            new RawAdvancementTableDictionaryJsonConverter(),
            new RawAIDictionaryJsonConverter(),
            new RawAIAbilityDictionaryJsonConverter(),
            new RawAreaDictionaryJsonConverter(),
            new RawAttributeDictionaryJsonConverter(),
            new RawDirectedGoalDictionaryJsonConverter(),
            new RawEffectDictionaryJsonConverter(),
            new RawItemDictionaryJsonConverter(),
            new RawSkillRequirementDictionaryJsonConverter(),
            new RawItemUseDictionaryJsonConverter(),
            new RawLoreBookCategoryDictionaryJsonConverter(),
            new RawLoreBookDictionaryJsonConverter(),
            new RawNpcDictionaryJsonConverter(),
            new RawPlayerTitleDictionaryJsonConverter(),
            new RawQuestDictionaryJsonConverter(),
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
}

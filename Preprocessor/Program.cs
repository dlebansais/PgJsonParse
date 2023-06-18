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
        /*Preprocess<RawAbilityDictionary>("abilities");
        Preprocess<RawAdvancementTableDictionary>("advancementtables");
        Preprocess<RawAIDictionary>("ai");
        Preprocess<RawAreaDictionary>("areas");
        Preprocess<RawAttributeDictionary>("attributes");
        Preprocess<RawDirectedGoalDictionary>("directedgoals");
        Preprocess<RawEffectDictionary>("effects");*/
        Preprocess<RawItemDictionary>("items");

        Debug.WriteLine("Done");
    }
    static void Preprocess<T>(string fileName)
        where T: ICollection
    {
        Debug.Write($"Processing {fileName}.json...");

        using FileStream Stream = new(@$"C:\Users\DLB\AppData\Roaming\PgJsonParse\Versions\387\{fileName}.json", FileMode.Open, FileAccess.Read);
        using StreamReader Reader = new(Stream, Encoding.UTF8);
        string ReadContent = Reader.ReadToEnd();

        string Indent = string.Empty;
        while (ReadContent.Contains("\t"))
        {
            ReadContent = ReadContent.Replace($"\n{Indent}\t", $"\n{Indent}  ");
            Indent += "  ";
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
        };

        JsonSerializerOptions ReadOptions = new();
        Converters.ForEach(ReadOptions.Converters.Add);
        T ObjectCollection = JsonSerializer.Deserialize<T>(ReadContent, ReadOptions) ?? throw new InvalidCastException();

        JsonSerializerOptions WriteOptions = new();
        Converters.ForEach(WriteOptions.Converters.Add);
        WriteOptions.WriteIndented = true;
        WriteOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        WriteOptions.NumberHandling = JsonNumberHandling.Strict;
        WriteOptions.Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
        string WriteContent = JsonSerializer.Serialize(ObjectCollection, WriteOptions);

        WriteContent = WriteContent.Replace("\r\n", "\n");

        if (ReadContent != WriteContent)
        {
            Comparer Comparer = new();
            Comparer.Compare(ReadContent, WriteContent);
        }
        else
            Debug.WriteLine($" OK ({ObjectCollection.Count})");
    }
}

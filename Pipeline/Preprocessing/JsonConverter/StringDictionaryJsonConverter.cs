namespace Preprocessor;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;

public class StringDictionaryJsonConverter<TElement, TRawElement, TDictionary> : JsonConverter<TDictionary>
    where TElement : class
    where TRawElement : class
    where TDictionary : Dictionary<string, TElement>, IDictionaryValueBuilderString<TElement, TRawElement>, new()
{
    public override TDictionary? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        TDictionary Result = new();

        ReadDictionary(Result, ref reader, options);

        return Result;
    }

    private void ReadDictionary(TDictionary dictionary, ref Utf8JsonReader reader, JsonSerializerOptions options)
    {
        while (reader.Read() && reader.TokenType == JsonTokenType.PropertyName)
        {
            string Key = reader.GetString() ?? throw new NullReferenceException();
            reader.Read();

            try
            {
                TRawElement RawElement = JsonSerializer.Deserialize<TRawElement>(ref reader, options) ?? throw new NullReferenceException();
                dictionary.Add(Key, dictionary.FromRaw(Key, RawElement));
            }
            catch (Exception Exception)
            {
                throw new PreprocessorException(Key, Exception);
            }
        }
    }

    public override void Write(Utf8JsonWriter writer, TDictionary value, JsonSerializerOptions options)
    {
        WriteDictionary(writer, value, options);
    }

    private void WriteDictionary(Utf8JsonWriter writer, TDictionary value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();

        foreach (KeyValuePair<string, TElement> Entry in value)
        {
            string Key = Entry.Key;
            TElement Element = Entry.Value;

            writer.WritePropertyName(Key);

            JsonSerializer.Serialize(writer, value.ToRaw(Element), options);
        }

        writer.WriteEndObject();
    }
}

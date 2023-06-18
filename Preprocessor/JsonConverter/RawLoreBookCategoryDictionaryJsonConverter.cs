namespace Preprocessor;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;

internal class RawLoreBookCategoryDictionaryJsonConverter : JsonConverter<RawLoreBookCategoryDictionary>
{
    public override RawLoreBookCategoryDictionary? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        RawLoreBookCategoryDictionary Result = new();

        ReadDictionary(Result, ref reader, options);

        return Result;
    }

    private void ReadDictionary(RawLoreBookCategoryDictionary dictionary, ref Utf8JsonReader reader, JsonSerializerOptions options)
    {
        while (reader.Read() && reader.TokenType == JsonTokenType.PropertyName)
        {
            string Key = reader.GetString() ?? throw new InvalidCastException();
            reader.Read();

            RawLoreBookCategory? LoreBookCategory = null;
            Exception? Exception1 = null;

            try
            {
                LoreBookCategory = JsonSerializer.Deserialize<RawLoreBookCategory>(ref reader, options) ?? throw new InvalidCastException();
            }
            catch (Exception Exception)
            {
                Exception1 = Exception;
            }

            if (LoreBookCategory is not null)
                dictionary.Add(Key, LoreBookCategory);
            else
            {
                Debug.WriteLine($"\r\nKey: {Key}");
                Debug.WriteLine(Exception1?.Message);
                throw new InvalidCastException();
            }
        }
    }

    public override void Write(Utf8JsonWriter writer, RawLoreBookCategoryDictionary value, JsonSerializerOptions options)
    {
        WriteDictionary(writer, value, options);
    }

    private void WriteDictionary(Utf8JsonWriter writer, RawLoreBookCategoryDictionary value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();

        foreach (KeyValuePair<string, RawLoreBookCategory> Entry in value)
        {
            string Key = Entry.Key;
            RawLoreBookCategory LoreBookCategory = Entry.Value;

            writer.WritePropertyName(Key);

            JsonSerializer.Serialize(writer, LoreBookCategory, options);
        }

        writer.WriteEndObject();
    }
}

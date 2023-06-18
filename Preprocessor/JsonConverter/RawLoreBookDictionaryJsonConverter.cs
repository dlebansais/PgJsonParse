namespace Preprocessor;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;

internal class RawLoreBookDictionaryJsonConverter : JsonConverter<RawLoreBookDictionary>
{
    public override RawLoreBookDictionary? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        RawLoreBookDictionary Result = new();

        ReadDictionary(Result, ref reader, options);

        return Result;
    }

    private void ReadDictionary(RawLoreBookDictionary dictionary, ref Utf8JsonReader reader, JsonSerializerOptions options)
    {
        while (reader.Read() && reader.TokenType == JsonTokenType.PropertyName)
        {
            string Key = reader.GetString() ?? throw new InvalidCastException();
            reader.Read();

            RawLoreBook? LoreBook = null;
            Exception? Exception1 = null;

            try
            {
                LoreBook = JsonSerializer.Deserialize<RawLoreBook>(ref reader, options) ?? throw new InvalidCastException();
            }
            catch (Exception Exception)
            {
                Exception1 = Exception;
            }

            if (Key.StartsWith("Book_") && int.TryParse(Key.Substring(5), out int LoreBookKey))
            {
                if (LoreBook is not null)
                    dictionary.Add(LoreBookKey, LoreBook);
                else
                {
                    Debug.WriteLine($"\r\nKey: {Key}");
                    Debug.WriteLine(Exception1?.Message);
                    throw new InvalidCastException();
                }
            }
            else
            {
                Debug.WriteLine($"Invalid ability key: {Key}");
                throw new InvalidCastException();
            }
        }
    }

    public override void Write(Utf8JsonWriter writer, RawLoreBookDictionary value, JsonSerializerOptions options)
    {
        WriteDictionary(writer, value, options);
    }

    private void WriteDictionary(Utf8JsonWriter writer, RawLoreBookDictionary value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();

        foreach (KeyValuePair<int, RawLoreBook> Entry in value)
        {
            string Key = $"Book_{Entry.Key}";
            RawLoreBook LoreBook = Entry.Value;

            writer.WritePropertyName(Key);

            JsonSerializer.Serialize(writer, LoreBook, options);
        }

        writer.WriteEndObject();
    }
}

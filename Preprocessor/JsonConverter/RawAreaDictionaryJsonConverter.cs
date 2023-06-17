namespace Preprocessor;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;

internal class RawAreaDictionaryJsonConverter : JsonConverter<RawAreaDictionary>
{
    public override RawAreaDictionary? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        RawAreaDictionary Result = new();

        ReadDictionary(Result, ref reader, options);

        return Result;
    }

    private void ReadDictionary(RawAreaDictionary dictionary, ref Utf8JsonReader reader, JsonSerializerOptions options)
    {
        while (reader.Read() && reader.TokenType == JsonTokenType.PropertyName)
        {
            string Key = reader.GetString() ?? throw new InvalidCastException();
            reader.Read();

            RawArea? Area = null;
            Exception? Exception1 = null;

            try
            {
                Area = JsonSerializer.Deserialize<RawArea>(ref reader, options) ?? throw new InvalidCastException();
            }
            catch (Exception Exception)
            {
                Exception1 = Exception;
            }

            if (Area is not null)
                dictionary.Add(Key, Area);
            else
            {
                Debug.WriteLine($"\r\nKey: {Key}");
                Debug.WriteLine(Exception1?.Message);
                throw new InvalidCastException();
            }
        }
    }

    public override void Write(Utf8JsonWriter writer, RawAreaDictionary value, JsonSerializerOptions options)
    {
        WriteDictionary(writer, value, options);
    }

    private void WriteDictionary(Utf8JsonWriter writer, RawAreaDictionary value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();

        foreach (KeyValuePair<string, RawArea> Entry in value)
        {
            string Key = Entry.Key;
            RawArea Area = Entry.Value;

            writer.WritePropertyName(Key);

            JsonSerializer.Serialize(writer, Area, options);
        }

        writer.WriteEndObject();
    }
}

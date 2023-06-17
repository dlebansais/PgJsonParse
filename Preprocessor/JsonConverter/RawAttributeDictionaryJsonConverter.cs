namespace Preprocessor;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;

internal class RawAttributeDictionaryJsonConverter : JsonConverter<RawAttributeDictionary>
{
    public override RawAttributeDictionary? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        RawAttributeDictionary Result = new();

        ReadDictionary(Result, ref reader, options);

        return Result;
    }

    private void ReadDictionary(RawAttributeDictionary dictionary, ref Utf8JsonReader reader, JsonSerializerOptions options)
    {
        while (reader.Read() && reader.TokenType == JsonTokenType.PropertyName)
        {
            string Key = reader.GetString() ?? throw new InvalidCastException();
            reader.Read();

            RawAttribute? Attribute = null;
            Exception? Exception1 = null;

            try
            {
                Attribute = JsonSerializer.Deserialize<RawAttribute>(ref reader, options) ?? throw new InvalidCastException();
            }
            catch (Exception Exception)
            {
                Exception1 = Exception;
            }

            if (Attribute is not null)
                dictionary.Add(Key, Attribute);
            else
            {
                Debug.WriteLine($"\r\nKey: {Key}");
                Debug.WriteLine(Exception1?.Message);
                throw new InvalidCastException();
            }
        }
    }

    public override void Write(Utf8JsonWriter writer, RawAttributeDictionary value, JsonSerializerOptions options)
    {
        WriteDictionary(writer, value, options);
    }

    private void WriteDictionary(Utf8JsonWriter writer, RawAttributeDictionary value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();

        foreach (KeyValuePair<string, RawAttribute> Entry in value)
        {
            string Key = Entry.Key;
            RawAttribute Attribute = Entry.Value;

            writer.WritePropertyName(Key);

            JsonSerializer.Serialize(writer, Attribute, options);
        }

        writer.WriteEndObject();
    }
}

namespace Preprocessor;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;

internal class RawAIDictionaryJsonConverter : JsonConverter<RawAIDictionary>
{
    public override RawAIDictionary? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        RawAIDictionary Result = new();

        ReadDictionary(Result, ref reader, options);

        return Result;
    }

    private void ReadDictionary(RawAIDictionary dictionary, ref Utf8JsonReader reader, JsonSerializerOptions options)
    {
        while (reader.Read() && reader.TokenType == JsonTokenType.PropertyName)
        {
            string Key = reader.GetString() ?? throw new InvalidCastException();
            reader.Read();

            RawAI? AI = null;
            Exception? Exception1 = null;

            try
            {
                AI = JsonSerializer.Deserialize<RawAI>(ref reader, options) ?? throw new InvalidCastException();
            }
            catch (Exception Exception)
            {
                Exception1 = Exception;
            }

            if (AI is not null)
                dictionary.Add(Key, AI);
            else
            {
                Debug.WriteLine($"\r\nKey: {Key}");
                Debug.WriteLine(Exception1?.Message);
                throw new InvalidCastException();
            }
        }
    }

    public override void Write(Utf8JsonWriter writer, RawAIDictionary value, JsonSerializerOptions options)
    {
        WriteDictionary(writer, value, options);
    }

    private void WriteDictionary(Utf8JsonWriter writer, RawAIDictionary value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();

        foreach (KeyValuePair<string, RawAI> Entry in value)
        {
            string Key = Entry.Key;
            RawAI AI = Entry.Value;

            writer.WritePropertyName(Key);

            JsonSerializer.Serialize(writer, AI, options);
        }

        writer.WriteEndObject();
    }
}

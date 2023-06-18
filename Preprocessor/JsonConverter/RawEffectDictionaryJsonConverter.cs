namespace Preprocessor;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;

internal class RawEffectDictionaryJsonConverter : JsonConverter<RawEffectDictionary>
{
    public override RawEffectDictionary? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        RawEffectDictionary Result = new();

        ReadDictionary(Result, ref reader, options);

        return Result;
    }

    private void ReadDictionary(RawEffectDictionary dictionary, ref Utf8JsonReader reader, JsonSerializerOptions options)
    {
        while (reader.Read() && reader.TokenType == JsonTokenType.PropertyName)
        {
            string Key = reader.GetString() ?? throw new InvalidCastException();
            reader.Read();

            RawEffect1? Effect1 = null;
            Exception? Exception1 = null;

            try
            {
                Effect1 = JsonSerializer.Deserialize<RawEffect1>(ref reader, options) ?? throw new InvalidCastException();
            }
            catch (Exception Exception)
            {
                Exception1 = Exception;
            }

            if (Effect1 is not null)
                dictionary.Add(Key, new RawEffect(Effect1));
            else
            {
                Debug.WriteLine($"\r\nKey: {Key}");
                Debug.WriteLine(Exception1?.Message);
                throw new InvalidCastException();
            }
        }
    }

    public override void Write(Utf8JsonWriter writer, RawEffectDictionary value, JsonSerializerOptions options)
    {
        WriteDictionary(writer, value, options);
    }

    private void WriteDictionary(Utf8JsonWriter writer, RawEffectDictionary value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();

        foreach (KeyValuePair<string, RawEffect> Entry in value)
        {
            string Key = Entry.Key;
            RawEffect Effect = Entry.Value;

            writer.WritePropertyName(Key);

            JsonSerializer.Serialize(writer, Effect.ToRawEffect1(), options);
        }

        writer.WriteEndObject();
    }
}

namespace Preprocessor;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;

internal class RawAIAbilityDictionaryJsonConverter : JsonConverter<RawAIAbilityDictionary>
{
    public override RawAIAbilityDictionary? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        RawAIAbilityDictionary Result = new();

        ReadDictionary(Result, ref reader, options);

        return Result;
    }

    private void ReadDictionary(RawAIAbilityDictionary dictionary, ref Utf8JsonReader reader, JsonSerializerOptions options)
    {
        while (reader.Read() && reader.TokenType == JsonTokenType.PropertyName)
        {
            string Key = reader.GetString() ?? throw new InvalidCastException();
            reader.Read();

            RawAIAbility1? RawAIAbility1 = null;
            Exception? Exception1 = null;

            try
            {
                RawAIAbility1 = JsonSerializer.Deserialize<RawAIAbility1>(ref reader, options) ?? throw new InvalidCastException();
            }
            catch (Exception Exception)
            {
                Exception1 = Exception;
            }

            if (RawAIAbility1 is not null)
                dictionary.Add(Key, new RawAIAbility(RawAIAbility1));
            else
            {
                Debug.WriteLine($"\r\nKey: {Key}");
                Debug.WriteLine(Exception1?.Message);
                throw new InvalidCastException();
            }
        }
    }

    public override void Write(Utf8JsonWriter writer, RawAIAbilityDictionary value, JsonSerializerOptions options)
    {
        WriteDictionary(writer, value, options);
    }

    private void WriteDictionary(Utf8JsonWriter writer, RawAIAbilityDictionary value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();

        foreach (KeyValuePair<string, RawAIAbility> Entry in value)
        {
            string Key = Entry.Key;
            RawAIAbility AIAbility = Entry.Value;

            writer.WritePropertyName(Key);

            JsonSerializer.Serialize(writer, AIAbility.ToRawAIAbility1(), options);
        }

        writer.WriteEndObject();
    }
}

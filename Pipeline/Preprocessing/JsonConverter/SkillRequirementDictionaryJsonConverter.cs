namespace Preprocessor;

using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

public class SkillRequirementDictionaryJsonConverter : JsonConverter<SkillRequirementDictionary>
{
    public override SkillRequirementDictionary? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        SkillRequirementDictionary Result = new();

        ReadDictionary(Result, ref reader, options);

        return Result;
    }

    private void ReadDictionary(SkillRequirementDictionary dictionary, ref Utf8JsonReader reader, JsonSerializerOptions options)
    {
        while (reader.Read() && reader.TokenType == JsonTokenType.PropertyName)
        {
            string Key = reader.GetString() ?? throw new InvalidCastException();

            reader.Read();
            int Value = reader.GetInt32();

            dictionary.Add(Key, Value);
        }
    }

    public override void Write(Utf8JsonWriter writer, SkillRequirementDictionary value, JsonSerializerOptions options)
    {
        WriteDictionary(writer, value, options);
    }

    private void WriteDictionary(Utf8JsonWriter writer, SkillRequirementDictionary value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();

        foreach (KeyValuePair<string, int> Entry in value)
        {
            string Key = Entry.Key;
            int Value = Entry.Value;

            writer.WritePropertyName(Key);
            writer.WriteNumberValue(Value);
        }

        writer.WriteEndObject();
    }
}

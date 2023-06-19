namespace Preprocessor;

using System;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;

internal class SkillAdvancementHintCollectionJsonConverter : JsonConverter<SkillAdvancementHintCollection>
{
    public override SkillAdvancementHintCollection? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        SkillAdvancementHintCollection Result = new();

        ReadTableDictionary(Result, ref reader, options);

        return Result;
    }

    private void ReadTableDictionary(SkillAdvancementHintCollection dictionary, ref Utf8JsonReader reader, JsonSerializerOptions options)
    {
        while (reader.Read() && reader.TokenType == JsonTokenType.PropertyName)
        {
            string Key = reader.GetString() ?? throw new InvalidCastException();
            reader.Read();
            string Hint = reader.GetString() ?? throw new InvalidCastException();

            if (int.TryParse(Key, out int Level))
            {
                SkillAdvancementHint AdvancementHint = new();
                AdvancementHint.Level = Level;
                AdvancementHint.Hint = Hint;
                dictionary.Add(AdvancementHint);
            }
            else
            {
                Debug.WriteLine($"Invalid advancement hint key: {Key}");
                throw new InvalidCastException();
            }
        }
    }

    public override void Write(Utf8JsonWriter writer, SkillAdvancementHintCollection value, JsonSerializerOptions options)
    {
        WriteTableDictionary(writer, value, options);
    }

    private void WriteTableDictionary(Utf8JsonWriter writer, SkillAdvancementHintCollection value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();

        foreach (SkillAdvancementHint AdvancementHint in value)
        {
            string Key = AdvancementHint.Level.ToString();
            string? Hint = AdvancementHint.Hint;

            writer.WriteString(Key, Hint);
        }

        writer.WriteEndObject();
    }
}

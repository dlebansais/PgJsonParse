namespace Preprocessor;

using System;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;

public class SkillReportCollectionJsonConverter : JsonConverter<SkillReportCollection>
{
    public override SkillReportCollection? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        SkillReportCollection Result = new();

        ReadTableDictionary(Result, ref reader, options);

        return Result;
    }

    private void ReadTableDictionary(SkillReportCollection collection, ref Utf8JsonReader reader, JsonSerializerOptions options)
    {
        while (reader.Read() && reader.TokenType == JsonTokenType.PropertyName)
        {
            string Key = reader.GetString() ?? throw new InvalidCastException();
            reader.Read();
            string Hint = reader.GetString() ?? throw new InvalidCastException();

            if (int.TryParse(Key, out int Level))
            {
                SkillReport Report = new();
                Report.Level = Level;
                Report.Report = Hint;
                collection.Add(Report);
            }
            else
            {
                Debug.WriteLine($"Invalid report key: {Key}");
                throw new InvalidCastException();
            }
        }
    }

    public override void Write(Utf8JsonWriter writer, SkillReportCollection value, JsonSerializerOptions options)
    {
        WriteTableDictionary(writer, value, options);
    }

    private void WriteTableDictionary(Utf8JsonWriter writer, SkillReportCollection value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();

        foreach (SkillReport Report in value)
        {
            string Key = Report.Level.ToString();
            string? Hint = Report.Report;

            writer.WriteString(Key, Hint);
        }

        writer.WriteEndObject();
    }
}

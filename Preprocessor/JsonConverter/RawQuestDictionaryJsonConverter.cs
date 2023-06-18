namespace Preprocessor;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;

internal class RawQuestDictionaryJsonConverter : JsonConverter<RawQuestDictionary>
{
    public override RawQuestDictionary? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        RawQuestDictionary Result = new();

        ReadDictionary(Result, ref reader, options);

        return Result;
    }

    private void ReadDictionary(RawQuestDictionary dictionary, ref Utf8JsonReader reader, JsonSerializerOptions options)
    {
        while (reader.Read() && reader.TokenType == JsonTokenType.PropertyName)
        {
            string Key = reader.GetString() ?? throw new InvalidCastException();
            reader.Read();

            RawQuest? Quest = null;
            Exception? Exception1 = null;

            try
            {
                Quest = JsonSerializer.Deserialize<RawQuest>(ref reader, options) ?? throw new InvalidCastException();
            }
            catch (Exception Exception)
            {
                Exception1 = Exception;
            }

            if (Key.StartsWith("quest_") && int.TryParse(Key.Substring(6), out int QuestKey))
            {
                if (Quest is not null)
                    dictionary.Add(QuestKey, Quest);
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

    public override void Write(Utf8JsonWriter writer, RawQuestDictionary value, JsonSerializerOptions options)
    {
        WriteDictionary(writer, value, options);
    }

    private void WriteDictionary(Utf8JsonWriter writer, RawQuestDictionary value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();

        foreach (KeyValuePair<int, RawQuest> Entry in value)
        {
            string Key = $"quest_{Entry.Key}";
            RawQuest Quest = Entry.Value;

            writer.WritePropertyName(Key);

            JsonSerializer.Serialize(writer, Quest, options);
        }

        writer.WriteEndObject();
    }
}

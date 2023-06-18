namespace Preprocessor;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;

internal class DirectedGoalDictionaryJsonConverter : JsonConverter<DirectedGoalDictionary>
{
    public override DirectedGoalDictionary? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        DirectedGoalDictionary Result = new();

        ReadDictionary(Result, ref reader, options);

        return Result;
    }

    private void ReadDictionary(DirectedGoalDictionary dictionary, ref Utf8JsonReader reader, JsonSerializerOptions options)
    {
        while (reader.Read() && reader.TokenType == JsonTokenType.StartObject)
        {
            int Key = 0;
            RawDirectedGoal? DirectedGoal1 = null;
            Exception? Exception1 = null;

            try
            {
                DirectedGoal1 = JsonSerializer.Deserialize<RawDirectedGoal>(ref reader, options) ?? throw new InvalidCastException();
                Key = DirectedGoal1.Id;
            }
            catch (Exception Exception)
            {
                Exception1 = Exception;
            }

            if (DirectedGoal1 is not null)
                dictionary.Add(Key, new DirectedGoal(DirectedGoal1));
            else
            {
                Debug.WriteLine($"\r\nKey: {Key}");
                Debug.WriteLine(Exception1?.Message);
                throw new InvalidCastException();
            }
        }
    }

    public override void Write(Utf8JsonWriter writer, DirectedGoalDictionary value, JsonSerializerOptions options)
    {
        WriteDictionary(writer, value, options);
    }

    private void WriteDictionary(Utf8JsonWriter writer, DirectedGoalDictionary value, JsonSerializerOptions options)
    {
        writer.WriteStartArray();

        foreach (KeyValuePair<int, DirectedGoal> Entry in value)
        {
            DirectedGoal DirectedGoal = Entry.Value;
            JsonSerializer.Serialize(writer, DirectedGoal.ToRawDirectedGoal(), options);
        }

        writer.WriteEndArray();
    }
}

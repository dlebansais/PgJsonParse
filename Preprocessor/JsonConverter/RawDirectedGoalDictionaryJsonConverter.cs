namespace Preprocessor;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;

internal class RawDirectedGoalDictionaryJsonConverter : JsonConverter<RawDirectedGoalDictionary>
{
    public override RawDirectedGoalDictionary? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        RawDirectedGoalDictionary Result = new();

        ReadDictionary(Result, ref reader, options);

        return Result;
    }

    private void ReadDictionary(RawDirectedGoalDictionary dictionary, ref Utf8JsonReader reader, JsonSerializerOptions options)
    {
        while (reader.Read() && reader.TokenType == JsonTokenType.StartObject)
        {
            int Key = 0;
            RawDirectedGoal1? DirectedGoal1 = null;
            Exception? Exception1 = null;

            try
            {
                DirectedGoal1 = JsonSerializer.Deserialize<RawDirectedGoal1>(ref reader, options) ?? throw new InvalidCastException();
                Key = DirectedGoal1.Id;
            }
            catch (Exception Exception)
            {
                Exception1 = Exception;
            }

            if (DirectedGoal1 is not null)
                dictionary.Add(Key, new RawDirectedGoal(DirectedGoal1));
            else
            {
                Debug.WriteLine($"\r\nKey: {Key}");
                Debug.WriteLine(Exception1?.Message);
                throw new InvalidCastException();
            }
        }
    }

    public override void Write(Utf8JsonWriter writer, RawDirectedGoalDictionary value, JsonSerializerOptions options)
    {
        WriteDictionary(writer, value, options);
    }

    private void WriteDictionary(Utf8JsonWriter writer, RawDirectedGoalDictionary value, JsonSerializerOptions options)
    {
        writer.WriteStartArray();

        foreach (KeyValuePair<int, RawDirectedGoal> Entry in value)
        {
            RawDirectedGoal DirectedGoal = Entry.Value;
            JsonSerializer.Serialize(writer, DirectedGoal.ToRawDirectedGoal1(), options);
        }

        writer.WriteEndArray();
    }
}

namespace Preprocessor;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;

public class DirectedGoalDictionaryJsonConverter : JsonConverter<DirectedGoalDictionary>
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
            RawDirectedGoal? RawDirectedGoal = null;
            Exception? Exception1 = null;

            try
            {
                RawDirectedGoal = JsonSerializer.Deserialize<RawDirectedGoal>(ref reader, options) ?? throw new NullReferenceException();
                Key = RawDirectedGoal.Id;
            }
            catch (Exception Exception)
            {
                Exception1 = Exception;
            }

            if (RawDirectedGoal is not null)
                dictionary.Add(Key, new DirectedGoal(RawDirectedGoal));
            else
            {
                Debug.WriteLine($"\r\nKey: {Key}");
                Debug.WriteLine(Exception1?.Message);
                throw new PreprocessorException(this);
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

namespace Preprocessor;

using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

public class ArrayJsonConverter<TElement, TRawElement, TArray> : JsonConverter<TArray>
    where TElement : class
    where TRawElement : class
    where TArray : List<TElement>, IDictionaryValueBuilder<TElement, TRawElement>, new()
{
    public override TArray? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        TArray Result = new();

        reader.Read();
        ReadArray(Result, ref reader, options);

        if (reader.TokenType != JsonTokenType.EndArray)
            throw new PreprocessorException("Expected EndArray.");

        return Result;
    }

    private void ReadArray(TArray dictionary, ref Utf8JsonReader reader, JsonSerializerOptions options)
    {
        while (reader.TokenType == JsonTokenType.StartObject)
        {
            try
            {
                TRawElement RawElement = JsonSerializer.Deserialize<TRawElement>(ref reader, options) ?? throw new NullReferenceException();
                dictionary.Add(dictionary.FromRaw(RawElement));
            }
            catch (Exception Exception)
            {
                throw new PreprocessorException(Exception);
            }

            if (reader.TokenType != JsonTokenType.EndObject)
                throw new PreprocessorException("Expected EndObject.");

            reader.Read();
        }
    }

    public override void Write(Utf8JsonWriter writer, TArray value, JsonSerializerOptions options)
    {
        WriteArray(writer, value, options);
    }

    private void WriteArray(Utf8JsonWriter writer, TArray value, JsonSerializerOptions options)
    {
        writer.WriteStartArray();

        foreach (TElement Element in value)
        {
            JsonSerializer.Serialize(writer, value.ToRaw(Element), options);
        }

        writer.WriteEndArray();
    }
}

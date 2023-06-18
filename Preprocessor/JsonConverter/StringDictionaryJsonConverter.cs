namespace Preprocessor;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;

internal class StringDictionaryJsonConverter<TItem, TRawItem, TDictionary> : JsonConverter<TDictionary>
    where TItem : class
    where TRawItem : class
    where TDictionary : Dictionary<string, TItem>, IDictionaryValueBuilder<TItem, TRawItem>, new()
{
    public override TDictionary? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        TDictionary Result = new();

        ReadDictionary(Result, ref reader, options);

        return Result;
    }

    private void ReadDictionary(TDictionary dictionary, ref Utf8JsonReader reader, JsonSerializerOptions options)
    {
        while (reader.Read() && reader.TokenType == JsonTokenType.PropertyName)
        {
            string Key = reader.GetString() ?? throw new InvalidCastException();
            reader.Read();

            TRawItem? RawItem = null;
            Exception? Exception1 = null;

            try
            {
                RawItem = JsonSerializer.Deserialize<TRawItem>(ref reader, options) ?? throw new InvalidCastException();
            }
            catch (Exception Exception)
            {
                Exception1 = Exception;
            }

            if (RawItem is not null)
                dictionary.Add(Key, dictionary.ToItem(RawItem));
            else
            {
                Debug.WriteLine($"\r\nKey: {Key}");
                Debug.WriteLine(Exception1?.Message);
                throw new InvalidCastException();
            }
        }
    }

    public override void Write(Utf8JsonWriter writer, TDictionary value, JsonSerializerOptions options)
    {
        WriteDictionary(writer, value, options);
    }

    private void WriteDictionary(Utf8JsonWriter writer, TDictionary value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();

        foreach (KeyValuePair<string, TItem> Entry in value)
        {
            string Key = Entry.Key;
            TItem Item = Entry.Value;

            writer.WritePropertyName(Key);

            JsonSerializer.Serialize(writer, value.ToRawItem(Item), options);
        }

        writer.WriteEndObject();
    }
}

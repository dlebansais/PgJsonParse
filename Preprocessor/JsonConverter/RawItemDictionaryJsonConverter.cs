namespace Preprocessor;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;

internal class RawItemDictionaryJsonConverter : JsonConverter<RawItemDictionary>
{
    public override RawItemDictionary? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        RawItemDictionary Result = new();

        ReadDictionary(Result, ref reader, options);

        return Result;
    }

    private void ReadDictionary(RawItemDictionary dictionary, ref Utf8JsonReader reader, JsonSerializerOptions options)
    {
        while (reader.Read() && reader.TokenType == JsonTokenType.PropertyName)
        {
            string Key = reader.GetString() ?? throw new InvalidCastException();
            reader.Read();

            RawItem? Item = null;
            Exception? Exception1 = null;

            try
            {
                Item = JsonSerializer.Deserialize<RawItem>(ref reader, options) ?? throw new InvalidCastException();
            }
            catch (Exception Exception)
            {
                Exception1 = Exception;
            }

            if (Key.StartsWith("item_") && int.TryParse(Key.Substring(5), out int ItemKey))
            {
                if (Item is not null)
                    dictionary.Add(ItemKey, Item);
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

    public override void Write(Utf8JsonWriter writer, RawItemDictionary value, JsonSerializerOptions options)
    {
        WriteDictionary(writer, value, options);
    }

    private void WriteDictionary(Utf8JsonWriter writer, RawItemDictionary value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();

        foreach (KeyValuePair<int, RawItem> Entry in value)
        {
            string Key = $"item_{Entry.Key}";
            RawItem Item = Entry.Value;

            writer.WritePropertyName(Key);

            JsonSerializer.Serialize(writer, Item, options);
        }

        writer.WriteEndObject();
    }
}

namespace Preprocessor;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;

internal class RawItemUseDictionaryJsonConverter : JsonConverter<RawItemUseDictionary>
{
    public override RawItemUseDictionary? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        RawItemUseDictionary Result = new();

        ReadDictionary(Result, ref reader, options);

        return Result;
    }

    private void ReadDictionary(RawItemUseDictionary dictionary, ref Utf8JsonReader reader, JsonSerializerOptions options)
    {
        while (reader.Read() && reader.TokenType == JsonTokenType.PropertyName)
        {
            string Key = reader.GetString() ?? throw new InvalidCastException();
            reader.Read();

            RawItemUse? ItemUse = null;
            Exception? Exception1 = null;

            try
            {
                ItemUse = JsonSerializer.Deserialize<RawItemUse>(ref reader, options) ?? throw new InvalidCastException();
            }
            catch (Exception Exception)
            {
                Exception1 = Exception;
            }

            if (Key.StartsWith("item_") && int.TryParse(Key.Substring(5), out int ItemUseKey))
            {
                if (ItemUse is not null)
                    dictionary.Add(ItemUseKey, ItemUse);
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

    public override void Write(Utf8JsonWriter writer, RawItemUseDictionary value, JsonSerializerOptions options)
    {
        WriteDictionary(writer, value, options);
    }

    private void WriteDictionary(Utf8JsonWriter writer, RawItemUseDictionary value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();

        foreach (KeyValuePair<int, RawItemUse> Entry in value)
        {
            string Key = $"item_{Entry.Key}";
            RawItemUse ItemUse = Entry.Value;

            writer.WritePropertyName(Key);

            JsonSerializer.Serialize(writer, ItemUse, options);
        }

        writer.WriteEndObject();
    }
}

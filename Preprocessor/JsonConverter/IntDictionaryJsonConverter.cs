namespace Preprocessor;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;

internal class IntDictionaryJsonConverter<TElement, TRawElement, TDictionary> : JsonConverter<TDictionary>
    where TElement : class
    where TRawElement : class
    where TDictionary : Dictionary<int, TElement>, IDictionaryValueBuilder<TElement, TRawElement>, new()
{
    public IntDictionaryJsonConverter(string prefix)
    {
        Prefix = prefix;
    }

    public string Prefix { get; }

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

            TRawElement? RawElement = null;
            Exception? DeserializingException = null;

            try
            {
                RawElement = JsonSerializer.Deserialize<TRawElement>(ref reader, options) ?? throw new InvalidCastException();
            }
            catch (Exception Exception)
            {
                DeserializingException = Exception;
            }

            if (Key.StartsWith($"{Prefix}_") && int.TryParse(Key.Substring(Prefix.Length + 1), out int AbilityKey))
            {
                if (RawElement is not null)
                    dictionary.Add(AbilityKey, dictionary.FromRaw(RawElement));
                else
                {
                    Debug.WriteLine($"\r\nKey: {Key}");
                    Debug.WriteLine(DeserializingException?.Message);
                    throw new InvalidCastException();
                }
            }
            else
            {
                Debug.WriteLine($"Invalid {Prefix} key: {Key}");
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

        foreach (KeyValuePair<int, TElement> Entry in value)
        {
            string Key = $"{Prefix}_{Entry.Key}";
            TElement Element = Entry.Value;

            writer.WritePropertyName(Key);

            JsonSerializer.Serialize(writer, value.ToRaw(Element), options);
        }

        writer.WriteEndObject();
    }
}

namespace Preprocessor;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;

internal class IntDictionaryJsonConverter<TItem, TRawItem, TDictionary> : JsonConverter<TDictionary>
    where TItem : class
    where TRawItem : class
    where TDictionary : Dictionary<int, TItem>, IDictionaryValueBuilder<TItem, TRawItem>, new()
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

            TRawItem? RawAbility1 = null;
            Exception? Exception1 = null;

            try
            {
                RawAbility1 = JsonSerializer.Deserialize<TRawItem>(ref reader, options) ?? throw new InvalidCastException();
            }
            catch (Exception Exception)
            {
                Exception1 = Exception;
            }

            if (Key.StartsWith($"{Prefix}_") && int.TryParse(Key.Substring(Prefix.Length + 1), out int AbilityKey))
            {
                if (RawAbility1 is not null)
                    dictionary.Add(AbilityKey, dictionary.ToItem(RawAbility1));
                else
                {
                    Debug.WriteLine($"\r\nKey: {Key}");
                    Debug.WriteLine(Exception1?.Message);
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

        foreach (KeyValuePair<int, TItem> Entry in value)
        {
            string Key = $"{Prefix}_{Entry.Key}";
            TItem Ability = Entry.Value;

            writer.WritePropertyName(Key);

            JsonSerializer.Serialize(writer, value.ToRawItem(Ability), options);
        }

        writer.WriteEndObject();
    }
}

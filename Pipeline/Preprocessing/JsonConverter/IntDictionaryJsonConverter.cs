namespace Preprocessor;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;

public class IntDictionaryJsonConverter<TElement, TRawElement, TDictionary> : JsonConverter<TDictionary>
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
            string Key = reader.GetString() ?? throw new NullReferenceException();
            reader.Read();

            TRawElement? RawElement = null;
            Exception? DeserializingException = null;

            try
            {
                RawElement = JsonSerializer.Deserialize<TRawElement>(ref reader, options) ?? throw new NullReferenceException();
            }
            catch (Exception Exception)
            {
                DeserializingException = Exception;
            }

            string PrefixWithUnderscore = $"{Prefix}_";
            int ElementKey;

            if ((Prefix == string.Empty && int.TryParse(Key, out ElementKey)) || 
                (Key.StartsWith(PrefixWithUnderscore) && int.TryParse(Key.Substring(PrefixWithUnderscore.Length), out ElementKey)))
            {
                if (RawElement is not null)
                    dictionary.Add(ElementKey, dictionary.FromRaw(RawElement));
                else
                {
                    Debug.WriteLine($"\r\nKey: {Key}");
                    Debug.WriteLine(DeserializingException?.Message);
                    PreprocessorException.Throw(this);
                }
            }
            else
            {
                Debug.WriteLine($"Invalid {Prefix} key: {Key}");
                PreprocessorException.Throw(this);
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

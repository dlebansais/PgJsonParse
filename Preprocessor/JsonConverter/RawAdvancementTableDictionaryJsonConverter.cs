namespace Preprocessor;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;

internal class RawAdvancementTableDictionaryJsonConverter : JsonConverter<RawAdvancementTableDictionary>
{
    public override RawAdvancementTableDictionary? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        RawAdvancementTableDictionary Result = new();

        ReadTableDictionary(Result, ref reader, options);

        return Result;
    }

    private void ReadTableDictionary(RawAdvancementTableDictionary dictionary, ref Utf8JsonReader reader, JsonSerializerOptions options)
    {
        while (reader.Read() && reader.TokenType == JsonTokenType.PropertyName)
        {
            string Key = reader.GetString() ?? throw new InvalidCastException();
            reader.Read();

            RawAdvancementDictionary? Table = new();
            Exception? Exception1 = null;

            try
            {
                ReadAdvancementDictionary(Table, ref reader, options);
            }
            catch (Exception Exception)
            {
                Exception1 = Exception;
                Table = null;
            }

            string[] SplittedKey = Key.Split('_');
            if (SplittedKey.Length >= 2 && int.TryParse(SplittedKey[0], out int AdvancementTableKey) && SplittedKey[1].Trim() is string Name && Name != string.Empty)
            {
                for (int i = 2; i < SplittedKey.Length; i++)
                    Name += $"_{SplittedKey[i]}";

                if (Table is not null)
                    dictionary.Add(AdvancementTableKey, new RawAdvancementTable(Name, Table));
                else
                {
                    Debug.WriteLine($"\r\nKey: {Key}");
                    Debug.WriteLine(Exception1?.Message);
                    throw new InvalidCastException();
                }
            }
            else
            {
                Debug.WriteLine($"Invalid advancement table key: {Key}");
                throw new InvalidCastException();
            }
        }
    }

    private void ReadAdvancementDictionary(RawAdvancementDictionary dictionary, ref Utf8JsonReader reader, JsonSerializerOptions options)
    {
        while (reader.Read() && reader.TokenType == JsonTokenType.PropertyName)
        {
            string Key = reader.GetString() ?? throw new InvalidCastException();
            reader.Read();

            RawAdvancementEffectAttributeCollection? Collection = new();
            Exception? Exception1 = null;

            try
            {
                ReadAdvancementEffectAttributeCollection(Collection, ref reader, options);
            }
            catch (Exception Exception)
            {
                Exception1 = Exception;
                Collection = null;
            }

            if (Key.StartsWith("Level_") && int.TryParse(Key.Substring(6), out int AdvancementKey))
            {
                if (Collection is not null)
                    dictionary.Add(AdvancementKey, new RawAdvancement(Collection));
                else
                {
                    Debug.WriteLine($"\r\nKey: {Key}");
                    Debug.WriteLine(Exception1?.Message);
                    throw new InvalidCastException();
                }
            }
            else
            {
                Debug.WriteLine($"Invalid advancement key: {Key}");
                throw new InvalidCastException();
            }
        }
    }

    private void ReadAdvancementEffectAttributeCollection(RawAdvancementEffectAttributeCollection collection, ref Utf8JsonReader reader, JsonSerializerOptions options)
    {
        while (reader.Read() && reader.TokenType == JsonTokenType.PropertyName)
        {
            string Attribute = reader.GetString() ?? throw new InvalidCastException();
            reader.Read();
            decimal Value = reader.GetDecimal();

            collection.Add(new RawAdvancementEffectAttribute() { Attribute = Attribute, Value = Value });
        }
    }

    public override void Write(Utf8JsonWriter writer, RawAdvancementTableDictionary value, JsonSerializerOptions options)
    {
        WriteTableDictionary(writer, value, options);
    }

    private void WriteTableDictionary(Utf8JsonWriter writer, RawAdvancementTableDictionary value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();

        foreach (KeyValuePair<int, RawAdvancementTable> Entry in value)
        {
            string Key = $"{Entry.Key}_{Entry.Value.Name}";
            RawAdvancementTable Table = Entry.Value;

            writer.WritePropertyName(Key);

            WriteAdvancementDictionary(writer, Table, options);
        }

        writer.WriteEndObject();
    }

    private void WriteAdvancementDictionary(Utf8JsonWriter writer, RawAdvancementTable value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();

        foreach (KeyValuePair<int, RawAdvancement> Entry in value.Levels)
        {
            string Key = Entry.Key < 10 ? $"Level_0{Entry.Key}" : $"Level_{Entry.Key}";
            RawAdvancement Advancement = Entry.Value;

            writer.WritePropertyName(Key);

            WriteAdvancementEffectAttributeCollection(writer, Advancement.Attributes, options);
        }

        writer.WriteEndObject();
    }

    private void WriteAdvancementEffectAttributeCollection(Utf8JsonWriter writer, RawAdvancementEffectAttributeCollection value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();

        foreach (RawAdvancementEffectAttribute Item in value)
        {
            writer.WriteNumber(Item.Attribute, Item.Value);
        }

        writer.WriteEndObject();
    }
}

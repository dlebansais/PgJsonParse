namespace Preprocessor;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;

internal class AdvancementTableDictionaryJsonConverter : JsonConverter<AdvancementTableDictionary>
{
    public override AdvancementTableDictionary? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        AdvancementTableDictionary Result = new();

        ReadTableDictionary(Result, ref reader, options);

        return Result;
    }

    private void ReadTableDictionary(AdvancementTableDictionary dictionary, ref Utf8JsonReader reader, JsonSerializerOptions options)
    {
        while (reader.Read() && reader.TokenType == JsonTokenType.PropertyName)
        {
            string Key = reader.GetString() ?? throw new InvalidCastException();
            reader.Read();

            AdvancementCollection? Advancements = new();
            Exception? Exception1 = null;

            try
            {
                ReadAdvancementDictionary(Advancements, ref reader, options);
            }
            catch (Exception Exception)
            {
                Exception1 = Exception;
                Advancements = null;
            }

            string[] SplittedKey = Key.Split('_');
            if (SplittedKey.Length >= 2 && int.TryParse(SplittedKey[0], out int AdvancementTableKey) && SplittedKey[1].Trim() is string Name && Name != string.Empty)
            {
                for (int i = 2; i < SplittedKey.Length; i++)
                    Name += $"_{SplittedKey[i]}";

                if (Advancements is not null)
                    dictionary.Add(AdvancementTableKey, new AdvancementTable(Name, Advancements));
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

    private void ReadAdvancementDictionary(AdvancementCollection advancements, ref Utf8JsonReader reader, JsonSerializerOptions options)
    {
        while (reader.Read() && reader.TokenType == JsonTokenType.PropertyName)
        {
            string Key = reader.GetString() ?? throw new InvalidCastException();
            reader.Read();

            AdvancementEffectAttributeCollection? Collection = new();
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
                    advancements.Add(new Advancement(Collection, AdvancementKey));
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

        advancements.Sort(SortByLevel);
    }

    private void ReadAdvancementEffectAttributeCollection(AdvancementEffectAttributeCollection collection, ref Utf8JsonReader reader, JsonSerializerOptions options)
    {
        while (reader.Read() && reader.TokenType == JsonTokenType.PropertyName)
        {
            string Attribute = reader.GetString() ?? throw new InvalidCastException();
            reader.Read();
            decimal Value = reader.GetDecimal();

            collection.Add(new AdvancementEffectAttribute() { Attribute = Attribute, Value = Value });
        }
    }

    public override void Write(Utf8JsonWriter writer, AdvancementTableDictionary value, JsonSerializerOptions options)
    {
        WriteTableDictionary(writer, value, options);
    }

    private void WriteTableDictionary(Utf8JsonWriter writer, AdvancementTableDictionary value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();

        foreach (KeyValuePair<int, AdvancementTable> Entry in value)
        {
            string Key = $"{Entry.Key}_{Entry.Value.Name}";
            AdvancementTable Table = Entry.Value;

            writer.WritePropertyName(Key);

            WriteAdvancementDictionary(writer, Table, options);
        }

        writer.WriteEndObject();
    }

    private void WriteAdvancementDictionary(Utf8JsonWriter writer, AdvancementTable value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();

        List<Advancement> AdvancementList = new(value.Levels);
        AdvancementList.Sort(SortByKey);

        foreach (Advancement Advancement in AdvancementList)
        {
            string Key = AdvancementToKey(Advancement);

            writer.WritePropertyName(Key);
            WriteAdvancementEffectAttributeCollection(writer, Advancement.Attributes, options);
        }

        writer.WriteEndObject();
    }

    private static string AdvancementToKey(Advancement advancement)
    {
        return advancement.Level < 10 ? $"Level_0{advancement.Level}" : $"Level_{advancement.Level}";
    }

    private static int SortByLevel(Advancement advancement1, Advancement advancement2)
    {
        return advancement1.Level - advancement2.Level;
    }

    private static int SortByKey(Advancement advancement1, Advancement advancement2)
    {
        return string.Compare(AdvancementToKey(advancement1), AdvancementToKey(advancement2), StringComparison.Ordinal);
    }

    private void WriteAdvancementEffectAttributeCollection(Utf8JsonWriter writer, AdvancementEffectAttributeCollection value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();

        foreach (AdvancementEffectAttribute Item in value)
        {
            writer.WriteNumber(Item.Attribute, Item.Value);
        }

        writer.WriteEndObject();
    }
}

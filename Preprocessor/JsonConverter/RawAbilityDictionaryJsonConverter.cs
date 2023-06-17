namespace Preprocessor;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;

internal class RawAbilityDictionaryJsonConverter : JsonConverter<RawAbilityDictionary>
{
    public override RawAbilityDictionary? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        RawAbilityDictionary Result = new();

        ReadDictionary(Result, ref reader, options);

        return Result;
    }

    private void ReadDictionary(RawAbilityDictionary dictionary, ref Utf8JsonReader reader, JsonSerializerOptions options)
    {
        while (reader.Read() && reader.TokenType == JsonTokenType.PropertyName)
        {
            string Key = reader.GetString() ?? throw new InvalidCastException();
            reader.Read();

            RawAbility1? RawAbility1 = null;
            RawAbility2? RawAbility2 = null;
            Exception? Exception1 = null;
            Exception? Exception2 = null;

            try
            {
                RawAbility1 = JsonSerializer.Deserialize<RawAbility1>(ref reader, options) ?? throw new InvalidCastException();
            }
            catch (Exception Exception)
            {
                Exception1 = Exception;
            }

            try
            {
                if (RawAbility1 is null)
                    RawAbility2 = JsonSerializer.Deserialize<RawAbility2>(ref reader, options) ?? throw new InvalidCastException();
            }
            catch (Exception Exception)
            {
                Exception2 = Exception;
            }

            if (Key.StartsWith("ability_") && int.TryParse(Key.Substring(8), out int AbilityKey))
            {
                if (RawAbility1 is not null)
                    dictionary.Add(AbilityKey, new RawAbility(RawAbility1));
                else if (RawAbility2 is not null)
                    dictionary.Add(AbilityKey, new RawAbility(RawAbility2));
                else
                {
                    Debug.WriteLine($"\r\nKey: {Key}");
                    Debug.WriteLine(Exception1?.Message);
                    Debug.WriteLine(Exception2?.Message);
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

    public override void Write(Utf8JsonWriter writer, RawAbilityDictionary value, JsonSerializerOptions options)
    {
        WriteDictionary(writer, value, options);
    }

    private void WriteDictionary(Utf8JsonWriter writer, RawAbilityDictionary value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();

        foreach (KeyValuePair<int, RawAbility> Entry in value)
        {
            string Key = $"ability_{Entry.Key}";
            RawAbility Ability = Entry.Value;

            writer.WritePropertyName(Key);

            if (Ability.SpecialCasterRequirementsIsSingle)
                JsonSerializer.Serialize(writer, Ability.ToRawAbility1(), options);
            else
                JsonSerializer.Serialize(writer, Ability.ToRawAbility2(), options);
        }

        writer.WriteEndObject();
    }
}

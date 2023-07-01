namespace Preprocessor;

using System;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;

internal class PowerTierCollectionJsonConverter : JsonConverter<PowerTierCollection>
{
    public override PowerTierCollection? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        PowerTierCollection Result = new();

        ReadTableDictionary(Result, ref reader, options);

        return Result;
    }

    private void ReadTableDictionary(PowerTierCollection collection, ref Utf8JsonReader reader, JsonSerializerOptions options)
    {
        while (reader.Read() && reader.TokenType == JsonTokenType.PropertyName)
        {
            string Key = reader.GetString() ?? throw new InvalidCastException();
            reader.Read();

            RawPowerTier? RawPowerTier = null;
            Exception? Exception1 = null;

            try
            {
                RawPowerTier = JsonSerializer.Deserialize<RawPowerTier>(ref reader, options);
            }
            catch (Exception Exception)
            {
                Exception1 = Exception;
            }

            if (Key.StartsWith("id_") && int.TryParse(Key.Substring(3), out int Tier) && RawPowerTier is not null)
                collection.Add(new PowerTier(Tier, RawPowerTier));
            else
            {
                Debug.WriteLine($"\r\nInvalid Tier: {Key}");
                Debug.WriteLine(Exception1?.Message);
                throw new InvalidCastException();
            }
        }
    }

    public override void Write(Utf8JsonWriter writer, PowerTierCollection value, JsonSerializerOptions options)
    {
        WriteTableDictionary(writer, value, options);
    }

    private void WriteTableDictionary(Utf8JsonWriter writer, PowerTierCollection value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();

        foreach (PowerTier PowerTier in value)
        {
            string Key = $"id_{PowerTier.Tier}";
            RawPowerTier RawPowerTier = PowerTier.ToRawPowerTier();

            writer.WritePropertyName(Key);
            JsonSerializer.Serialize(writer, RawPowerTier, options);
        }

        writer.WriteEndObject();
    }
}

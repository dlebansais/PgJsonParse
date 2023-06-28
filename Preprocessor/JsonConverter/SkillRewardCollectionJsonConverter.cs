namespace Preprocessor;

using System;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;

internal class SkillRewardCollectionJsonConverter : JsonConverter<SkillRewardCollection>
{
    public override SkillRewardCollection? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        SkillRewardCollection Result = new();

        ReadTableDictionary(Result, ref reader, options);

        return Result;
    }

    private void ReadTableDictionary(SkillRewardCollection collection, ref Utf8JsonReader reader, JsonSerializerOptions options)
    {
        while (reader.Read() && reader.TokenType == JsonTokenType.PropertyName)
        {
            string Key = reader.GetString() ?? throw new InvalidCastException();
            reader.Read();

            RawSkillReward? RawReward = null;
            Exception? Exception1 = null;

            try
            {
                RawReward = JsonSerializer.Deserialize<RawSkillReward>(ref reader, options);
            }
            catch (Exception Exception)
            {
                Exception1 = Exception;
            }

            string[] SplittedKey = Key.Split('_');
            if (SplittedKey.Length >= 1 && int.TryParse(SplittedKey[0], out int Level))
            {
                string[]? Races = null;

                if (SplittedKey.Length >= 2)
                {
                    Races = new string[SplittedKey.Length - 1];
                    for (int i = 1; i < SplittedKey.Length; i++)
                        Races[i - 1] = SplittedKey[i];
                }

                if (RawReward is not null)
                {
                    RawReward.Level = Level;
                    RawReward.Races = Races;
                    collection.Add(new SkillReward(RawReward));
                }
                else
                {
                    Debug.WriteLine($"\r\nKey: {Key}");
                    Debug.WriteLine(Exception1?.Message);
                    throw new InvalidCastException();
                }
            }
            else
            {
                Debug.WriteLine($"Invalid skill reward key: {Key}");
                throw new InvalidCastException();
            }
        }
    }

    public override void Write(Utf8JsonWriter writer, SkillRewardCollection value, JsonSerializerOptions options)
    {
        WriteTableDictionary(writer, value, options);
    }

    private void WriteTableDictionary(Utf8JsonWriter writer, SkillRewardCollection value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();

        foreach (SkillReward Reward in value)
        {
            RawSkillReward RawReward = Reward.ToRawSkillReward();

            string Key;

            if (RawReward.Races is null)
                Key = RawReward.Level.ToString();
            else
                Key = $"{RawReward.Level}_{string.Join("_", RawReward.Races)}";

            RawReward.Level = null;
            RawReward.Races = null;

            writer.WritePropertyName(Key);
            JsonSerializer.Serialize(writer, RawReward, options);
        }

        writer.WriteEndObject();
    }
}

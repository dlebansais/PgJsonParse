namespace Preprocessor;

using System;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;

internal class SkillLevelCapCollectionJsonConverter : JsonConverter<SkillLevelCapCollection>
{
    public override SkillLevelCapCollection? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        SkillLevelCapCollection Result = new();

        ReadTableDictionary(Result, ref reader, options);

        return Result;
    }

    private SkillLevelCap CreateSkillLevelCap(int level, int skillCap, string skill, bool isPerformanceSkill)
    {
        SkillLevelCap Result = new();
        Result.Level = level;
        Result.SkillCap = skillCap;
        Result.Skill = skill;
        Result.IsPerformanceSkill = isPerformanceSkill;

        return Result;
    }

    private void ReadTableDictionary(SkillLevelCapCollection dictionary, ref Utf8JsonReader reader, JsonSerializerOptions options)
    {
        while (reader.Read() && reader.TokenType == JsonTokenType.PropertyName)
        {
            string Key = reader.GetString() ?? throw new InvalidCastException();
            reader.Read();
            int Level = reader.GetInt32();
            SkillLevelCap LevelCap;

            string[] SplittedKey = Key.Split('_');
            if (SplittedKey.Length == 3 && SplittedKey[0] == "LevelCap" && SplittedKey[1].Trim() is string Skill && Skill != string.Empty && int.TryParse(SplittedKey[2], out int SkillCap))
            {
                if (Skill == "ArmorSmithing")
                    Skill = "Armorsmithing";

                LevelCap = CreateSkillLevelCap(Level, SkillCap, Skill, isPerformanceSkill: Skill == "Dance");
            }
            else if (SplittedKey.Length == 3 && SplittedKey[0] == "LevelCap" && SplittedKey[1] == "Performance" && SplittedKey[2].Trim() is string PerformanceSkill && PerformanceSkill != string.Empty && int.TryParse(PerformanceSkill.Substring(PerformanceSkill.Length - 2), out int PerformanceSkillCap))
            {
                LevelCap = CreateSkillLevelCap(Level, PerformanceSkillCap, PerformanceSkill.Substring(0, PerformanceSkill.Length - 2), isPerformanceSkill: true);
            }
            else
            {
                Debug.WriteLine($"\r\nInvalid level cap key: {Key}");
                throw new InvalidCastException();
            }

            dictionary.Add(LevelCap);
        }
    }

    public override void Write(Utf8JsonWriter writer, SkillLevelCapCollection value, JsonSerializerOptions options)
    {
        WriteTableDictionary(writer, value, options);
    }

    private void WriteTableDictionary(Utf8JsonWriter writer, SkillLevelCapCollection value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();

        foreach (SkillLevelCap SkillLevelCap in value)
        {
            string Key;

            if (SkillLevelCap.IsPerformanceSkill && SkillLevelCap.Skill != "Dance")
                Key = $"LevelCap_Performance_{SkillLevelCap.Skill}{SkillLevelCap.SkillCap}";
            else
            {
                string? SkillName = SkillLevelCap.Skill == "Armorsmithing" ? "ArmorSmithing" : SkillLevelCap.Skill;
                Key = $"LevelCap_{SkillName}_{SkillLevelCap.SkillCap}";
            }

            int Level = SkillLevelCap.Level;

            writer.WriteNumber(Key, Level);
        }

        writer.WriteEndObject();
    }
}

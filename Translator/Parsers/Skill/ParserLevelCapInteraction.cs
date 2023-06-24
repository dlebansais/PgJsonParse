namespace Translator;

using System.Collections.Generic;
using PgJsonReader;
using PgObjects;

public class ParserLevelCapInteraction : Parser
{
    public override object CreateItem()
    {
        return new PgLevelCapInteraction();
    }

    public override bool FinishItem(ref object? item, string objectKey, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, string parsedFile, string parsedKey)
    {
        if (item is not PgLevelCapInteraction AsPgLevelCapInteraction)
            return Program.ReportFailure("Unexpected failure");

        return FinishItem(AsPgLevelCapInteraction, contentTable, contentTypeTable, itemCollection, lastItemType, parsedFile, parsedKey);
    }

    private bool FinishItem(PgLevelCapInteraction item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, string parsedFile, string parsedKey)
    {
        bool Result = true;
        string? SkillName = null;
        int? Level = null;
        int? OtherLevel = null;
        bool IsPerformanceSkill = false;

        foreach (KeyValuePair<string, object> Entry in contentTable)
        {
            string Key = Entry.Key;
            object Value = Entry.Value;

            switch (Key)
            {
                case "Skill":
                    Result = SetStringProperty((string valueString) => SkillName = valueString, Value);
                    break;
                case "Level":
                    Result = SetIntProperty((int valueInt) => Level = valueInt, Value);
                    break;
                case "SkillCap":
                    Result = SetIntProperty((int valueInt) => OtherLevel = valueInt, Value);
                    break;
                case "IsPerformanceSkill":
                    Result = SetBoolProperty((bool valueBool) => IsPerformanceSkill = valueBool, Value);
                    break;
                default:
                    Result = Program.ReportFailure(parsedFile, parsedKey, $"Key '{Key}' not handled");
                    break;
            }

            if (!Result)
                break;
        }

        if (Result && SkillName is not null && Level is not null && OtherLevel is not null)
        {
            if (SkillName == "Dance")
                SkillName = "Performance_Dance";
            else if (SkillName == "ArmorSmithing")
                SkillName = "Armorsmithing";
            else if (IsPerformanceSkill)
                SkillName = $"Performance_{SkillName}";

            Result = Inserter<PgSkill>.SetItemByKey((PgSkill valueSkill) => item.Skill_Key = Parser.GetItemKey(valueSkill), SkillName);
            item.RawLevel = Level;
            item.RawRangeUnlock = OtherLevel - Level;
        }

        return Result;
    }

    private bool ParseInteraction(PgLevelCapInteraction item, string interaction, int level, string parsedFile, string parsedKey)
    {
        if (interaction.Length > 0 && char.IsDigit(interaction[interaction.Length - 1]))
        {
            int FirstDigitIndex = interaction.Length - 1;
            while (FirstDigitIndex > 0 && char.IsDigit(interaction[FirstDigitIndex - 1]))
                FirstDigitIndex--;

            if (FirstDigitIndex > 0 && interaction[FirstDigitIndex - 1] != '_' && interaction[FirstDigitIndex - 1] != ' ')
                interaction = interaction.Substring(0, FirstDigitIndex) + "_" + interaction.Substring(FirstDigitIndex);
        }

        string[] Split = interaction.Split('_');

        if (Split.Length < 3 || Split[0] != "LevelCap")
            return Program.ReportFailure($"Invalid level cap interaction '{interaction}'");

        string MergedSkill = string.Empty;
        int i;
        for (i = 1; i + 1 < Split.Length; i++)
        {
            if (MergedSkill.Length > 0)
                MergedSkill += "_";
            MergedSkill += Split[i];
        }

        if (MergedSkill == "Dance")
            MergedSkill = "Performance_Dance";

        PgSkill ParsedSkill = null!;
        if (!ParserSkill.Parse((PgSkill valueSkill) => ParsedSkill = valueSkill, MergedSkill, parsedFile, parsedKey))
            return false;

        int OtherLevel;
        if (!int.TryParse(Split[i], out OtherLevel) || OtherLevel <= 0)
            return Program.ReportFailure($"Invalid level cap interaction '{interaction}'");

        if (OtherLevel != level + 10 && OtherLevel != level + 5)
            return Program.ReportFailure("Inconsistent interaction level cap");

        PgLevelCapInteraction NewInteraction = new PgLevelCapInteraction();
        NewInteraction.RawLevel = level;
        NewInteraction.RawRangeUnlock = OtherLevel - level;
        NewInteraction.Skill_Key = Parser.GetItemKey(ParsedSkill);

        ParsingContext.AddSuplementaryObject(NewInteraction);
        //item.List.Add(NewInteraction);
        return true;
    }
}

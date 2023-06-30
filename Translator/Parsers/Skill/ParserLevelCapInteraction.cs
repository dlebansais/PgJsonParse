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
            if (IsPerformanceSkill)
                SkillName = $"Performance_{SkillName}";

            Result = Inserter<PgSkill>.SetItemByKey((PgSkill valueSkill) => item.Skill_Key = PgObject.GetItemKey(valueSkill), SkillName);
            item.RawLevel = Level;
            item.RawRangeUnlock = OtherLevel - Level;
        }

        return Result;
    }
}

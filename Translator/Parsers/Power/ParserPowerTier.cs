namespace Translator;

using System.Collections.Generic;
using PgJsonReader;
using PgObjects;

public class ParserPowerTier : Parser
{
    public override object CreateItem()
    {
        return new PgPowerTier();
    }

    public override bool FinishItem(ref object? item, string objectKey, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, string parsedFile, string parsedKey)
    {
        if (item is not PgPowerTier AsPgPowerTier)
            return Program.ReportFailure("Unexpected failure");

        return FinishItem(AsPgPowerTier, contentTable, contentTypeTable, itemCollection, lastItemType, parsedFile, parsedKey);
    }

    private bool FinishItem(PgPowerTier item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, string parsedFile, string parsedKey)
    {
        bool Result = true;

        foreach (KeyValuePair<string, object> Entry in contentTable)
        {
            string Key = Entry.Key;
            object Value = Entry.Value;

            switch (Key)
            {
                case "EffectDescriptions":
                    Result = Inserter<PgPowerEffect>.AddKeylessArray(item.EffectList, Value);
                    break;
                case "SkillLevelPrerequirement":
                    Result = SetIntProperty((int valueInt) => item.RawSkillLevelPrereq = valueInt, Value);
                    break;
                case "MinLevel":
                    Result = SetIntProperty((int valueInt) => item.RawMinLevel = valueInt, Value);
                    break;
                case "MaxLevel":
                    Result = SetIntProperty((int valueInt) => item.RawMaxLevel = valueInt, Value);
                    break;
                case "MinRarity":
                    Result = StringToEnumConversion<RecipeItemKey>.SetEnum((RecipeItemKey valueEnum) => item.MinRarity = valueEnum, $"MinRarity_{Value}");
                    break;
                case "Tier":
                    Result = SetIntProperty((int valueInt) => item.Level = valueInt, Value);
                    break;
                default:
                    Result = Program.ReportFailure(parsedFile, parsedKey, $"Key '{Key}' not handled");
                    break;
            }

            if (!Result)
                break;
        }

        if (Result)
        {
            if (!item.RawSkillLevelPrereq.HasValue)
                Result = Program.ReportFailure(parsedFile, parsedKey, $"Power has no skill level requirement");
        }

        return Result;
    }
}

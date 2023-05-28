namespace Translator;

using System.Collections.Generic;
using PgJsonReader;
using PgObjects;

public class ParserAIAbility : Parser
{
    public override object CreateItem()
    {
        return new PgAIAbility();
    }

    public override bool FinishItem(ref object? item, string objectKey, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, string parsedFile, string parsedKey)
    {
        if (item is not PgAIAbility AsPgAIAbility)
            return Program.ReportFailure("Unexpected failure");

        return FinishItem(AsPgAIAbility, contentTable, contentTypeTable, itemCollection, lastItemType, parsedFile, parsedKey);
    }

    private bool FinishItem(PgAIAbility item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, string parsedFile, string parsedKey)
    {
        bool Result = true;

        foreach (KeyValuePair<string, object> Entry in contentTable)
        {
            string Key = Entry.Key;
            object Value = Entry.Value;

            switch (Key)
            {
                case "minLevel":
                    Result = SetIntProperty((int valueInt) => item.RawMinLevel = valueInt, Value);
                    break;
                case "maxLevel":
                    Result = SetIntProperty((int valueInt) => item.RawMaxLevel = valueInt, Value);
                    break;
                case "minDistance":
                    Result = SetIntProperty((int valueInt) => item.RawMinDistance = valueInt, Value);
                    break;
                case "minRange":
                    Result = SetFloatProperty((float valueFloat) => item.RawMinRange = valueFloat, Value);
                    break;
                case "maxRange":
                    Result = SetIntProperty((int valueInt) => item.RawMaxRange = valueInt, Value);
                    break;
                case "cue":
                    Result = StringToEnumConversion<AbilityCue>.SetEnum((AbilityCue valueEnum) => item.Cue = valueEnum, Value);
                    break;
                case "cueVal":
                    Result = SetIntProperty((int valueInt) => item.RawCueValue = valueInt, Value);
                    break;
                default:
                    Result = Program.ReportFailure(parsedFile, parsedKey, $"Key '{Key}' not handled");
                    break;
            }

            if (!Result)
                break;
        }

        return Result;
    }
}

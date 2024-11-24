namespace Translator;

using System.Collections.Generic;
using PgJsonReader;
using PgObjects;

public class ParserDoT : Parser
{
    public override object CreateItem()
    {
        return new PgDoT();
    }

    public override bool FinishItem(ref object? item, string objectKey, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, string parsedFile, string parsedKey)
    {
        if (item is not PgDoT AsPgDoT)
            return Program.ReportFailure("Unexpected failure");

        return FinishItem(AsPgDoT, contentTable, contentTypeTable, itemCollection, lastItemType, parsedFile, parsedKey);
    }

    private bool FinishItem(PgDoT item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, string parsedFile, string parsedKey)
    {
        bool Result = true;

        foreach (KeyValuePair<string, object> Entry in contentTable)
        {
            string Key = Entry.Key;
            object Value = Entry.Value;

            switch (Key)
            {
                case "DamagePerTick":
                    Result = SetIntProperty((int valueInt) => item.RawDamagePerTick = valueInt, Value);
                    break;
                case "NumTicks":
                    Result = SetIntProperty((int valueInt) => item.RawNumTicks = valueInt, Value);
                    break;
                case "Duration":
                    Result = SetIntProperty((int valueInt) => item.RawDuration = valueInt, Value);
                    break;
                case "DamageType":
                    Result = StringToEnumConversion<DamageType>.SetEnum((DamageType valueEnum) => item.DamageType = valueEnum, Value);
                    break;
                case "SpecialRules":
                    Result = StringToEnumConversion<DoTSpecialRule>.TryParseList(Value, item.SpecialRuleList);
                    break;
                case "AttributesThatDelta":
                    Result = Inserter<PgAttribute>.AddPgObjectArrayByKey<PgAttribute>(item.AttributesThatDeltaList, Value);
                    break;
                case "AttributesThatMod":
                    Result = Inserter<PgAttribute>.AddPgObjectArrayByKey<PgAttribute>(item.AttributesThatModList, Value);
                    break;
                case "Preface":
                    Result = SetStringProperty((string valueString) => item.Preface = valueString, Value);
                    break;
                case "ReqEffectKeyword":
                    Result = StringToEnumConversion<EffectKeyword>.SetEnum((EffectKeyword valueEnum) => item.ReqEffectKeyword = valueEnum, Value);
                    break;
                default:
                    Result = Program.ReportFailure(parsedFile, parsedKey, $"Key '{Key}' not handled");
                    break;
            }

            if (!Result)
                break;
        }

        if (!Result)
            return false;

        if (item.RawDamagePerTick == null)
            return Program.ReportFailure(parsedFile, parsedKey, $"Unexpected empty damage per tick");

        if (item.DamageType == DamageType.Internal_None)
            return Program.ReportFailure(parsedFile, parsedKey, $"Unexpected empty damage type");

        if (item.RawNumTicks == null)
            return Program.ReportFailure(parsedFile, parsedKey, $"Unexpected empty num tick");

        if (item.RawDuration == null)
            return Program.ReportFailure(parsedFile, parsedKey, $"Unexpected empty duration");

        return true;
    }
}

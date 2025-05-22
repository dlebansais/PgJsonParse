namespace Translator;

using System.Collections.Generic;
using System.Diagnostics;
using PgJsonReader;
using PgObjects;

public class ParserAbilityKeyword : Parser
{
    public override object CreateItem()
    {
        return new PgAbilityKeyword();
    }

    public override bool FinishItem(ref object? item, string objectKey, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, string parsedFile, string parsedKey)
    {
        if (item is not PgAbilityKeyword AsPgAbilityKeyword)
            return Program.ReportFailure("Unexpected failure");

        return FinishItem(AsPgAbilityKeyword, contentTable, contentTypeTable, itemCollection, lastItemType, parsedFile, parsedKey);
    }

    private bool FinishItem(PgAbilityKeyword item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, string parsedFile, string parsedKey)
    {
        bool Result = true;

        foreach (KeyValuePair<string, object> Entry in contentTable)
        {
            string Key = Entry.Key;
            object Value = Entry.Value;

            switch (Key)
            {
                case "AttributesThatDeltaAccuracy":
                    Result = Inserter<PgAttribute>.AddPgObjectArrayByKey<PgAttribute>(item.AttributesThatDeltaAccuracyList, Value);
                    break;
                case "AttributesThatDeltaCritChance":
                    Result = Inserter<PgAttribute>.AddPgObjectArrayByKey<PgAttribute>(item.AttributesThatDeltaCritChanceList, Value);
                    break;
                case "AttributesThatDeltaDamage":
                    Result = Inserter<PgAttribute>.AddPgObjectArrayByKey<PgAttribute>(item.AttributesThatDeltaDamageList, Value);
                    break;
                case "AttributesThatDeltaPowerCost":
                    Result = Inserter<PgAttribute>.AddPgObjectArrayByKey<PgAttribute>(item.AttributesThatDeltaPowerCostList, Value);
                    break;
                case "AttributesThatDeltaRange":
                    Result = Inserter<PgAttribute>.AddPgObjectArrayByKey<PgAttribute>(item.AttributesThatDeltaRangeList, Value);
                    break;
                case "AttributesThatDeltaResetTime":
                    Result = Inserter<PgAttribute>.AddPgObjectArrayByKey<PgAttribute>(item.AttributesThatDeltaResetTimeList, Value);
                    break;
                case "AttributesThatModCritDamage":
                    Result = Inserter<PgAttribute>.AddPgObjectArrayByKey<PgAttribute>(item.AttributesThatModCritDamageList, Value);
                    break;
                case "AttributesThatModDamage":
                    Result = Inserter<PgAttribute>.AddPgObjectArrayByKey<PgAttribute>(item.AttributesThatModDamageList, Value);
                    break;
                case "MustHaveAbilityKeywords":
                    Result = StringToEnumConversion<AbilityKeyword>.TryParseList(Value, item.MustHaveAbilityKeywordList);
                    break;
                case "MustHaveActiveSkill":
                    Result = ParserSkill.Parse((PgSkill valueSkill) => item.MustHaveActiveSkill_Key = PgObject.GetItemKey(valueSkill), Value, parsedFile, parsedKey);
                    break;
                case "MustHaveEffectKeywords":
                    Result = StringToEnumConversion<EffectKeyword>.TryParseList(Value, item.MustHaveEffectKeywordList);
                    break;
                case "MustNotHaveAbilityKeywords":
                    Result = StringToEnumConversion<AbilityKeyword>.TryParseList(Value, item.MustNotHaveAbilityKeywordList);
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
        }

        return Result;
    }
}

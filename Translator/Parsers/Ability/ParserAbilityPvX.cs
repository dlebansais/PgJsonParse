namespace Translator;

using System.Collections.Generic;
using PgJsonReader;
using PgObjects;

public class ParserAbilityPvX : Parser
{
    public override object CreateItem()
    {
        return new PgAbilityPvX();
    }

    public override bool FinishItem(ref object? item, string objectKey, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, string parsedFile, string parsedKey)
    {
        if (item is not PgAbilityPvX AsAbilityPvX)
            return Program.ReportFailure("Unexpected failure");

        return FinishItem(AsAbilityPvX, contentTable, contentTypeTable, itemCollection, lastItemType, parsedFile, parsedKey);
    }

    private bool FinishItem(PgAbilityPvX item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, string parsedFile, string parsedKey)
    {
        bool Result = true;

        foreach (KeyValuePair<string, object> Entry in contentTable)
        {
            string Key = Entry.Key;
            object Value = Entry.Value;

            switch (Key)
            {
                case "Damage":
                    Result = SetIntProperty((int valueInt) => item.RawDamage = valueInt, Value);
                    break;
                case "HealthSpecificDamage":
                    Result = SetIntProperty((int valueInt) => item.RawHealthSpecificDamage = valueInt, Value);
                    break;
                case "ExtraDamageIfTargetVulnerable":
                    Result = SetIntProperty((int valueInt) => item.RawExtraDamageIfTargetVulnerable = valueInt, Value);
                    break;
                case "ArmorSpecificDamage":
                    Result = SetIntProperty((int valueInt) => item.RawArmorSpecificDamage = valueInt, Value);
                    break;
                case "Range":
                    Result = SetIntProperty((int valueInt) => item.RawRange = valueInt, Value);
                    break;
                case "PowerCost":
                    Result = SetIntProperty((int valueInt) => item.RawPowerCost = valueInt, Value);
                    break;
                case "ArmorMitigationRatio":
                    Result = SetIntProperty((int valueInt) => item.RawArmorMitigationRatio = valueInt, Value);
                    break;
                case "AoE":
                    Result = SetIntProperty((int valueInt) => item.RawAoE = valueInt, Value);
                    break;
                case "SelfPreEffects":
                    Result = Inserter<PgSelfPreEffect>.AddKeylessArray(item.SelfPreEffectList, Value);
                    break;
                case "RageBoost":
                    Result = SetIntProperty((int valueInt) => item.RawRageBoost = valueInt, Value);
                    break;
                case "RageMultiplier":
                    Result = SetFloatProperty((float valueFloat) => item.RawRageMultiplier = valueFloat, Value);
                    break;
                case "Accuracy":
                    Result = SetFloatProperty((float valueFloat) => item.RawAccuracy = valueFloat, Value);
                    break;
                case "AttributesThatDeltaDamage":
                    Result = Inserter<PgAttribute>.AddPgObjectArrayByKey<PgAttribute>(item.AttributesThatDeltaDamageList, Value);
                    break;
                case "AttributesThatModDamage":
                    Result = Inserter<PgAttribute>.AddPgObjectArrayByKey<PgAttribute>(item.AttributesThatModDamageList, Value);
                    break;
                case "AttributesThatModBaseDamage":
                    Result = Inserter<PgAttribute>.AddPgObjectArrayByKey<PgAttribute>(item.AttributesThatModBaseDamageList, Value);
                    break;
                case "AttributesThatDeltaTaunt":
                    Result = Inserter<PgAttribute>.AddPgObjectArrayByKey<PgAttribute>(item.AttributesThatDeltaTauntList, Value);
                    break;
                case "AttributesThatModTaunt":
                    Result = Inserter<PgAttribute>.AddPgObjectArrayByKey<PgAttribute>(item.AttributesThatModTauntList, Value);
                    break;
                case "AttributesThatDeltaRage":
                    Result = Inserter<PgAttribute>.AddPgObjectArrayByKey<PgAttribute>(item.AttributesThatDeltaRageList, Value);
                    break;
                case "AttributesThatModRage":
                    Result = Inserter<PgAttribute>.AddPgObjectArrayByKey<PgAttribute>(item.AttributesThatModRageList, Value);
                    break;
                case "AttributesThatDeltaRange":
                    Result = Inserter<PgAttribute>.AddPgObjectArrayByKey<PgAttribute>(item.AttributesThatDeltaRangeList, Value);
                    break;
                case "AttributesThatDeltaAccuracy":
                    Result = Inserter<PgAttribute>.AddPgObjectArrayByKey<PgAttribute>(item.AttributesThatDeltaAccuracyList, Value);
                    break;
                case "AttributesThatModCritDamage":
                    Result = Inserter<PgAttribute>.AddPgObjectArrayByKey<PgAttribute>(item.AttributesThatModCritDamageList, Value);
                    break;
                case "AttributesThatDeltaTempTaunt":
                    Result = Inserter<PgAttribute>.AddPgObjectArrayByKey<PgAttribute>(item.AttributesThatDeltaTempTauntList, Value);
                    break;
                case "AttributesThatDeltaAoE":
                    Result = Inserter<PgAttribute>.AddPgObjectArrayByKey<PgAttribute>(item.AttributesThatDeltaAoEList, Value);
                    break;
                case "AttributesThatDeltaDamageIfTargetIsVulnerable":
                    Result = Inserter<PgAttribute>.AddPgObjectArrayByKey<PgAttribute>(item.AttributesThatDeltaDamageIfTargetIsVulnerableList, Value);
                    break;
                case "SpecialValues":
                    Result = Inserter<PgSpecialValue>.AddKeylessArray(item.SpecialValueList, Value);
                    break;
                case "TauntDelta":
                    Result = SetIntProperty((int valueInt) => item.RawTauntDelta = valueInt, Value);
                    break;
                case "TauntMod":
                    Result = SetIntProperty((int valueInt) => item.RawTauntMod = valueInt, Value);
                    break;
                case "TempTauntDelta":
                    Result = SetIntProperty((int valueInt) => item.RawTempTauntDelta = valueInt, Value);
                    break;
                case "RageCost":
                    Result = SetIntProperty((int valueInt) => item.RawRageCost = valueInt, Value);
                    break;
                case "RageCostMod":
                    Result = SetFloatProperty((float valueFloat) => item.RawRageCostMod = valueFloat, Value);
                    break;
                case "DoTs":
                    Result = Inserter<PgDoT>.AddKeylessArray(item.DoTList, Value);
                    break;
                case "CritDamageMod":
                    Result = SetFloatProperty((float valueFloat) => item.RawCritDamageMod = valueFloat, Value);
                    break;
                case "SelfEffectsOnCrit":
                    Result = StringToEnumConversion<SelfEffect>.TryParseList(Value, item.SelfEffectOnCritList);
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

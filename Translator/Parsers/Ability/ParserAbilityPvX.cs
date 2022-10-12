namespace Translator
{
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
                    /*case "MetabolismCost":
                        Result = SetIntProperty((int valueInt) => item.RawMetabolismCost = valueInt, Value);
                        break;*/
                    case "ArmorMitigationRatio":
                        Result = SetIntProperty((int valueInt) => item.RawArmorMitigationRatio = valueInt, Value);
                        break;
                    case "AoE":
                        Result = SetIntProperty((int valueInt) => item.RawAoE = valueInt, Value);
                        break;
                    case "SelfPreEffects":
                        Result = ParseSelfPreEffects(item, Value);
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
                    case "SpecialValues":
                        Result = Inserter<PgSpecialValue>.AddKeylessArray(item.SpecialValueList, Value);
                        break;
                    case "TauntDelta":
                        Result = SetIntProperty((int valueInt) => item.RawTauntDelta = valueInt, Value);
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

        private bool ParseSelfPreEffects(PgAbilityPvX item, object value)
        {
            if (!(value is List<object> StringArray))
                return Program.ReportFailure($"Value {value} was expected to be a list");

            foreach (object StringItem in StringArray)
            {
                if (!(StringItem is string SelfPreEffectString))
                    return Program.ReportFailure($"Value {StringItem} was expected to be a string");

                if (!ParseSelfPreEffect(item, SelfPreEffectString, out PgSelfPreEffect selfPreEffect))
                    return false;

                ParsingContext.AddSuplementaryObject(selfPreEffect);
                item.SelfPreEffectList.Add(selfPreEffect);
            }

            return true;
        }

        private bool ParseSelfPreEffect(PgAbilityPvX item, string value, out PgSelfPreEffect selfPreEffect)
        {
            selfPreEffect = null!;

            int StartIndex = value.IndexOf('(');
            int EndIndex = value.IndexOf(')');
            if (StartIndex > 0 && EndIndex > StartIndex + 1 && EndIndex + 1 == value.Length)
            {
                string Prefix = value.Substring(0, StartIndex);
                if (Prefix == "EnhanceZombie")
                {
                    string Enhancement = value.Substring(StartIndex + 1, EndIndex - StartIndex - 1);
                    if (!StringToEnumConversion<PreEffect>.TryParse(Enhancement, out PreEffect enumValue))
                        return false;

                    selfPreEffect = new PgSelfPreEffectEnhanceZombie() { Value = enumValue };
                }
                else if (Prefix == "ConfigGalvanize")
                {
                    if (value[StartIndex + 1] != ',')
                        return Program.ReportFailure($"Symbol ',' was expected");

                    string Enhancement = value.Substring(StartIndex + 2, EndIndex - StartIndex - 2);
                    if (!int.TryParse(Enhancement, out int Value))
                        return Program.ReportFailure($"Value {Enhancement} was expected to be an int");

                    selfPreEffect = new PgSelfPreEffectConfigGalvanize() { RawValue = Value };
                }
                else
                    return Program.ReportFailure($"Invalid SelfPreEffect format {Prefix}");
            }
            else
            {
                if (!StringToEnumConversion<PreEffect>.TryParse(value, out PreEffect enumValue))
                    return false;

                selfPreEffect = new PgSelfPreEffectSimple() { Value = enumValue };
            }

            return true;
        }
    }
}

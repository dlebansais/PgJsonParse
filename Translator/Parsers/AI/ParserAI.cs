﻿namespace Translator
{
    using System.Collections.Generic;
    using PgJsonReader;
    using PgObjects;

    public class ParserAI : Parser
    {
        public override object CreateItem()
        {
            return new PgAI();
        }

        public override bool FinishItem(ref object? item, string objectKey, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, string parsedFile, string parsedKey)
        {
            if (item is not PgAI AsPgAI)
                return Program.ReportFailure("Unexpected failure");

            return FinishItem(AsPgAI, contentTable, contentTypeTable, itemCollection, lastItemType, parsedFile, parsedKey);
        }

        private bool FinishItem(PgAI item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, string parsedFile, string parsedKey)
        {
            bool Result = true;

            foreach (KeyValuePair<string, object> Entry in contentTable)
            {
                string Key = Entry.Key;
                object Value = Entry.Value;

                switch (Key)
                {
                    case "Abilities":
                        Result = Inserter<PgAIAbilitySet>.SetItemProperty((PgAIAbilitySet valueAIAbilitySet) => item.Abilities = valueAIAbilitySet, Value);
                        break;
                    case "Comment":
                        Result = SetStringProperty((string valueString) => item.Comment = valueString, Value);
                        break;
                    case "UncontrolledPet":
                        Result = SetBoolProperty((bool valueBool) => item.SetIsUncontrolledPet(valueBool), Value);
                        break;
                    case "ServerDriven":
                        Result = SetBoolProperty((bool valueBool) => item.SetIsServerDriven(valueBool), Value);
                        break;
                    case "MinDelayBetweenAbilities":
                        Result = SetFloatProperty((float valueFloat) => item.RawMinDelayBetweenAbilities = valueFloat, Value);
                        break;
                    case "UseAbilitiesWithoutEnemyTarget":
                        Result = SetBoolProperty((bool valueBool) => item.SetUseAbilitiesWithoutEnemyTarget(valueBool), Value);
                        break;
                    case "Swimming":
                        Result = SetBoolProperty((bool valueBool) => item.SetIsSwimming(valueBool), Value);
                        break;
                    case "MobilityType":
                        Result = StringToEnumConversion<MobilityType>.SetEnum((MobilityType valueEnum) => item.MobilityType = valueEnum, Value);
                        break;
                    case "Strategy":
                        Result = StringToEnumConversion<Strategy>.SetEnum((Strategy valueEnum) => item.Strategy = valueEnum, Value);
                        break;
                    case "FlyOffset":
                        Result = SetIntProperty((int valueInt) => item.RawFlyOffset = valueInt, Value);
                        break;
                    case "Flying":
                        Result = SetBoolProperty((bool valueBool) => item.SetIsFlying(valueBool), Value);
                        break;
                    case "Description":
                        Result = SetStringProperty((string valueString) => item.Description = valueString, Value);
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
}

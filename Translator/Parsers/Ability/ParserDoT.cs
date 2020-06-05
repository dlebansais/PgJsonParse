﻿namespace Translator
{
    using PgJsonObjects;
    using PgJsonReader;
    using System.Collections.Generic;

    public class ParserDoT : Parser
    {
        public override object CreateItem()
        {
            return new PgDoT();
        }

        public override bool FinishItem(ref object item, string objectKey, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> ContentTypeTable, List<object> itemCollection, Json.Token LastItemType, string parsedFile, string parsedKey)
        {
            if (!(item is PgDoT AsPgDoT))
                return Program.ReportFailure("Unexpected failure");

            return FinishItem(AsPgDoT, contentTable, ContentTypeTable, itemCollection, LastItemType, parsedFile, parsedKey);
        }

        private bool FinishItem(PgDoT item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> ContentTypeTable, List<object> itemCollection, Json.Token LastItemType, string parsedFile, string parsedKey)
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
                        Result = Inserter<DamageType>.SetEnum((DamageType valueEnum) => item.DamageType = valueEnum, Value);
                        break;
                    case "SpecialRules":
                        Result = StringToEnumConversion<DoTSpecialRule>.TryParseList(Value, item.SpecialRuleList);
                        break;
                    case "AttributesThatDelta":
                        Result = Inserter<PgAttribute>.AddArray(item.AttributesThatDeltaList, Value);
                        break;
                    case "AttributesThatMod":
                        Result = Inserter<PgAttribute>.AddArray(item.AttributesThatModList, Value);
                        break;
                    case "Preface":
                        Result = SetStringProperty((string valueString) => item.Preface = valueString, Value);
                        break;
                    default:
                        Result = Program.ReportFailure(parsedFile, parsedKey, $"Key '{Key}'not handled");
                        break;
                }

                if (!Result)
                    break;
            }

            return Result;
        }
    }
}
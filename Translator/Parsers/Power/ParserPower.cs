namespace Translator
{
    using PgObjects;
    using PgJsonReader;
    using System.Collections.Generic;

    public class ParserPower : Parser
    {
        public override object CreateItem()
        {
            return new PgPower();
        }

        public override bool FinishItem(ref object item, string objectKey, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> ContentTypeTable, List<object> itemCollection, Json.Token LastItemType, string parsedFile, string parsedKey)
        {
            if (!(item is PgPower AsPgPower))
                return Program.ReportFailure("Unexpected failure");

            return FinishItem(AsPgPower, contentTable, ContentTypeTable, itemCollection, LastItemType, parsedFile, parsedKey);
        }

        private bool FinishItem(PgPower item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> ContentTypeTable, List<object> itemCollection, Json.Token LastItemType, string parsedFile, string parsedKey)
        {
            bool Result = true;

            foreach (KeyValuePair<string, object> Entry in contentTable)
            {
                string Key = Entry.Key;
                object Value = Entry.Value;

                switch (Key)
                {
                    case "Prefix":
                        Result = SetStringProperty((string valueString) => item.Prefix = valueString, Value);
                        break;
                    case "Suffix":
                        Result = SetStringProperty((string valueString) => item.Suffix = valueString, Value);
                        break;
                    case "Tiers":
                        Result = Inserter<PgPowerTierList>.SetItemProperty((PgPowerTierList valuePowerTierList) => item.PowerTierList = valuePowerTierList, Value);
                        break;
                    case "Slots":
                        Result = StringToEnumConversion<ItemSlot>.TryParseList(Value, item.SlotList);
                        break;
                    case "Skill":
                        Result = ParserSkill.Parse((PgSkill valueSkill) => item.Skill = valueSkill, Value, parsedFile, parsedKey);
                        break;
                    case "IsUnavailable":
                        Result = SetBoolProperty((bool valueBool) => item.RawIsUnavailable = valueBool, Value);
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

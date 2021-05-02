namespace Translator
{
    using System.Collections.Generic;
    using PgJsonReader;
    using PgObjects;

    public class ParserItemBehavior : Parser
    {
        public override object CreateItem()
        {
            return new PgItemBehavior();
        }

        public override bool FinishItem(ref object? item, string objectKey, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, string parsedFile, string parsedKey)
        {
            if (item is not PgItemBehavior AsPgItemBehavior)
                return Program.ReportFailure("Unexpected failure");

            return FinishItem(AsPgItemBehavior, contentTable, contentTypeTable, itemCollection, lastItemType, parsedFile, parsedKey);
        }

        private bool FinishItem(PgItemBehavior item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, string parsedFile, string parsedKey)
        {
            bool Result = true;

            foreach (KeyValuePair<string, object> Entry in contentTable)
            {
                string Key = Entry.Key;
                object Value = Entry.Value;

                switch (Key)
                {
                    case "UseVerb":
                        Result = StringToEnumConversion<ItemUseVerb>.SetEnum((ItemUseVerb valueEnum) => item.UseVerb = valueEnum, Value);
                        break;
                    case "UseRequirements":
                        Result = StringToEnumConversion<ItemUseRequirement>.TryParseList(Value, item.UseRequirementList);
                        break;
                    case "UseAnimation":
                        Result = StringToEnumConversion<ItemUseAnimation>.SetEnum((ItemUseAnimation valueEnum) => item.UseAnimation = valueEnum, Value);
                        break;
                    case "UseDelayAnimation":
                        Result = StringToEnumConversion<ItemUseAnimation>.SetEnum((ItemUseAnimation valueEnum) => item.UseDelayAnimation = valueEnum, Value);
                        break;
                    case "MetabolismCost":
                        Result = SetIntProperty((int valueInt) => item.RawMetabolismCost = valueInt, Value);
                        break;
                    case "UseDelay":
                        Result = SetFloatProperty((float valueFloat) => item.RawUseDelay = valueFloat, Value);
                        break;
                    default:
                        Result = Program.ReportFailure(parsedFile, parsedKey, $"Key '{Key}' not handled");
                        break;
                }

                if (!Result)
                    break;
            }

            if (!contentTable.ContainsKey("UseVerb"))
                Result = Program.ReportFailure(parsedFile, parsedKey, "Behavior has no verb");

            return Result;
        }
    }
}

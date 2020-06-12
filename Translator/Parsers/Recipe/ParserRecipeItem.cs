namespace Translator
{
    using PgObjects;
    using PgJsonReader;
    using System.Collections.Generic;

    public class ParserRecipeItem : Parser
    {
        public override object CreateItem()
        {
            return new PgRecipeItem();
        }

        public override bool FinishItem(ref object item, string objectKey, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> ContentTypeTable, List<object> itemCollection, Json.Token LastItemType, string parsedFile, string parsedKey)
        {
            if (!(item is PgRecipeItem AsPgRecipeItem))
                return Program.ReportFailure("Unexpected failure");

            return FinishItem(AsPgRecipeItem, contentTable, ContentTypeTable, itemCollection, LastItemType, parsedFile, parsedKey);
        }

        private bool FinishItem(PgRecipeItem item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> ContentTypeTable, List<object> itemCollection, Json.Token LastItemType, string parsedFile, string parsedKey)
        {
            bool Result = true;

            foreach (KeyValuePair<string, object> Entry in contentTable)
            {
                string Key = Entry.Key;
                object Value = Entry.Value;

                switch (Key)
                {
                    case "ItemCode":
                        Result = Inserter<PgItem>.SetItemByKey((PgItem valueItem) => item.Item = valueItem, $"item_{Value}");
                        break;
                    case "StackSize":
                        Result = SetIntProperty((int valueInt) => item.RawStackSize = valueInt, Value);
                        break;
                    case "PercentChance":
                        Result = SetFloatProperty((float valueFloat) => item.RawPercentChance = valueFloat, Value);
                        break;
                    case "ItemKeys":
                        Result = StringToEnumConversion<RecipeItemKey>.TryParseList(Value, item.ItemKeyList);
                        break;
                    case "Desc":
                        Result = SetStringProperty((string valueString) => item.Description = valueString, Value);
                        break;
                    case "ChanceToConsume":
                        Result = SetFloatProperty((float valueFloat) => item.RawChanceToConsume = valueFloat, Value);
                        break;
                    case "DurabilityConsumed":
                        Result = SetFloatProperty((float valueFloat) => item.RawDurabilityConsumed = valueFloat, Value);
                        break;
                    case "AttuneToCrafter":
                        Result = SetBoolProperty((bool valueBool) => item.RawAttuneToCrafter = valueBool, Value);
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

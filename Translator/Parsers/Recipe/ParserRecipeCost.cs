namespace Translator
{
    using System.Collections.Generic;
    using PgJsonReader;
    using PgObjects;

    public class ParserRecipeCost : Parser
    {
        public override object CreateItem()
        {
            return new PgRecipeCost();
        }

        public override bool FinishItem(ref object? item, string objectKey, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, string parsedFile, string parsedKey)
        {
            if (item is not PgRecipeCost AsPgRecipeCost)
                return Program.ReportFailure("Unexpected failure");

            return FinishItem(AsPgRecipeCost, contentTable, contentTypeTable, itemCollection, lastItemType, parsedFile, parsedKey);
        }

        private bool FinishItem(PgRecipeCost item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, string parsedFile, string parsedKey)
        {
            bool Result = true;

            foreach (KeyValuePair<string, object> Entry in contentTable)
            {
                string Key = Entry.Key;
                object Value = Entry.Value;

                switch (Key)
                {
                    case "Currency":
                        Result = StringToEnumConversion<RecipeCurrency>.SetEnum((RecipeCurrency valueEnum) => item.Currency = valueEnum, Value);
                        break;
                    case "Price":
                        Result = SetFloatProperty((float valueFloat) => item.RawPrice = valueFloat, Value);
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
                if (item.Currency == RecipeCurrency.Internal_None || !item.RawPrice.HasValue)
                    return Program.ReportFailure(parsedFile, parsedKey, "Missing cost info");
            }

            return Result;
        }
    }
}

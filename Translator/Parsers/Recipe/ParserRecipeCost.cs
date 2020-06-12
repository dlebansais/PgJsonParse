﻿namespace Translator
{
    using PgObjects;
    using PgJsonReader;
    using System.Collections.Generic;

    public class ParserRecipeCost : Parser
    {
        public override object CreateItem()
        {
            return new PgRecipeCost();
        }

        public override bool FinishItem(ref object item, string objectKey, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> ContentTypeTable, List<object> itemCollection, Json.Token LastItemType, string parsedFile, string parsedKey)
        {
            if (!(item is PgRecipeCost AsPgRecipeCost))
                return Program.ReportFailure("Unexpected failure");

            return FinishItem(AsPgRecipeCost, contentTable, ContentTypeTable, itemCollection, LastItemType, parsedFile, parsedKey);
        }

        private bool FinishItem(PgRecipeCost item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> ContentTypeTable, List<object> itemCollection, Json.Token LastItemType, string parsedFile, string parsedKey)
        {
            bool Result = true;

            foreach (KeyValuePair<string, object> Entry in contentTable)
            {
                string Key = Entry.Key;
                object Value = Entry.Value;

                switch (Key)
                {
                    case "Currency":
                        Result = Inserter<RecipeCurrency>.SetEnum((RecipeCurrency valueEnum) => item.Currency = valueEnum, Value);
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

            return Result;
        }
    }
}

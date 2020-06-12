namespace Translator
{
    using PgObjects;
    using PgJsonReader;
    using System.Collections.Generic;

    public class ParserXpTable : Parser
    {
        public override object CreateItem()
        {
            return new PgXpTable();
        }

        public override bool FinishItem(ref object item, string objectKey, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> ContentTypeTable, List<object> itemCollection, Json.Token LastItemType, string parsedFile, string parsedKey)
        {
            if (!(item is PgXpTable AsPgXpTable))
                return Program.ReportFailure("Unexpected failure");

            return FinishItem(AsPgXpTable, contentTable, ContentTypeTable, itemCollection, LastItemType, parsedFile, parsedKey);
        }

        private bool FinishItem(PgXpTable item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> ContentTypeTable, List<object> itemCollection, Json.Token LastItemType, string parsedFile, string parsedKey)
        {
            bool Result = true;

            foreach (KeyValuePair<string, object> Entry in contentTable)
            {
                string Key = Entry.Key;
                object Value = Entry.Value;

                switch (Key)
                {
                    case "InternalName":
                        Result = SetStringProperty((string valueString) => item.InternalName = valueString, Value);
                        break;
                    case "XpAmounts":
                        Result = ParseXpAmounts(item, Value, parsedFile, parsedKey);
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

        private bool ParseXpAmounts(PgXpTable item, object value, string parsedFile, string parsedKey)
        {
            if (!(value is List<object> ArrayAmount))
                return Program.ReportFailure($"Value '{value}' was expected to be a list");

            foreach (object Item in ArrayAmount)
            {
                if (!(Item is int ValueAmount))
                    return Program.ReportFailure($"Value '{Item}' was expected to be an int");

                item.XpAmountList.Add(ValueAmount);
            }

            return true;
        }
    }
}

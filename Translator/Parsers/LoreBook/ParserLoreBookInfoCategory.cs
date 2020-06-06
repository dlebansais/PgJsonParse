namespace Translator
{
    using PgJsonObjects;
    using PgJsonReader;
    using System.Collections.Generic;

    public class ParserLoreBookInfoCategory : Parser
    {
        public override object CreateItem()
        {
            return new PgLoreBookInfoCategory();
        }

        public override bool FinishItem(ref object item, string objectKey, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> ContentTypeTable, List<object> itemCollection, Json.Token LastItemType, string parsedFile, string parsedKey)
        {
            if (!(item is PgLoreBookInfoCategory AsPgLoreBookInfoCategory))
                return Program.ReportFailure("Unexpected failure");

            return FinishItem(AsPgLoreBookInfoCategory, contentTable, ContentTypeTable, itemCollection, LastItemType, parsedFile, parsedKey);
        }

        private bool FinishItem(PgLoreBookInfoCategory item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> ContentTypeTable, List<object> itemCollection, Json.Token LastItemType, string parsedFile, string parsedKey)
        {
            bool Result = true;

            foreach (KeyValuePair<string, object> Entry in contentTable)
            {
                string Key = Entry.Key;
                object Value = Entry.Value;

                switch (Key)
                {
                    case "Title":
                        Result = SetStringProperty((string valueString) => item.Title = valueString, Value);
                        break;
                    case "SubTitle":
                        Result = SetStringProperty((string valueString) => item.SubTitle = valueString, Value);
                        break;
                    case "SortTitle":
                        Result = SetStringProperty((string valueString) => item.SortTitle = valueString, Value);
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

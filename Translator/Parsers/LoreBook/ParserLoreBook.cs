namespace Translator
{
    using PgObjects;
    using PgJsonReader;
    using System.Collections.Generic;

    public class ParserLoreBook : Parser
    {
        public override object CreateItem()
        {
            return new PgLoreBook();
        }

        public override bool FinishItem(ref object item, string objectKey, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> ContentTypeTable, List<object> itemCollection, Json.Token LastItemType, string parsedFile, string parsedKey)
        {
            if (!(item is PgLoreBook AsPgLoreBook))
                return Program.ReportFailure("Unexpected failure");

            return FinishItem(AsPgLoreBook, contentTable, ContentTypeTable, itemCollection, LastItemType, parsedFile, parsedKey);
        }

        private bool FinishItem(PgLoreBook item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> ContentTypeTable, List<object> itemCollection, Json.Token LastItemType, string parsedFile, string parsedKey)
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
                    case "LocationHint":
                        Result = SetStringProperty((string valueString) => item.LocationHint = valueString, Value);
                        break;
                    case "Category":
                        Result = ParserLoreBookInfoCategory(item, Value, parsedFile, parsedKey);
                        break;
                    case "Keywords":
                        Result = StringToEnumConversion<LoreBookKeyword>.TryParseList(Value, item.KeywordList);
                        break;
                    case "IsClientLocal":
                        Result = SetBoolProperty((bool valueBool) => item.RawIsClientLocal = valueBool, Value);
                        break;
                    case "Visibility":
                        Result = StringToEnumConversion<LoreBookVisibility>.SetEnum((LoreBookVisibility valueEnum) => item.Visibility = valueEnum, Value);
                        break;
                    case "InternalName":
                        Result = SetStringProperty((string valueString) => item.InternalName = valueString, Value);
                        break;
                    case "Text":
                        Result = SetStringProperty((string valueString) => item.Text = Tools.CleanedUpString(valueString), Value);
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

        private bool ParserLoreBookInfoCategory(PgLoreBook item, object value, string parsedFile, string parsedKey)
        {
            if (!(value is string ValueKey))
                return Program.ReportFailure(parsedFile, parsedKey, $"Value '{value}' was expected to be a string");

            if (!ParsingContext.ObjectKeyTable.ContainsKey(typeof(PgLoreBookInfo)))
                return Program.ReportFailure(parsedFile, parsedKey, "No lore book info");

            Dictionary<string, ParsingContext> Table = ParsingContext.ObjectKeyTable[typeof(PgLoreBookInfo)];

            if (!Table.ContainsKey("Categories"))
                return Program.ReportFailure(parsedFile, parsedKey, "No lore book info categories");

            ParsingContext Context = Table["Categories"];
            Dictionary<string, object> ContentTable = Context.ContentTable;

            if (!ContentTable.ContainsKey(ValueKey))
                return Program.ReportFailure(parsedFile, parsedKey, $"Category {ValueKey} not found");

            if (!(ContentTable[ValueKey] is ParsingContext CategoryContext))
                return Program.ReportFailure(parsedFile, parsedKey, $"Category {ValueKey} is expected to be a parsing context");

            if (!(CategoryContext.Item is PgLoreBookInfoCategory AsCategory))
                return Program.ReportFailure(parsedFile, parsedKey, $"Category {ValueKey} is not the expected object");

            item.Category = AsCategory;
            return true;
        }
    }
}

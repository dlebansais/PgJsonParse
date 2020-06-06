﻿namespace Translator
{
    using PgJsonObjects;
    using PgJsonReader;
    using System.Collections.Generic;

    public class ParserPlayerTitle : Parser
    {
        public override object CreateItem()
        {
            return new PgPlayerTitle();
        }

        public override bool FinishItem(ref object item, string objectKey, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> ContentTypeTable, List<object> itemCollection, Json.Token LastItemType, string parsedFile, string parsedKey)
        {
            if (!(item is PgPlayerTitle AsPgPlayerTitle))
                return Program.ReportFailure("Unexpected failure");

            return FinishItem(AsPgPlayerTitle, contentTable, ContentTypeTable, itemCollection, LastItemType, parsedFile, parsedKey);
        }

        private bool FinishItem(PgPlayerTitle item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> ContentTypeTable, List<object> itemCollection, Json.Token LastItemType, string parsedFile, string parsedKey)
        {
            bool Result = true;

            foreach (KeyValuePair<string, object> Entry in contentTable)
            {
                string Key = Entry.Key;
                object Value = Entry.Value;

                switch (Key)
                {
                    case "Title":
                        Result = ParseTitle(item, Value, parsedFile, parsedKey);
                        break;
                    case "Tooltip":
                        Result = SetStringProperty((string valueString) => item.Tooltip = valueString, Value);
                        break;
                    case "Keywords":
                        Result = StringToEnumConversion<TitleKeyword>.TryParseList(Value, item.KeywordList);
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

        private bool ParseTitle(PgPlayerTitle item, object value, string parsedFile, string parsedKey)
        {
            if (!(value is string ValueTitle))
                return Program.ReportFailure(parsedFile, parsedKey, $"Value '{value}' was expected to be a string");

            item.Title = StripStringTags(ValueTitle);
            return true;
        }

        private string StripStringTags(string s)
        {
            int TagStartIndex, TagEndIndex;

            while ((TagStartIndex = s.IndexOf('<')) >= 0 && (TagEndIndex = s.IndexOf('>', TagStartIndex)) >= 0)
            {
                s = s.Substring(0, TagStartIndex) + s.Substring(TagEndIndex + 1);
            }

            return s;
        }
    }
}
